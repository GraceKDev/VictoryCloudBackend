namespace VictoryCloudApi.Models
{
    public class Comic
    {
        public int ComicId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string CoverImageUrl { get; set; } = string.Empty;
        public string[] Tags { get; set; } = [];
        public ComicDetails Details { get; set; } = null!;
        public List<ComicChapter> Chapters { get; set; } = [];
        public string? Comments { get; set; }
        public string UploadedAt { get; set; } = string.Empty;
        public string UpdatedAt { get; set; } = string.Empty;
    }

    public class ComicDetails
    {
        public int ComicDetailsId { get; set; }
        public string Status { get; set; } = string.Empty;
        public int Year { get; set; }
        public string OriginalLanguage { get; set; } = string.Empty;
        public string ContentRating { get; set; } = string.Empty;
        public int ComicId { get; set; }

        public Comic Comic { get; set; } = null!;
    }

    public class ComicChapter
    {
        public int ComicChapterId { get; set; }
        public string ChapterTitle { get; set; } = string.Empty;
        public string[] Images { get; set; } = [];
        public int ComicId { get; set; }
        public string UploadedAt { get; set; } = string.Empty;
        public string UpdatedAt { get; set; } = string.Empty;
        public Comic Comic { get; set; } = null!;
    }
}