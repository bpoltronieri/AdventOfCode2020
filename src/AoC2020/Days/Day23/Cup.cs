namespace AoC2020.Days.Day23Utils
{
    internal class Cup
    {
        public int Label { get; }
        public Cup Next { get; set; }

        public Cup(int label)
        {
            Label = label;
        }
    }
}