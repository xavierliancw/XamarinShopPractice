using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DatabaseStuff;
using ShopPractice;

namespace Services
{
    //TODO: Things not considered:
    //What happens when the database is full?

    //Well wait a sec, none of this really matters, this is just a dummy driver so I learn how to make Xamarin UI. There's no need to communicate
    //with any servers or handle those edge cases. I'll do that in a separate practice app then (one that involves .net).
    public static class ChatSvc
    {
        static Guid? _currentChatGuid;

        /// <summary>
        /// Creates a new blank conversation and sets the current active conversation to this new one.
        /// </summary>
        public static void CreateNewConversation()
        {
            _currentChatGuid = Guid.NewGuid();

            ConversationDO newConvo = new ConversationDO()
            {
                GUID = new Guid(_currentChatGuid.ToString()),
                Name = "New Message"
            };
            Task.Run(() => newConvo.SaveToDatabase());
        }

        /// <summary>
        /// Deletes the current active conversation, making it null.
        /// </summary>
        /// <returns>An asynchrounous task operation.</returns>
        /// <param name="conversationGUID">Conversation GUID.</param>
        public async static Task DeleteConversationAsync(Guid conversationGUID)
        {
            var convos = await ConversationDO.GetAllConversationsAsync();
            foreach (var convo in convos)
            {
                if (convo.GUID == conversationGUID)
                {
                    await convo.DeleteFromDatabase();
                    _currentChatGuid = null;
                    return;
                }
            }
        }

        //Handle an outgoing message
        /// <summary>
        /// Sends a message from the current device. If there's no current conversation, then nothing happens.
        /// </summary>
        /// <param name="message">The message string that is getting sent.</param>
        public static void SendMessage(String message)
        {
            //Check if there's no current conversation first
            if (_currentChatGuid == null)
            {
                return;
            }
            //TODO handle network calls to send message to the server

            //Save to the database
            ChatBubbleDO newBubble = new ChatBubbleDO((Guid)_currentChatGuid, Gimme.TheDevice.GUID, message);
            Task.Run(() => newBubble.SaveToDatabase());
        }

        //Handle an incoming message
        public async static void ReceiveMessageAsync(String randomMsg, Guid fromPerson, String personName, Guid inConversation)
        {
            //Create a conversation object (saving it will update an existing one if it exists in the db)
            ConversationDO newConvo = new ConversationDO()
            {
                GUID = inConversation,
                Name = personName
            };
            await newConvo.SaveToDatabase();

            //Search for the person in the db
            var dbPerson = await ChatParticipantDO.GetName(fromPerson);
            ChatParticipantDO newPers = new ChatParticipantDO(fromPerson, personName, false);
            await newPers.SaveToDatabase(); //Handles decision to update or to add new record

            //Save the Message
            ChatBubbleDO newMsg = new ChatBubbleDO(inConversation, fromPerson, randomMsg);
            await newMsg.SaveToDatabase();
        }

        public async static Task AddPersonToConversationAsync(Guid newPerson)
        {
            //Contact the server and submit the person's guid
            //Rename the conversation (I could probably just keep the object as a field of this class
            var convos = await ConversationDO.GetAllConversationsAsync();
            foreach (var convo in convos)
            {
                if (convo.GUID == _currentChatGuid)
                {
                    //Find the person in the db
                    var person = await ChatParticipantDO.GetName(newPerson);
                    convo.Name = convo.Name + ", " + person;
                    await convo.SaveToDatabase();
                    return;
                }
            }
        }

        /// <summary>
        /// Sets the current conversation.
        /// </summary>
        /// <param name="conversationGUID">Conversation GUID.</param>
        public static void SetCurrentConversation(Guid conversationGUID)
        {
            _currentChatGuid = conversationGUID;
        }

        /// <summary>
        /// Retrieves all messages from the current conversation.
        /// </summary>
        /// <returns>A list of ConversationMessage objects.</returns>
        public async static Task<List<ConversationMessage>> RetrieveActiveConversationContentsAsync()
        {
            List<ConversationMessage> ret = new List<ConversationMessage>();

            if (_currentChatGuid == null)
            {
                return ret;
            }
            //Retrieve all messages from the db
            var messages = await ChatBubbleDO.RetrieveMessagesForConversationAsync((Guid)_currentChatGuid);

            //Build the list of conversation messages to return
            foreach (var message in messages)
            {
                //Determine if the message's author is the user of the current device
                Boolean isOwner = false || message.UserGUID == Gimme.TheDevice.GUID;

                //Append to the list
                ConversationMessage aBubble = new ConversationMessage()
                {
                    Author = await ChatParticipantDO.GetName(message.UserGUID),
                    DeviceOwner = isOwner,
                    Message = message.Message,
                    TimeStamp = message.TimeStamp
                };
                ret.Add(aBubble);
            }
            return ret;
        }

        /// <summary>
        /// Retrieves the list of all conversations.
        /// </summary>
        /// <returns>A list of conversation tuples where Item1 is the conversation name and Item2 is the conversation GUID.</returns>
        public async static Task<List<Tuple<String, Guid>>> GetAllConversationsAsync()
        {
            var list = await ConversationDO.GetAllConversationsAsync();
            List<Tuple<String, Guid>> ret = new List<Tuple<string, Guid>>();
            foreach (var thing in list)
            {
                ret.Add(new Tuple<string, Guid>(thing.Name, thing.GUID));
            }
            return ret;
        }

        /// <summary>
        /// Obtains the ChatSvc's current active conversation' GUID.
        /// </summary>
        /// <returns>The current conversation GUID.</returns>
        public static Guid? GetActiveConversationGUID()
        {
            return _currentChatGuid;
        }

        /// <summary>
        /// Gets the name of the active conversation.
        /// </summary>
        /// <returns>The active conversation name. Null if not found.</returns>
        public async static Task<String> GetActiveConversationNameAsync()
        {
            var convos = await ConversationDO.GetAllConversationsAsync();
            foreach (var convo in convos)
            {
                if (convo.GUID == _currentChatGuid)
                {
                    return convo.Name;
                }
            }
            return null;
        }
    }
}
