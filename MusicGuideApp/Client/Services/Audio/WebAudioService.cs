using Microsoft.JSInterop;
using MusicGuideApp.Shared.Enums;

namespace MusicGuideApp.Client.Services.Audio
{
    public class WebAudioService : IWebAudioService, IAsyncDisposable
    {
        private readonly IJSRuntime _jsRuntime;
        private IJSObjectReference? _audioModule;
        private DotNetObjectReference<WebAudioService>? _dotNetRef;

        public event Action<PlaybackState>? OnPlaybackStateChanged;
        public event Action<double>? OnTimeUpdate;

        public WebAudioService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task InitializeAsync()
        {
            try
            {
                _audioModule = await _jsRuntime.InvokeAsync<IJSObjectReference>(
                    "import", "./js/webAudioManager.js");
                _dotNetRef = DotNetObjectReference.Create(this);

                Console.WriteLine("Inicializando módulo de audio...");
                await _audioModule.InvokeVoidAsync("initialize", _dotNetRef);
                Console.WriteLine("Módulo de audio inicializado correctamente");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error inicializando servicio de audio: {ex}");
                throw;
            }
        }

        public async Task LoadTrackAsync(string trackId, string url)
        {
            if (_audioModule is null) throw new InvalidOperationException("Audio module not initialized");

            try
            {
                Console.WriteLine($"Cargando pista {trackId} desde {url}");
                await _audioModule.InvokeVoidAsync("loadTrack", trackId, url);
                Console.WriteLine($"Pista {trackId} cargada correctamente");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error cargando pista {trackId}: {ex}");
                throw;
            }
        }

        public async Task PlayAsync(string trackId)
        {
            if (_audioModule is null) throw new InvalidOperationException("Audio module not initialized");
            await _audioModule.InvokeVoidAsync("play", trackId);
        }

        public async Task PauseAsync()
        {
            if (_audioModule is null) throw new InvalidOperationException("Audio module not initialized");
            await _audioModule.InvokeVoidAsync("pause");
        }

        public async Task StopAsync()
        {
            if (_audioModule is null) return;
            await _audioModule.InvokeVoidAsync("stop");
        }

        public async Task SetVolumeAsync(string trackId, float volume)
        {
            if (_audioModule is null) return;
            await _audioModule.InvokeVoidAsync("setVolume", trackId, volume);
        }

        public async Task SetTimeAsync(double time)
        {
            if (_audioModule is null) return;
            await _audioModule.InvokeVoidAsync("setTime", time);
        }

        public async Task<double> GetCurrentTimeAsync()
        {
            if (_audioModule is null) return 0;
            return await _audioModule.InvokeAsync<double>("getCurrentTime");
        }

        public async Task<PlaybackState> GetPlaybackStateAsync()
        {
            if (_audioModule is null) return PlaybackState.Stopped;
            string state = await _audioModule.InvokeAsync<string>("getPlaybackState");
            return Enum.Parse<PlaybackState>(state);
        }

        [JSInvokable]
        public void UpdatePlaybackState(string state)
        {
            var playbackState = Enum.Parse<PlaybackState>(state);
            OnPlaybackStateChanged?.Invoke(playbackState);
        }

        [JSInvokable]
        public void UpdateTime(double time)
        {
            OnTimeUpdate?.Invoke(time);
        }

        public async ValueTask DisposeAsync()
        {
            if (_audioModule is not null)
            {
                await _audioModule.DisposeAsync();
            }
            _dotNetRef?.Dispose();
        }
    }
}
