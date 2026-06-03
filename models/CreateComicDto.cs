namespace VictoryCloudApi.Models
{
    public class CreateComicDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string CoverImageUrl { get; set; } = string.Empty;
        public string[] Tags { get; set; } = [];
        public ComicDetailsDto Details { get; set; } = null!;
        public List<ChapterDto> Chapters { get; set; } = [];
        public string? Comments { get; set; }
    }

    public class ComicDetailsDto
    {
        public string Status { get; set; } = string.Empty;
        public int Year { get; set; }
        public string OriginalLanguage { get; set; } = string.Empty;
        public string ContentRating { get; set; } = string.Empty;
    }

    public class ChapterDto
    {
        public string ChapterTitle { get; set; } = string.Empty;
        public string[] Images { get; set; } = [];
    }
}
