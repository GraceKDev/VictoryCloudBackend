namespace VictoryCloudApi.Models
{
    public class CreateWritingDto
    {
        public string Title {get;set;} = string.Empty;
        public string Description {get;set;} = string.Empty;
        public string CoverUrl {get;set;} = string.Empty;
        public string[] Tags {get;set;} = [];
        public string[] Links {get;set;} = [];
        public string UploadedAt {get;set;} = string.Empty;

    }
    public class WritingChapterDto
    {
        public string ChapterTitle {get;set;} = string.Empty;
    }
    public class WritingChapterContentDto
    {
        public string ContentPosition {get;set;} = string.Empty;
        public string ContentType {get;set;} = string.Empty;
    }

     public class WritingChapterContentBlockDTO 
    {
        public string? Content { get; set; }
        public string? ImageUrl { get; set; }
        public string? AltText { get; set; }
    }

   
}
