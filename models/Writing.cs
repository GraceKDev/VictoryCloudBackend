namespace VictoryCloudApi.Models
{
    public class Writing
    {
        public int WritingId {get;set;}
        public string Title {get;set;} = string.Empty;
        public string Description {get;set;} = string.Empty;
        public string[] Tags {get;set;} = [];
        public string[] Links {get;set;} = [];
        public string UploadedAt {get;set;} = string.Empty;
        public WritingChapter[] Chapters {get;set;} = [];
        public Comment[] Comments {get;set;} = [];
    }
    public class WritingChapter
    {
        public int WritingChapterId {get;set;}
        public int WritingParentId {get;set;}
        public string WritingChapterTitle {get;set;} = string.Empty;
        public WritingChapterContent[] WritingChapterContent {get;set;} = [];
    }
    public class WritingChapterContent
    {
        public int WritingChapterContentId {get;set;}
        public int WritingChapterParentId {get;set;}
        public string WritingContentPosition {get;set;} = string.Empty;
        public string WritingContentType {get;set;} = string.Empty;
    }
}