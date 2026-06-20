namespace VictoryCloudApi.Models
{
    public class CreateComicDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string CoverImageUrl { get; set; } = string.Empty;
        public string[] Tags { get; set; } = [];
        public ComicDetailsDto Details { get; set; } = null!;
        public List<ComicChapterDto> Chapters { get; set; } = [];
        public string UploadedAt {get;set;} = string.Empty;
        public string UpdatedAt {get;set;} = string.Empty;
        public string? Comments { get; set; }
    }

    public class ComicDetailsDto
    {
        public string Status { get; set; } = string.Empty;
        public int Year { get; set; }
        public string OriginalLanguage { get; set; } = string.Empty;
        public string ContentRating { get; set; } = string.Empty;
    }

    public class ComicChapterDto
    {
        public string ChapterTitle { get; set; } = string.Empty;
        public string UploadedAt {get;set;} = string.Empty;
        public string UpdatedAt {get;set;} = string.Empty;
        public string[] Images { get; set; } = [];
    }
}
