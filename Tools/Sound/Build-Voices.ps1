param([string]$ProjectRoot = (Resolve-Path (Join-Path $PSScriptRoot '..\..')).Path)
$ErrorActionPreference = 'Stop'
Add-Type -AssemblyName System.Speech
$config = Get-Content -LiteralPath (Join-Path $PSScriptRoot 'voices.json') -Raw -Encoding UTF8 | ConvertFrom-Json
$output = Join-Path $ProjectRoot 'Assets\Resources\CampusSound'
New-Item -ItemType Directory -Path $output -Force | Out-Null
$synth = New-Object System.Speech.Synthesis.SpeechSynthesizer
try {
    $format = New-Object System.Speech.AudioFormat.SpeechAudioFormatInfo(22050, [System.Speech.AudioFormat.AudioBitsPerSample]::Sixteen, [System.Speech.AudioFormat.AudioChannel]::Mono)
    foreach ($entry in $config.voices) {
        $synth.SelectVoice($entry.voice)
        $synth.Rate = -1
        $synth.Volume = 90
        $synth.SetOutputToWaveFile((Join-Path $output ($entry.clip + '.wav')), $format)
        $synth.Speak($entry.text)
        $synth.SetOutputToNull()
        Write-Output ('Generated ' + $entry.clip)
    }
} finally { $synth.Dispose() }
Write-Output 'Now run python Tools/Sound/build_audio.py to apply the PA effect and generate sound effects.'
