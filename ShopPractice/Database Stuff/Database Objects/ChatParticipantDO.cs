using System;
using SQLite;
namespace ShopPractice
{
	/// <summary>
	/// A chat participant has a GUID to identify a chatter, a human-readable name to identify chatter to other humans, and a
	/// boolean indicating if a chatter owns the device.
	/// </summary>
	public class ChatParticipantDO
	{
		[PrimaryKey]
		public Guid GUID { get; set; }
		public String Name { get; set; }
		public Boolean Owner { get; set; }
	}
}
