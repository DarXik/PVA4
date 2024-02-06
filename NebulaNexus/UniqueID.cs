using System;
using System.Collections.Generic;

namespace NebulaNexus
{
    public static class UniqueId
    {
        public static readonly HashSet<int> Ids = new HashSet<int>();
        private static int _currentIteration = 1;

        public static int GenerateId()
        {
            // DateTime dt = DateTime.Now;
            // Random rnd = new Random();
            // var uniqueID = Math.Abs(rnd.Next(currentIteration, currentIteration + 5) * dt.Millisecond) / 10;
            var uniqueId = _currentIteration;
            // while (Ids.Contains(uniqueID))
            // {
            //     uniqueID = Math.Abs(rnd.Next(currentIteration, currentIteration + 5) * dt.Millisecond) / 10;
            // }

            Ids.Add(uniqueId);
            _currentIteration++;
            return uniqueId;
        }
    }
}