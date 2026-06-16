#!/usr/bin/env bash
set -euo pipefail

install_autosdk_cli() {
  dotnet tool update --global autosdk.cli --prerelease >/dev/null 2>&1 || \
    dotnet tool install --global autosdk.cli --prerelease
}

# Rime AI has no public OpenAPI spec - openapi.yaml is hand-written from
# the public docs at https://docs.rime.ai and lives at the repo root.
install_autosdk_cli
rm -rf Generated
cp ../../../openapi.yaml openapi.yaml

autosdk generate openapi.yaml \
  --namespace Rime \
  --clientClassName RimeClient \
  --targetFramework net10.0 \
  --output Generated \
  --exclude-deprecated-operations \
  --security-scheme Http:Header:Bearer

rm -rf ../../cli/Rime.CLI

autosdk cli-project openapi.yaml \
  --output ../../cli/Rime.CLI \
  --sdk-project ../../libs/Rime/Rime.csproj \
  --targetFramework net10.0 \
  --namespace Rime \
  --clientClassName RimeClient \
  --package-id Rime.CLI \
  --tool-command-name rime \
  --user-secrets-id Rime.CLI \
  --api-key-env-var RIME_API_KEY \
  --base-url-env-var RIME_BASE_URL \
  --cli-credential-file \
  --exclude-deprecated-operations \
  --security-scheme Http:Header:Bearer
