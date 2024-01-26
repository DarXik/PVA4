using System;
using System.Collections.Generic;

namespace NebulaNexus
{
    public static class UniqueID
    {
        public static HashSet<int> IDs = new HashSet<int>();
        private static int currentIteration = 10;

        public static int GenerateID()
        {
            DateTime dt = DateTime.Now;
            Random rnd = new Random();
            var uniqueID = Math.Abs(rnd.Next(currentIteration, currentIteration + 5) * dt.Millisecond) / 10;

            while (IDs.Contains(uniqueID))
            {
                uniqueID = Math.Abs(rnd.Next(currentIteration, currentIteration + 5) * dt.Millisecond) / 10;
            }

            IDs.Add(uniqueID);
            currentIteration++;
            return uniqueID;
        }
    }
}