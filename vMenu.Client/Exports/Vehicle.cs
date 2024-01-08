using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CitizenFX.Core;
using CitizenFX.Core.Native;

using static CitizenFX.Core.Native.API;

namespace vMenu.Client.Exports
{
    public class Vehicle
    {
        private static readonly object _padlock = new();
        private static Vehicle _instance;

        private Vehicle()
        {
            Main.Instance.Export("GetGearRatios", new Func<int, List<double>>(GetGearRatios));
            Main.Instance.Export("GetGearRatioByGear", new Func<int, int, double>(GetGearRatioByGear));
            Main.Instance.Export("GetGearDriveRatio", new Func<float, float, float, float>(GetGearDriveRatio));
            Debug.WriteLine("Vehicle Exports Initialized");
        }

        internal static Vehicle Instance
        {
            get
            {
                lock (_padlock)
                {
                    return _instance ??= new Vehicle();
                }
            }
        }

        private static double FinalGearRatio = 0.9;
        private static double ReverseGearRatio = -0.3;
        private static double FirstGearRatio = 0.3;

        private static double ProgressionFactor = 1.1;

        public static List<double> GetGearRatios(int Vehicle)
        {
            int NumGears = GetVehicleHighGear(Vehicle);
            List<double> GearRatios = new List<double> ( new double[NumGears+1] );

            GearRatios[0] = 1.0 / ReverseGearRatio;
            GearRatios[NumGears] = FinalGearRatio;

            if(NumGears==1)
            {
                return GearRatios;
            }

            GearRatios[1] = 1.0 / FirstGearRatio;

            if(NumGears==2)
                return GearRatios;

            double baseRatio = Math.Pow((1.0 / (Math.Pow(ProgressionFactor, 0.5 * (NumGears - 1.0) * (NumGears - 2.0)))) * GearRatios[1], 1.0 / (NumGears - 1));
            for (int i = 2; i <= NumGears; i++)
            {
                GearRatios[i] = GearRatios[NumGears] * Math.Pow(baseRatio, (double)NumGears - i) * Math.Pow(ProgressionFactor, 0.5 * (NumGears - i) * (NumGears - i - 1.0));
            }
            return GearRatios;
        }
        public static double GetGearRatioByGear(int Vehicle, int Gear)
        {
            if (GetVehicleHighGear(Vehicle) >= Gear)
            {
                if (!(Gear < 0))
                {
                    return GetGearRatios(Vehicle)[Gear];
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }
        public static float GetGearDriveRatio(float Ratio, float DriveForce, float ClutchMult)
        {      
            return (float)Math.Min(1.0f, 2.0f * ClutchMult) * DriveForce * (float)Ratio;
        }
        public static CitizenFX.Core.Vehicle GetVehicle(bool lastVehicle = false)
        {
            if (lastVehicle)
            {
                return Game.PlayerPed.LastVehicle;
            }
            else
            {
                if (Game.PlayerPed.IsInVehicle())
                {
                    return Game.PlayerPed.CurrentVehicle;
                }
            }
            return null;
        }
    }
}