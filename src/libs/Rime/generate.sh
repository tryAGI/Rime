#!/usr/bin/env bash
set -euo pipefail

# Rime AI has no public OpenAPI spec - openapi.yaml is hand-written from
# the public docs at https://docs.rime.ai and lives at the repo root.

dotnet tool install --global autosdk.cli --prerelease
rm -rf Generated
cp ../../../openapi.yaml openapi.yaml

autosdk generate openapi.yaml \
  --namespace Rime \
  --clientClassName RimeClient \
  --targetFramework net10.0 \
  --output Generated \
  --exclude-deprecated-operations \
  --security-scheme Http:Header:Bearer
