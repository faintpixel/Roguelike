using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DodongosQuest.Announcements
{
    public class Message
    {
        public string MessageText = "";
        public MessageTypes MessageType = MessageTypes.Other;

        public Message(string messageText, MessageTypes messageType)
        {
            MessageText = messageText;
            MessageType = messageType;
        }
    }
}
