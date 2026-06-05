namespace VictoryCloudApi.Models
{
    public class CommentDto
    {
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public string Comment { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string Date { get; set; } = string.Empty;
        public int Likes { get; set; }
        public int? ParentId { get; set; }
    }
}