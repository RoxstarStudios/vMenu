using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace vMenu.Server.Services.DiscordEvents
{
    public class GuildDownloadCompleted
    {
        /*
        public static async Task<Task> OnOnGuildDownloadCompleted(DiscordClient client, GuildDownloadCompletedEventArgs args)
        {
            await DiscordService.Instance.Client.UpdateStatusAsync(new DiscordActivity
            {
                ActivityType = ActivityType.Playing,
                Name = "vMenu Revamped",
                StreamUrl = "https://trello.com/b/HpQdFX9J/vmenu-revamped-fivem"
            }, UserStatus.DoNotDisturb);

            await DiscordService.Instance.Client.UpdateCurrentUserAsync();

            //await DiscordService.Instance.Client.SetStatusAsync(UserStatus.DoNotDisturb);
            //await DiscordService.Instance.Client.SetGameAsync("FiveM", null, ActivityType.Playing);

            LoggingService.Instance.Success($"Discord Bot Has Started - {DiscordService.Instance.Client.CurrentUser.Username ?? "NOUSER"}");

            return Task.CompletedTask;
        }
        */
    }
}
