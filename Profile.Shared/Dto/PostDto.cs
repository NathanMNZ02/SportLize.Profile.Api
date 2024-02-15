namespace SportLize.Profile.Api.Profile.Shared.Dto
{
    public class PostWriteDto
    {
        public string Media64 { get; set; } = string.Empty;
        public int Like { get; set; }
        public DateTime PubblicationDate { get; set; }
        public string Description { get; set; } = string.Empty;
    }

    public class PostReadDto
    {
        public int Id { get; set; }
        public string Media64 { get; set; } = string.Empty;
        public int Like { get; set; }
        public DateTime PubblicationDate { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
