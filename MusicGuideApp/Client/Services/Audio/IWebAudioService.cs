using MusicGuideApp.Shared.Enums;

namespace MusicGuideApp.Client.Services.Audio
{
    public interface IWebAudioService
    {
        Task InitializeAsync();
        Task LoadTrackAsync(string trackId, string url); 
        Task PlayAsync(string trackId);
        Task PauseAsync();
        Task StopAsync();
        Task SetVolumeAsync(string trackId, float volume);
        Task SetTimeAsync(double time);
        Task<double> GetCurrentTimeAsync();
        Task<PlaybackState> GetPlaybackStateAsync();
        event Action<PlaybackState> OnPlaybackStateChanged;
        event Action<double> OnTimeUpdate;
    }
}
