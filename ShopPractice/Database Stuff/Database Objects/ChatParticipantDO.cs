using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ShopPractice.DatabaseStuff;
using SQLite;
namespace DatabaseStuff
{
    /// <summary>
    /// A chat participant has a GUID to identify a chatter, a human-readable name to identify chatter to other humans, and a
    /// boolean indicating if a chatter owns the device.
    /// </summary>
    public class ChatParticipantDO : DatabaseObject
    {
        [PrimaryKey]
        public Guid GUID { get; set; }
        public String Name { get; set; }
        public Boolean Owner { get; set; }

        public ChatParticipantDO() {}

        public ChatParticipantDO(Guid participantGUID, String participantName, bool deviceOwner)
        {
            this.GUID = participantGUID;
            this.Name = participantName;
            this.Owner = deviceOwner;
        }

        #region Database Methods

        override public async Task SaveToDatabase()
        {
            //Check if user exists in the database
            var possDuplicates = await TheDatabase.Connection.Table<ChatParticipantDO>().Where(
                x => x.GUID == this.GUID
            ).ToListAsync();

            //If user doesn't exist, save new record
            if (possDuplicates.Count == 0)
            {
                await base.SaveToDatabase();
            } //If user's name has changed, update the record
            else if (possDuplicates[0].Name != this.Name)
            {
                await TheDatabase.Connection.UpdateAsync(this);
            } //Otherwise just don't save
        }

        /// <summary>
        /// Deletes chat participants who do not have any chat bubbles in any of the database's chat history.
        /// </summary>
        /// <returns>The number of chat participants deleted.</returns>
        public static async Task<int> Purge()
        {
            var messages = await TheDatabase.Connection.Table<ChatBubbleDO>().ToListAsync();
            var chatters = await TheDatabase.Connection.Table<ChatParticipantDO>().ToListAsync();
            int chattersDeleted = 0;

            //Create a checklist of chatters to purge
            List<Tuple<ChexBox, ChatParticipantDO>> purgeList = new List<Tuple<ChexBox, ChatParticipantDO>>();
            foreach (var chatter in chatters)
            {
                ChexBox check = new ChexBox()
                {
                    IsChecked = true //All chatters start out as being purge-able
                };
                purgeList.Add(new Tuple<ChexBox, ChatParticipantDO>(check, chatter));
            }
            //Mark chatters with chat history in the db to not be purged
            foreach (var message in messages)
            {
                foreach (var item in purgeList)
                {
                    if (message.UserGUID == item.Item2.GUID)
                    {
                        item.Item1.IsChecked = false;
                    }
                }
            }
            //Purge chatters marked true
            foreach (var item in purgeList)
            {
                if (item.Item1.IsChecked)
                {
                    chattersDeleted += await item.Item2.DeleteFromDatabase();
                }
            }
            return chattersDeleted;
        }

        public static async Task<String> GetName(Guid GUID)
        {
            var results = await TheDatabase.Connection.Table<ChatParticipantDO>().Where(
                x => x.GUID == GUID
            ).ToListAsync();
            return results[0].Name;
        }

        #endregion

        private class ChexBox
		{
            public bool IsChecked;
		}
    }
}
