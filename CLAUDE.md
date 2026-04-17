# CLAUDE.md -- Rime SDK

## Overview

Auto-generated C# SDK for [Rime AI](https://rime.ai/) -- ultra-low-latency (<200ms)
conversational text-to-speech with 300+ voices across Arcana, Mist v2, and Mist v3 models.

**No public OpenAPI spec exists** -- `openapi.yaml` was manually created from
the public docs at https://docs.rime.ai (Mist v2/v3 HTTP, Arcana HTTP, voices endpoints).

## Build & Test

```bash
dotnet build Rime.slnx
dotnet test src/tests/IntegrationTests/
```

## Auth

Standard Bearer token auth (sent as `Authorization: Bearer <API_KEY>`):

```csharp
var client = new RimeClient(apiKey); // RIME_API_KEY env var
```

## Key Files

- `openapi.yaml` -- **Manually maintained** OpenAPI spec (no public spec from Rime)
- `src/libs/Rime/generate.sh` -- Runs autosdk with `--security-scheme Http:Header:Bearer` (copies spec from repo root)
- `src/libs/Rime/Generated/` -- **Never edit** -- auto-generated code
- `src/libs/Rime/Extensions/RimeClient.Tools.cs` -- MEAI AIFunction tools
- `src/tests/IntegrationTests/Tests.cs` -- Test helper with bearer auth
- `src/tests/IntegrationTests/Examples/` -- Example tests (also generate docs)

## API Base URL

`https://users.rime.ai`

## Endpoints

| Endpoint | Method | Description |
|----------|--------|-------------|
| `/v1/rime-tts` | POST | Synthesize speech (Arcana, Mist v2, Mist v3) -- returns audio bytes |
| `/data/voices/all-v2.json` | GET | List voice names grouped by model -> language |
| `/data/voices/voice_details.json` | GET | List voice details (gender, age, dialect, genre, etc.) |

## Models

- **Arcana** -- flagship conversational voices (3,000 char limit)
- **Mist v3** -- ultra-low-latency streaming (500 char limit, default for realtime)
- **Mist v2** -- streaming fallback with extra controls (speedAlpha, noTextNormalization, saveOovs)

## MEAI Tools

| Tool | Description |
|------|-------------|
| `AsTextToSpeechTool()` | Synthesize speech from text (default voice / model configurable) |
| `AsListVoicesTool()` | List voice names grouped by model + language |
| `AsListVoiceDetailsTool()` | List voice metadata (gender, dialect, genre, ...) |

## Not Implemented

- Streaming variants (SSE, WebSocket, multipart JSON frames) -- HTTP `/v1/rime-tts`
  already streams audio bytes; WebSocket/AsyncAPI coverage can be added later.
- Arcana legacy endpoints (websockets, websockets-json) -- superseded by Mist v3.
