using System;

namespace AoC2020
{
    interface IDay
    {
        void LoadInput(string file);

        string PartOne();
        string PartTwo();
    }
}