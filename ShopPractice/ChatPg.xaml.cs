using System;
using System.Collections.Generic;
using PCLStorage;

using Xamarin.Forms;

namespace ShopPractice
{
    public partial class ChatPg : ContentPage
    {
        //Properties

        Guid chatID;

        //Private Properties

        private IFile chatFile;

        //Construction

        public ChatPg(Guid chatID)
        {
            InitializeComponent();
            this.chatID = chatID;
            InitChatFile();
        }

        //Private Methods

        private void storeMessage()
        {
            
        }

        private void InitChatFile()
        {
            IFolder chatsFolder = FileStorageSvc.GetFolderAsync("Chats").Result;
            chatFile = FileStorageSvc.GetFileAsync(chatID.ToString() + ".txt",chatsFolder).Result;
        }
    }
}
