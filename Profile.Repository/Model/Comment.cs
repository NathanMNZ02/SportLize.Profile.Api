using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportLize.Profile.Api.Profile.Repository.Model
{
	public class Comment
	{
        public int Id { get; set; }
		public string Text { get; set; } = string.Empty;
		public int Like { get; set; }
		public DateTime PubblicationDate { get; set; }


        //Navigation Post
        public required int PostId { get; set; }
        public required Post Post { get; set; }
    }
}

