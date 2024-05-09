using Rocket.API;
using System;
using System.Collections.Generic;
using Rocket.Unturned.Events;
using Rocket.Unturned.Player;

namespace UCGS.RichChat
{

    public class Config : IRocketPluginConfiguration
    {
        public bool ChatModePrefix = true;
        public string GlobalPrefix = "[G]";
        public string GlobalPrefixColor = "yellow";
        public string AreaPrefix = "[A]";
        public string AreaPrefixColor = "yellow";
        public string GroupPrefix = "[G]";
        public string GroupPrefixColor = "yellow";
        public List<Group> Groups;
        public void LoadDefaults()
        {
            Groups = new List<Group>()
            {
                new Group("default", "[Player]", "red", "blue", "yellow"),
                new Group("vip", "[VIP]", "cyan", "pink", "red")
            };
        }
    }
}

