using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using CitizenFX.Core;

using vMenu.Server.Functions;
using static vMenu.Server.Services.DiscordEvents.GuildDownloadCompleted;
using vMenu.Server.Services.DiscordEvents;

namespace vMenu.Server.Services
{
    public class DiscordService
    {
        private static readonly object _padlock = new();
        private static DiscordService _instance;

        //public DiscordClient Client;

        private DiscordService()
        {
            Debug.WriteLine("[vMenu] Initializing Discord Service");
            MainAsync();
        }

        public async void MainAsync()
        {
            var token = Convar.GetSettingsString("vmenu_discord_bot_token", "none");

            if (token != "none" && !string.IsNullOrEmpty(token))
            {
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                /*
                Client = new DiscordClient(new DiscordConfiguration()
                {
                    Token = token,
                    TokenType = TokenType.Bot,
                    Intents = DiscordIntents.AllUnprivileged | DiscordIntents.MessageContents,
                    Proxy = new WebProxy()
                    {
                        UseDefaultCredentials = true,
                    },
                    MessageCacheSize = 50,
                    MinimumLogLevel = LogLevel.Debug,
                    AlwaysCacheMembers = true,
                    LogUnknownEvents = true,
                    AutoReconnect = false,
                });
                */

                //Client.Log += Log;

                //Client.GuildDownloadCompleted += OnOnGuildDownloadCompleted;

                //await Client.ConnectAsync();
            }
            else
            {
                LoggingService.Instance.Warning("Discord Token Not Set - Bot Not Started");
            }
        }

        internal static DiscordService Instance
        {
            get
            {
                lock (_padlock)
                {
                    return _instance ??= new DiscordService();
                }
            }
        }
    }
}
