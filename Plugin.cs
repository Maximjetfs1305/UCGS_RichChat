using System;
using Rocket.Core.Plugins;
using Rocket.Unturned.Player;
using SDG.Unturned;
using UnityEngine;

namespace UCGS.RichChat
{
    public class Plugin : RocketPlugin<Config>
    {
        public static Plugin Instance;
        public static Config Config;
        string PluginVersion = "1.0.0";
        protected override void Load()
        {
            Instance = this;
            Config = base.Configuration.Instance;
            ChatManager.onChatted += ClientChatted;
            Extensions.WriteConsole("Plugin loaded successfully.");
            Extensions.WriteConsole("Plugin version : " + PluginVersion);
            Extensions.WriteConsole("Groups loaded : " + Config.Groups.Count);
            Extensions.WriteConsole("Author :  Maximjetfs");
            Extensions.WriteConsole("github.com/Maximjetfs1305");
        }

        protected override void Unload()
        {
            Instance = null;
            Config = null;
            ChatManager.onChatted -= ClientChatted;
            Extensions.WriteConsole("Plugin unloaded.");
        }

        private void ClientChatted(SteamPlayer sendClient, EChatMode chatMode, ref Color color, ref bool isRich, string message, ref bool isVisible)
        {
            UnturnedPlayer client = UnturnedPlayer.FromSteamPlayer(sendClient);
            if (message.StartsWith("/")) return;
            if (client.ChekClientGroups(out Group group))
            {
                isVisible = false;
                if (chatMode == EChatMode.GLOBAL)
                {
                    foreach (SteamPlayer toClient in Provider.clients)
                    {
                        string ModePrefix = Config.ChatModePrefix ? "<color=" + Config.GlobalPrefixColor + ">" + Config.GlobalPrefix + "</color> " : "";
                        string PermissionTag = "<color=" + group.ColorTag + ">" + group.Tag + "</color> ";
                        string ClientName = "<color=" + group.ColorName + ">" + toClient.playerID.characterName + "</color> : ";
                        string Message = "<color=" + group.ColorText + ">" + message + "</color>";
                        toClient.ChatPrint(ModePrefix + PermissionTag + ClientName + Message, sendClient);
                    }
                }
                if (chatMode == EChatMode.LOCAL)
                {
                    foreach (SteamPlayer toClient in Provider.clients)
                    {
                        if (!(toClient.player == null) && (toClient.player.transform.position - sendClient.player.transform.position).sqrMagnitude < 16384f)
                        {
                            string ModePrefix = Config.ChatModePrefix ? "<color=" + Config.GlobalPrefixColor + ">" + Config.GlobalPrefix + "</color> " : "";
                            string PermissionTag = "<color=" + group.ColorTag + ">" + group.Tag + "</color> ";
                            string ClientName = "<color=" + group.ColorName + ">" + toClient.playerID.characterName + "</color> : ";
                            string Message = "<color=" + group.ColorText + ">" + message + "</color>";
                            toClient.ChatPrint(ModePrefix + PermissionTag + ClientName + Message, sendClient);
                        }
                    }

                }
                if (chatMode == EChatMode.GROUP)
                {
                    foreach (SteamPlayer toClient in Provider.clients)
                    {
                        if (!(toClient.player == null) && toClient.player.quests.isMemberOfSameGroupAs(sendClient.player))
                        {
                            string ModePrefix = Config.ChatModePrefix ? "<color=" + Config.GlobalPrefixColor + ">" + Config.GlobalPrefix + "</color> " : "";
                            string PermissionTag = "<color=" + group.ColorTag + ">" + group.Tag + "</color> ";
                            string ClientName = "<color=" + group.ColorName + ">" + toClient.playerID.characterName + "</color> : ";
                            string Message = "<color=" + group.ColorText + ">" + message + "</color>";
                            toClient.ChatPrint(ModePrefix + PermissionTag + ClientName + Message, sendClient);
                        }
                    }
                }
            }
            else
            {
                Rocket.Core.Logging.Logger.Log("[RichChat] Client \" " + sendClient.playerID.steamID.ToString() + " \" has no chat settings.", ConsoleColor.Red);
                return;
            }
        }
    }
}
