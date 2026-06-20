namespace VictoryCloudApi.Models
{
    public class CreateWritingDto
    {
        public string Title {get;set;} = string.Empty;
        public string Description {get;set;} = string.Empty;
        public string CoverUrl {get;set;} = string.Empty;
        public string[] Tags {get;set;} = [];
        public string[] Links {get;set;} = [];
        public WritingChapterDto[] Chapters  {get;set;} = [];
        public string UploadedAt {get;set;} = string.Empty;
        public string UpdatedAt {get;set;} = string.Empty;

    }
    public class WritingChapterDto
    {
        public string ChapterTitle {get;set;} = string.Empty;
        public string UploadedAt {get;set;} = string.Empty;
        public string UpdatedAt {get;set;} = string.Empty;
        public WritingChapterContentDto[] Content {get;set;} = [];
    }
    public class WritingChapterContentDto
    {
        public int ContentPosition {get;set;}
        public string ContentType {get;set;} = string.Empty;
        public WritingChapterContentBlockDTO? Content {get;set;} = null;
    }

     public class WritingChapterContentBlockDTO 
    {
        public string? Content { get; set; }
        public string? ImageUrl { get; set; }
        public string? AltText { get; set; }
    }

   
}
