using System;
namespace Services
{
    public class ConversationMessage
    {
        public String Message { get; set; }
        public DateTime TimeStamp { get; set; }
        public String Author { get; set; }
        public Boolean DeviceOwner { get; set; }
    }
}
