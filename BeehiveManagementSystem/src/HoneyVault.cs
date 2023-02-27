using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BeehiveManagementSystem.src
{
    internal static class HoneyVault
    {
        private static float honey = 25f;
        private static float nectar = 100f;

        public const float NECTAR_CONVERSION_RATIO = .19f;
        public const float LOW_LEVEL_WARNING = 10f;

        /// <summary>
        /// Converts nectar amount to honey
        /// If amount bigger than nectar amount converts all nectar what have into honey
        /// </summary>
        /// <param name="amount"> nectar amount to convert</param>
        public static void ConvertNectarToHoney(float amount)
        {
            if (amount < 0) new Exception("amount must be greater than zero");

            if (amount > nectar)
            {
                honey = amount * NECTAR_CONVERSION_RATIO;
                nectar = 0;
            }
            else
            {
                nectar -= amount;
                honey += amount * NECTAR_CONVERSION_RATIO;
            }

        }

        /// <summary>
        /// How much bees consuming honey.
        /// </summary>
        /// <param name="amount">How much to consume from honey</param>
        /// <returns>true if honey was consumed by amount</returns>
        public static bool ConsumeHoney(float amount)
        {
            if (honey >= amount)
            {
                honey -= amount;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Adds nectar amount to voult
        /// </summary>
        /// <param name="amount"></param>
        public static void CollectNectar(float amount)
        {
            if (amount > 0f) nectar += amount;
        }

        public static string StatusReport
        {
            get
            {
                string status = $"{honey:0.0} units of honey\n" + $"{nectar:0.0} units of nectar\n";
                string warnings = "";
                if (honey < LOW_LEVEL_WARNING) warnings += "LOW HONEY - ADD A HONEY MANUFACTURER\n";
                if (nectar < LOW_LEVEL_WARNING) warnings += "LOW NECTAR - ADD A NECTAR COLLECTOR\n";
                
                return status + warnings;
            }
        }
    }
}
