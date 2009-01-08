using CodeCampServer.Core.Domain.Model.Enumerations;
using Tarantino.Core.Commons.Model;

namespace CodeCampServer.Core.Domain.Model
{
	public class Session : PersistentObject
	{
		public virtual Track Track { get; set; }
		public virtual TimeSlot TimeSlot { get; set; }
		public virtual Speaker Speaker { get; set; }
		public virtual Conference Conference { get; set; }
		public virtual string RoomNumber { get; set; }
		public virtual string Title { get; set; }
		public virtual string Abstract { get; set; }
		public virtual SessionLevel Level { get; set; }
		public virtual string MaterialsUrl { get; set; }
		public virtual string SessionKey { get; set; }
	}
}