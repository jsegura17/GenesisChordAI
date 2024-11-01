using MusicGuideApp.Shared.Enums;

namespace MusicGuideApp.Client.Models
{
    public class Section
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public SectionType Type { get; set; }
        public double StartTime { get; set; }
        public double EndTime { get; set; }
        public List<LyricLine> Lines { get; set; } = new();
    }
}