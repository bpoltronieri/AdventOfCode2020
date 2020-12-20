using System;
using System.IO;
using System.Numerics;

namespace AoC2020.Days
{
    public class Day12 : IDay
    {
        private string[] input;
        private Vector<int> position;
        private Vector<int> direction;
        private Vector<int> waypoint;

        public Day12(string file)
        {
            LoadInput(file);
        }

        private void LoadInput(string file)
        {
           input = File.ReadAllLines(file);
        }     

        private Vector<int> NewVec(int x, int y)
        {
            var VecArr = new int[] {x, y, 0, 0, 0, 0, 0, 0};
            var Vec = new Vector<int>(VecArr); 
            return Vec;
        }

        public string PartOne()
        {
            position = NewVec(0,0);
            direction = NewVec(1,0);
            waypoint = NewVec(10,1);

            foreach (var instruction in input)
                MoveShip(instruction);

            return ManhattanDistanceFromOrigin().ToString();
        }

        private void MoveShip(string instructionStr)
        {
            var instruction = instructionStr[0];
            var value = int.Parse(instructionStr.Substring(1));
            
            switch (instruction)
            {
                case 'N':
                    position += NewVec(0,1) * value;
                    break;
                case 'S':
                    position += NewVec(0,-1) * value;
                    break;
                case 'E':
                    position += NewVec(1,0) * value;
                    break;
                case 'W':
                    position += NewVec(-1,0) * value;
                    break;
                case 'L':
                    {
                    var n90DegTurns = value/90;
                    for (var i = 0; i < n90DegTurns; i++)
                        direction = NewVec(-direction[1], direction[0]);
                    break;
                    }
                case 'R':
                    {
                    var n90DegTurns = value/90;
                    for (var i = 0; i < n90DegTurns; i++)
                        direction = NewVec(direction[1], -direction[0]);
                    break;
                    }
                case 'F':
                    position += direction * value;
                    break;
            }
        }

        private int ManhattanDistanceFromOrigin()
        {
            return Math.Abs(position[0]) + Math.Abs(position[1]);
        }

        public string PartTwo()
        {
            position = NewVec(0,0);
            direction = NewVec(1,0);
            waypoint = NewVec(10,1);

            foreach (var instruction in input)
                MoveWaypoint(instruction);

            return ManhattanDistanceFromOrigin().ToString();
        }

        private void MoveWaypoint(string instructionStr)
        {
            var instruction = instructionStr[0];
            var value = int.Parse(instructionStr.Substring(1));
            
            switch (instruction)
            {
                case 'N':
                    waypoint += NewVec(0,1) * value;
                    break;
                case 'S':
                    waypoint += NewVec(0,-1) * value;
                    break;
                case 'E':
                    waypoint += NewVec(1,0) * value;
                    break;
                case 'W':
                    waypoint += NewVec(-1,0) * value;
                    break;
                case 'L':
                    {
                    var n90DegTurns = value/90;
                    for (var i = 0; i < n90DegTurns; i++)
                        waypoint = NewVec(-waypoint[1], waypoint[0]);
                    break;
                    }
                case 'R':
                    {
                    var n90DegTurns = value/90;
                    for (var i = 0; i < n90DegTurns; i++)
                        waypoint = NewVec(waypoint[1], -waypoint[0]);
                    break;
                    }
                case 'F':
                    position += waypoint * value;
                    break;
            }
        }
    }
}