namespace SportLize.Profile.Api.Profile.Shared.Dto
{
    public class CommentWriteDto
    {
        public string Text { get; set; } = string.Empty;
        public int Like { get; set; }
        public DateTime PubblicationDate { get; set; }
    }
    public class CommentReadDto
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public int Like { get; set; }
        public DateTime PubblicationDate { get; set; }
    }
}
