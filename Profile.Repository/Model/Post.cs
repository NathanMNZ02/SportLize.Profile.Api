using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportLize.Profile.Api.Profile.Repository.Model
{
	public class Post
	{
        public int Id { get; set; }
		public byte[] Media { get; set; } = [];
		public int Like { get; set; }
		public DateTime PubblicationDate { get; set; }
		public string Description { get; set; } = string.Empty;

        //Navigation User
        public int UserId { get; set; }
        public required User User { get; set; }


        //Navigation Comments
        public List<Comment> Comments { get; set; } = [];
    }
}

