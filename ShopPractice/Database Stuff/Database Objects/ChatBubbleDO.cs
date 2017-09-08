using System;
using SQLite;
using System.Collections.Generic;
using ShopPractice.DatabaseStuff;
using System.Threading.Tasks;
namespace ShopPractice
{
    /// <summary>
    /// A database object that represents a single chat bubble.
    /// </summary>
    public class ChatBubbleDO
    {
        [PrimaryKey]
        public Guid ConversationGUID { get; set; } //ID of the conversation this bubble belongs in
        public Guid UserGUID { get; set; }         //ID of the user who made this bubble
        public DateTime TimeStamp { get; set; }    //Time stamp of the time this bubble was created
        public String Message { get; set; }        //What the user said

        /// <summary>
        /// Obtains all messages that belong to a certain conversation.
        /// </summary>
        /// <returns>The bubbles for conversation.</returns>
        /// <param name="GUID">GUID.</param>
        public async Task<List<ChatBubbleDO>> GetBubblesForConversationAsync(Guid GUID)
        {
            return await TheDatabase.Connection.Table<ChatBubbleDO>().Where(
                x => x.ConversationGUID == GUID
            ).ToListAsync();
        }
    }
}
