# Setup-MusicGuideApp.ps1

# Primero creamos el proyecto Blazor WebAssembly
dotnet new blazorwasm -o MusicGuideApp --pwa

# Nos movemos al directorio del proyecto
Set-Location MusicGuideApp

# Instalamos los paquetes NuGet necesarios
dotnet add package Microsoft.AspNetCore.Components.WebAssembly
dotnet add package Microsoft.AspNetCore.Components.WebAssembly.DevServer
dotnet add package Microsoft.Extensions.Http

# Función auxiliar para crear directorios y archivos
function Create-DirectoryAndFile {
    param (
        [string]$path,
        [string]$content = ""
    )
    
    $dir = Split-Path $path
    if (!(Test-Path $dir)) {
        New-Item -ItemType Directory -Path $dir -Force
    }
    
    if (!(Test-Path $path)) {
        New-Item -ItemType File -Path $path -Force
        if ($content) {
            Set-Content -Path $path -Value $content
        }
    }
}

# Creación de la estructura de directorios y archivos

# Client/Components
$componentsPath = "Client/Components"
@(
    "$componentsPath/AudioPlayer/AudioPlayerComponent.razor",
    "$componentsPath/AudioPlayer/AudioPlayerService.cs",
    "$componentsPath/AudioPlayer/IAudioPlayerService.cs",
    "$componentsPath/SongLibrary/SongLibraryComponent.razor",
    "$componentsPath/SongLibrary/SongService.cs",
    "$componentsPath/SongLibrary/ISongService.cs",
    "$componentsPath/ChordDisplay/ChordDisplayComponent.razor",
    "$componentsPath/LyricDisplay/LyricDisplayComponent.razor"
) | ForEach-Object { Create-DirectoryAndFile $_ }

# Client/Models
$modelsPath = "Client/Models"
@(
    "$modelsPath/Song.cs",
    "$modelsPath/Section.cs",
    "$modelsPath/LyricLine.cs",
    "$modelsPath/AudioTrack.cs"
) | ForEach-Object { Create-DirectoryAndFile $_ }

# Client/Services
$servicesPath = "Client/Services"
@(
    "$servicesPath/Audio/WebAudioService.cs",
    "$servicesPath/Audio/IWebAudioService.cs",
    "$servicesPath/Storage/LocalStorageService.cs",
    "$servicesPath/Storage/ILocalStorageService.cs"
) | ForEach-Object { Create-DirectoryAndFile $_ }

# Client/wwwroot/js
$wwwrootPath = "Client/wwwroot"
@(
    "$wwwrootPath/js/audioInterop.js",
    "$wwwrootPath/js/webAudioManager.js"
) | ForEach-Object { Create-DirectoryAndFile $_ }

# Shared
$sharedPath = "Shared"
@(
    "$sharedPath/DTOs/SongDTO.cs",
    "$sharedPath/DTOs/AudioTrackDTO.cs",
    "$sharedPath/Enums/PlaybackState.cs",
    "$sharedPath/Enums/SectionType.cs",
    "$sharedPath/Constants/AudioConstants.cs"
) | ForEach-Object { Create-DirectoryAndFile $_ }

# Server
$serverPath = "Server"
@(
    "$serverPath/Controllers/SongController.cs",
    "$serverPath/Controllers/AudioController.cs",
    "$serverPath/Services/Storage/AudioFileStorage.cs"
) | ForEach-Object { Create-DirectoryAndFile $_ }

# Crear los archivos JavaScript con contenido inicial
$webAudioManagerContent = @"
// webAudioManager.js
let audioContext;
let tracks = new Map();
let dotNetRef;
let currentTime = 0;
let isPlaying = false;

export function initialize(dotNetReference) {
    dotNetRef = dotNetReference;
    audioContext = new (window.AudioContext || window.webkitAudioContext)();
    requestAnimationFrame(updateTime);
}
"@

Set-Content -Path "Client/wwwroot/js/webAudioManager.js" -Value $webAudioManagerContent

# Crear el archivo principal de estilos CSS con contenido inicial
$cssContent = @"
.audio-player {
    max-width: 800px;
    margin: 0 auto;
    padding: 20px;
}

.chord-display {
    font-size: 2.5rem;
    text-align: center;
    margin: 20px 0;
    padding: 20px;
    background-color: #f0f0f0;
    border-radius: 10px;
}

.chord-display.active {
    background-color: #e0e0ff;
}

.controls {
    display: flex;
    justify-content: center;
    gap: 20px;
    margin: 20px 0;
}

.btn-large {
    padding: 15px 30px;
    font-size: 1.5rem;
    border-radius: 50%;
    border: none;
    cursor: pointer;
}

.btn-large.primary {
    background-color: #007bff;
    color: white;
}
"@

Set-Content -Path "Client/wwwroot/css/app.css" -Value $cssContent

# Mostrar mensaje de finalización
Write-Host "¡Estructura del proyecto creada exitosamente!" -ForegroundColor Green
Write-Host "Próximos pasos:" -ForegroundColor Yellow
Write-Host "1. Revisa todos los archivos creados" -ForegroundColor Yellow
Write-Host "2. Actualiza el contenido de los archivos con el código proporcionado anteriormente" -ForegroundColor Yellow
Write-Host "3. Ejecuta 'dotnet build' para verificar que todo esté correcto" -ForegroundColor Yellow
Write-Host "4. Ejecuta 'dotnet run' para probar la aplicación" -ForegroundColor Yellow