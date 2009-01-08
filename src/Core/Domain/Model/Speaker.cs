using Tarantino.Core.Commons.Model;

namespace CodeCampServer.Core.Domain.Model
{
	public class Speaker : PersistentObject
	{
		public virtual string SpeakerKey { get; set; }
		public virtual string FirstName { get; set; }
		public virtual string LastName { get; set; }
		public virtual string Company { get; set; }
		public virtual string EmailAddress { get; set; }
		public virtual string JobTitle { get; set; }
		public virtual string Bio { get; set; }
		public virtual string WebsiteUrl { get; set; }
	}
}