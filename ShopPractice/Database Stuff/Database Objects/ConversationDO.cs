using System;
using SQLite;
namespace ShopPractice
{
	/// <summary>
	/// Every conversation has a GUID identifying itself and a human readable name.
	/// </summary>
	public class ConversationDO
	{
		[PrimaryKey]
		public Guid GUID { get; set; }
		public String Name { get; set; }
	}
}
