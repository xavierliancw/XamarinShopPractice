using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ShopPractice.DatabaseStuff;
using SQLite;
namespace ShopPractice
{
    /// <summary>
    /// Every conversation has a GUID identifying itself and a human readable name.
    /// </summary>
    public class ConversationDO : DatabaseObject
    {
        [PrimaryKey]
        public Guid GUID { get; set; }
        public String Name { get; set; }

        public ConversationDO() {}

        public ConversationDO(String name, Guid initialParticipantGUID, String initialParticipantName)
        {
            this.GUID = Guid.NewGuid();
            this.Name = name;
            ChatParticipantDO initialChatter = new ChatParticipantDO(initialParticipantGUID, initialParticipantName, true);
        }

        override public async Task SaveToDatabase()
        {
            var possDup = await TheDatabase.Connection.Table<ConversationDO>().Where(
                x => x.GUID == this.GUID
            ).ToListAsync();
            if (possDup.Count == 0)
            {
                await base.SaveToDatabase();
            }
            else if (possDup[0].Name != this.Name)
            {
                await TheDatabase.Connection.UpdateAsync(this);
            }
        }

        #region Database Methods

        public static async Task<List<ConversationDO>> GetAllConversations()
        {
            return await TheDatabase.Connection.Table<ConversationDO>().ToListAsync();
        }

        override public async Task<int> DeleteFromDatabase()
        {
            //Get all bubbles associated with this conversation and delete them
            var bubs = await TheDatabase.Connection.Table<ChatBubbleDO>().Where(
                x => x.ConversationGUID == this.GUID
            ).ToListAsync();
            foreach (var bub in bubs)
            {
                await TheDatabase.Connection.DeleteAsync(bub);
            }
            //Purge any chat participants that are no longer participating
            await ChatParticipantDO.Purge();

            //Finally, delete the conversation
            return await base.DeleteFromDatabase();
        }

        #endregion
    }
}
