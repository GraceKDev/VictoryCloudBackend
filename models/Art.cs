namespace VictoryCloudApi.Models
{
    public class Art
    {
        public int ArtId {get;set;}
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public string[] Tags { get; set; } = [];
        public string[] Links { get; set; } = [];
        public string UploadedAt {get;set;} = string.Empty;
        public string UpdatedAt {get;set;} = string.Empty;
    }
}