using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CitizenFX.Core;
using CitizenFX.Core.Native;

using FxEvents;

using Logger;

using ScaleformUI.Menu;

using Newtonsoft.Json;

using vMenu.Client.Events;
using vMenu.Client.Functions;

using vMenu.Client.KeyMappings;
using vMenu.Client.Settings;
using vMenu.Shared.Enums;
using vMenu.Shared.Objects;

using static CitizenFX.Core.Native.API;

namespace vMenu.Client
{
    public class Main : BaseScript
    {
        public static Main Instance { get; private set; }
        public static PlayerList PlayerList;
        public static List<KeyValuePair<OnlinePlayersCB, string>> OnlinePlayers = new List<KeyValuePair<OnlinePlayersCB, string>>();

        public static MenuAlign MenuAlign = MenuAlign.Left;

        public static List<UIMenu> Menus = new List<UIMenu>();

        private static string JsonData = LoadResourceFile(GetCurrentResourceName(), "config/MenuSettings.jsonc") ?? "{}";
        private static vMenu.Client.Settings.MenuSettings.MenuSettingJson JsonSettings = JsonConvert.DeserializeObject<vMenu.Client.Settings.MenuSettings.MenuSettingJson>(JsonData);

        public static string CustomFontName = "";
        public static int CustomFontId = 0;

        public static long DuiObject = 0;
        public static MenuBannerObject MenuBanner = new MenuBannerObject()
        {
            BannerTitle = JsonSettings.BannerTitle,

            TextureDictionary = JsonSettings.TextureDictionary,
            TextureName = JsonSettings.TextureName,

            // This will be used instead of the texture set above (only when set) - Link must be a static image or gif (.png, .jpg, .gif, etc...) //
            // The image/gif MUST be 288x130px //
            // COMMENT OUT THE LINE BELOW IF NOT USING //
            //TextureUrl = JsonSettings.TextureUrl
        };

        public Main()
        {
            PlayerList = Players;

            EventDispatcher.Initalize("vMenu:Inbound", "vMenu:Outbound", "vMenu:Signature");

            RegisterFontFile("Quicksand");

            CustomFontId = RegisterFontId("Quicksand");
            CustomFontName = "Quicksand";

            Instance = this;

            _ = vMenu.Client.Exports.Vehicle.Instance;

            Init();
        }

        public void Init()
        {
            _ = Languages.Instance;
            _ = MenuEvents.Instance;
        }

        internal void AddEventHandler(string eventName, Delegate @delegate)
        {
            EventHandlers[eventName] += @delegate;
        }

        internal void AttachTick(Func<Task> task)
        {
            Tick += task;
        }

        internal void DetachTick(Func<Task> task)
        {
            Tick -= task;
        }

        public bool isPlayerDead = false;
        internal void Export(string val, Delegate obj)
        {
            Exports.Add(val, obj);
        }
        [Tick]
        public async Task OnPlayerDeadCheckTick()
        {
            if (MenuFunctions.Instance == null)
            {
                await Delay(100);
                return;
            }

            if (Game.Player.Character.IsDead)
            {
                if (!isPlayerDead)
                {
                    isPlayerDead = true;
                    MenuFunctions.Instance.RestartMenu();
                    
                }
            }
            else
            {
                if (isPlayerDead)
                {
                    isPlayerDead = false;
                    MenuFunctions.Instance.RestartMenu();
                    
                }
            }

            await Delay(100);
        }

    }
}
