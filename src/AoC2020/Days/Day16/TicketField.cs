using System;

namespace AoC2020.Days.Day16Utils
{
    class TicketField
    {
        public string Name { get; set; }
        public int[] ValidRange1 { get; set; }
        public int[] ValidRange2 { get; set; }

        public TicketField(string name, int[] validRange1, int[] validRange2)
        {
            if (validRange1.Length != 2 || validRange2.Length != 2)
                throw new ArgumentException();
                
            Name = name;
            ValidRange1 = validRange1;
            ValidRange2 = validRange2;
        }
        
        internal bool ValidValue(int v)
        {
            return InRange(v, ValidRange1) || InRange(v, ValidRange2);
        }

        private bool InRange(int v, int[] validRange)
        {
            return v >= validRange[0] && v <= validRange[1];
        }
    }
}