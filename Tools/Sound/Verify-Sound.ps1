param(
    [Parameter(Mandatory=$true)][string]$EditorPath,
    [string]$ProjectRoot = (Resolve-Path (Join-Path $PSScriptRoot '..\..')).Path,
    [string]$ValidationRoot = (Join-Path $env:TEMP ('CampusSoundVerification-' + [guid]::NewGuid().ToString('N')))
)
$ErrorActionPreference = 'Stop'
if (Test-Path -LiteralPath $ValidationRoot) { throw 'ValidationRoot must be a new directory, so existing work is never overwritten.' }
New-Item -ItemType Directory -Path "$ValidationRoot\Assets\Editor","$ValidationRoot\Packages","$ValidationRoot\ProjectSettings" -Force | Out-Null
Copy-Item -LiteralPath "$ProjectRoot\Assets\Scripts" -Destination "$ValidationRoot\Assets\Scripts" -Recurse
New-Item -ItemType Directory -Path "$ValidationRoot\Assets\Resources" -Force | Out-Null
Copy-Item -LiteralPath "$ProjectRoot\Assets\Resources\CampusSound" -Destination "$ValidationRoot\Assets\Resources\CampusSound" -Recurse
Copy-Item -LiteralPath "$PSScriptRoot\Validation\SoundVerification.cs" -Destination "$ValidationRoot\Assets\Editor\SoundVerification.cs"
$manifest = @{dependencies=@{
    'com.unity.xr.interaction.toolkit'='3.5.1'; 'com.unity.xr.core-utils'='2.6.0';
    'com.unity.inputsystem'='1.20.0'; 'com.unity.ugui'='2.0.0';
    'com.unity.modules.audio'='1.0.0'; 'com.unity.modules.physics'='1.0.0';
    'com.unity.modules.ui'='1.0.0'; 'com.unity.modules.imgui'='1.0.0'; 'com.unity.modules.jsonserialize'='1.0.0'
}}
$manifest | ConvertTo-Json | Set-Content -LiteralPath "$ValidationRoot\Packages\manifest.json" -Encoding UTF8
$editorVersion = Split-Path (Split-Path (Split-Path $EditorPath -Parent) -Parent) -Leaf
"m_EditorVersion: $editorVersion" | Set-Content -LiteralPath "$ValidationRoot\ProjectSettings\ProjectVersion.txt" -Encoding UTF8
$arguments = @('-batchmode','-nographics','-projectPath',('"' + $ValidationRoot + '"'),'-executeMethod','SoundVerification.Run','-logFile',('"' + "$ValidationRoot\validation.log" + '"'))
$process = Start-Process -FilePath $EditorPath -ArgumentList $arguments -WindowStyle Hidden -PassThru -Wait
if (Test-Path -LiteralPath "$ValidationRoot\sound-verification.txt") { Get-Content -LiteralPath "$ValidationRoot\sound-verification.txt" }
Write-Output "Validation files: $ValidationRoot"
if ($process.ExitCode -ne 0) { throw "Unity validation failed. Inspect $ValidationRoot\validation.log" }
