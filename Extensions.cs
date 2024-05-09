using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rocket.API;
using Rocket.Core.Plugins;
using Rocket.Unturned.Player;
using Rocket.Unturned.Events;
using SDG.Unturned;
using UnityEngine;
using System.Runtime.CompilerServices;
using System;

namespace UCGS.RichChat
{
    public static class Extensions
    {
        public static bool ChekClientGroups(this UnturnedPlayer client, out Group group)
        {
            foreach (Group g in Plugin.Config.Groups)
            {
                if (client.HasPermission("chat." + g.Name.ToLower()))
                {
                    group = g;
                    return true;
                }
            }
            group = null;
            return false;
        }
        public static void ChatPrint(this SteamPlayer steamPlayer, string message, SteamPlayer fromPlayer)
        {
            string IconUrl = (fromPlayer != null) ? UnturnedPlayer.FromSteamPlayer(fromPlayer).SteamProfile.AvatarIcon.ToString() : "";
            ChatManager.serverSendMessage(message, Color.white, fromPlayer, steamPlayer, EChatMode.SAY, IconUrl, true);
        }
        public static void WriteConsole(string message) 
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("[");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("RichChat");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("] ");
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
