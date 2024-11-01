namespace MusicGuideApp.Client.Models
{
    public class LyricLine
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public double Time { get; set; }
        public string Text { get; set; } = string.Empty;
        public string Chord { get; set; } = string.Empty;
        public double Duration { get; set; }
    }
}