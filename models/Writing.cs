namespace VictoryCloudApi.Models
{
    public class Writing
    {
        public int WritingId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string[] Tags { get; set; } = [];
        public string[] Links { get; set; } = [];
        public string UploadedAt { get; set; } = string.Empty;
        public List<WritingChapter> Chapters { get; set; } = [];
        public List<Comment>? Comments { get; set; } = [];
    }
    public class WritingChapter
    {
        public int WritingChapterId { get; set; }
        public int WritingId { get; set; }
        public string WritingChapterTitle { get; set; } = string.Empty;
        public List<WritingChapterContent> WritingChapterContent { get; set; } = [];
    }
    public class WritingChapterContent
    {
        public int WritingChapterContentId { get; set; }
        public int WritingChapterId { get; set; }
        public int WritingContentPosition { get; set; }
        public string WritingContentType { get; set; } = string.Empty;
        public List<WritingChapterContentBlock> WritingContentBlock { get; set; } = [];
    }
    public class WritingChapterContentBlock
    {
        public int WritingChapterContentBlockId { get; set; }
        public int WritingChapterContentId { get; set; }
        public string? WritingContentBlockContent { get; set; } = string.Empty;
        public string? WritingContentBlockImageUrl { get; set; } = string.Empty;
        public string? WritingContentBlockAltText { get; set; } = string.Empty;
    }
}