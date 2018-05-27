using System;
using SQLite;
using System.Collections.Generic;
using ShopPractice.DatabaseStuff;
using System.Threading.Tasks;

namespace DatabaseStuff
{
    /// <summary>
    /// A database object that represents a single chat bubble.
    /// </summary>
    public class ChatBubbleDO : DatabaseObject
    {
        public Guid ConversationGUID { get; set; } //ID of the conversation this bubble belongs in
        public Guid UserGUID { get; set; }         //ID of the user who made this bubble
        public DateTime TimeStamp { get; set; }    //Time stamp of the time this bubble was created
        [MaxLength(1000)]
        public String Message { get; set; }        //What the user said
        [PrimaryKey]
        public Guid BubbleGUID { get; set; }       //A way to identify every bubble

        public ChatBubbleDO() {}

        public ChatBubbleDO(Guid conversationGUID, Guid userGUID, String message)
        {
            this.ConversationGUID = conversationGUID;
            this.UserGUID = userGUID;
            this.TimeStamp = DateTime.Now;
            this.BubbleGUID = Guid.NewGuid();
            this.Message = message;
        }

        override public async Task SaveToDatabase()
        {
            var possDup = await TheDatabase.Connection.Table<ChatBubbleDO>().Where(
                x => x.BubbleGUID == this.BubbleGUID
            ).ToListAsync();
            if (possDup.Count == 0)
            {
                await base.SaveToDatabase();
            }
            else
            {
                this.BubbleGUID = Guid.NewGuid();
                await this.SaveToDatabase();    //Keep repeating until a unique guid is obtained
            }
        }

        #region Database Operations

        /// <summary>
        /// Obtains all messages that belong to a certain conversation.
        /// </summary>
        /// <returns>The messages for a conversation.</returns>
        /// <param name="GUID">GUID.</param>
        public static async Task<List<ChatBubbleDO>> RetrieveMessagesForConversationAsync(Guid GUID)
        {
            return await TheDatabase.Connection.Table<ChatBubbleDO>().Where(
                x => x.ConversationGUID == GUID
            ).ToListAsync();
        }

        #endregion
    }
}
