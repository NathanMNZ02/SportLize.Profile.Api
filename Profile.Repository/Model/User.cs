using System;
using System.ComponentModel.DataAnnotations.Schema;
using SportLize.Profile.Api.Profile.Repository.Enumeration;

namespace SportLize.Profile.Api.Profile.Repository.Model
{
	public class User
	{
        public int Id { get; set; }
		public Actor Actor { get; set; }
		public string Nickname { get; set; } = string.Empty;
		public string Name { get; set; } = string.Empty;
		public string Surname { get; set; } = string.Empty;
		public string Password { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
        public DateTime DateOfBorn { get; set; }

        //Navigation Followers/Following 
        public List<User> Followers { get; set; } = new List<User>();

		//Navigation Post
		public List<Post> Posts { get; set; } = new List<Post>();
	}
}

