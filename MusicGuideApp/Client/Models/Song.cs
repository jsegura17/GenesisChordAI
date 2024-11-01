namespace MusicGuideApp.Client.Models
{
    public class Song
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Title { get; set; } = string.Empty;
        public string Artist { get; set; } = string.Empty;
        public int Tempo { get; set; }
        public string Key { get; set; } = string.Empty;
        public List<AudioTrack> AudioTracks { get; set; } = new();
        public List<Section> Sections { get; set; } = new();
    }
}