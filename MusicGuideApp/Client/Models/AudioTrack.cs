using MusicGuideApp.Shared.Enums;

namespace MusicGuideApp.Client.Models
{
    public class AudioTrack
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public TrackType Type { get; set; }
        public float Volume { get; set; } = 1.0f;
    }
}