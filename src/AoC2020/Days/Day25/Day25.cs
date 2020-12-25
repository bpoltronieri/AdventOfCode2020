using System;
using System.IO;

namespace AoC2020.Days
{
    public class Day25 : IDay
    {
        private int CardPublicKey;

        private int DoorPublicKey;

        public Day25(string file)
        {
            LoadInput(file);
        }

        private void LoadInput(string file)
        {
            var input = File.ReadAllLines(file);
            CardPublicKey = int.Parse(input[0]);
            DoorPublicKey = int.Parse(input[1]);
        }

        public string PartOne()
        {
            int? cardLoopSize = null;
            int? doorLoopSize = null;

            var tryLoopSize = 0;
            while (cardLoopSize == null && doorLoopSize == null)
            {
                tryLoopSize += 1;
                var key = TransformSubjet(7, tryLoopSize);
                if (key == CardPublicKey) cardLoopSize = tryLoopSize;
                if (key == DoorPublicKey) doorLoopSize = tryLoopSize;
            }
            Int64 encryptionKey;
            if (cardLoopSize != null)
                encryptionKey = TransformSubjet(DoorPublicKey, (int)cardLoopSize);
            else
                encryptionKey = TransformSubjet(CardPublicKey, (int)doorLoopSize);

            return encryptionKey.ToString();
        }

        public string PartTwo()
        {
            return "No part two on Christmas day!";
        }

        private Int64 TransformSubjet(int subject, int loopSize)
        {
            Int64 value = 1;
            for (var i = 0; i < loopSize; i++)
                value = (value * subject) % 20201227;
            return value;
        }
    }
}