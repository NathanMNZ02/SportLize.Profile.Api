namespace SportLize.Profile.Api.Profile.Repository.Model
{
    public class TransactionalOutbox
    {
        public long Id { get; set; }
        public string Table { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}
