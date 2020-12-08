using System.Collections.Generic;
using System.IO;

namespace AoC2020.Days
{
    public class Day05 : IDay
    {
        private string[] input;

        public Day05(string file)
        {
            LoadInput(file);
        }

        private void LoadInput(string file)
        {
            input = File.ReadAllLines(file);
        }

        private int SeatID(string seat)
        {
            var row = 0;
            var nRows = 128;
            for (var i = 0; i < 7; i++)
            {
                nRows = nRows/2;
                if (seat[i] == 'B') 
                    row += nRows;
            }

            var col = 0;
            var nCols = 8;
            for (var i = 0; i < 3; i++)
            {
                nCols = nCols/2;
                if (seat[i+7] == 'R')
                    col += nCols;
            }

            return row * 8 + col;
        }

        public string PartOne()
        {
            var maxSeatID = -1;
            foreach (var line in input)
            {
                var seatID = SeatID(line);
                if (seatID > maxSeatID)
                    maxSeatID = seatID;
            }
            return maxSeatID.ToString();
        }

        public string PartTwo()
        {
            var maxSeatID = 8*127 + 7;

            var seatIDs = new HashSet<int>();
            foreach (var seat in input)
                seatIDs.Add(SeatID(seat));

            for (var seatID = 0; seatID <= maxSeatID; seatID++)
            {
                if (!seatIDs.Contains(seatID) && seatIDs.Contains(seatID-1) && seatIDs.Contains(seatID+1))
                    return seatID.ToString();
            }
            return "";
        }
    }
}