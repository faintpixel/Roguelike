using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace DodongosQuest.Announcements
{
    public sealed class Announcer
    {
        static Announcer instance = null;
        static readonly object padlock = new object();

        public delegate void AnnouncementEvent(Message message);
        public event AnnouncementEvent Announcement;

        public Announcer()
        {
        }

        public static Announcer Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new Announcer();
                    }
                    return instance;
                }
            }
        }

        public void Announce(string message, MessageTypes messageType)
        {
            Debug.WriteLine(message);
            if(Announcement != null)
                Announcement(new Message(message, messageType));
        }
    }
}
