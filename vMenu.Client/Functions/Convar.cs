using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CitizenFX.Core;

using static CitizenFX.Core.Native.API;


namespace vMenu.Client.Functions
{
    internal class Convar : BaseScript
    {

        /// <summary>
        /// Get a boolean setting.
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        public static bool GetSettingsBool(string setting, bool defaultval = false)
        {
            return GetConvar(setting.ToString(), defaultval.ToString()) == "true";
        }

        /// <summary>
        /// Get an integer setting.
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        public static int GetSettingsInt(string setting, int defaultint = -1)
        {
            var convarInt = GetConvarInt(setting.ToString(), defaultint);
            if (convarInt == -1)
            {
                if (int.TryParse(GetConvar(setting.ToString(), defaultint.ToString()), out var convarIntAlt))
                {
                    return convarIntAlt;
                }
            }
            return convarInt;
        }

        /// <summary>
        /// Get a float setting.
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        public static float GetSettingsFloat(string setting, float defaultfloat = -1.0f)
        {
            if (float.TryParse(GetConvar(setting.ToString(), defaultfloat.ToString()), out var result))
            {
                return result;
            }
            return -1f;
        }

        /// <summary>
        /// Get a string setting.
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        public static string GetSettingsString(string setting, string defaultValue = null)
        {
            var value = GetConvar(setting.ToString(), defaultValue ?? "");
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }
            return value;
        }
    }
}

