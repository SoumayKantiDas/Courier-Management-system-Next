using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Courier_Management_system_Next.Utilities
{
    public class TrackingNumberGenerator
    {
        private static readonly Random random = new Random();
        private const string Characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        public static string GenerateTrackingNumber()
        {
            int length = 10; // Define the length of the tracking number
            char[] trackingNumber = new char[length];

            for (int i = 0; i < length; i++)
            {
                trackingNumber[i] = Characters[random.Next(Characters.Length)];
            }

            return new string(trackingNumber);
        }
    }
}