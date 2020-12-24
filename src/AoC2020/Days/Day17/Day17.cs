using System.Collections.Generic;
using System.IO;
using AoC2020.Days.Day17Utils;
using System.Linq;
using System;

namespace AoC2020.Days
{
    public class Day17 : IDay
    {
        private string[] input;
        private Dictionary<Point, bool> Map;

        public Day17(string file)
        {
            Map = new Dictionary<Point, bool>();
            LoadInput(file);
        }

        private void LoadInput(string file)
        {
            input = File.ReadAllLines(file);
        }

        private void LoadMap(int dimension)
        {
            for (var y = 0; y < input.Length; y++)
                for (var x = 0; x < input[y].Length; x++)
                {
                    Point point;
                    if (dimension == 3) 
                        point = new Point(x, y, 0);
                    else if (dimension == 4) 
                        point = new Point(x, y, 0, 0);
                    else
                        throw new ArgumentException();
                    Map.Add(point, input[y][x] == '#');
                }
        }

        private bool ActivePoint(Point point, Dictionary<Point, bool> map)
        {
            return map.ContainsKey(point) && map[point];
        }

        public string PartOne()
        {
            LoadMap(3);
            for (var i = 0; i < 6; i++)
                RunCycle();
            return Map.Values.Count(b => b).ToString();
        }

        private void RunCycle()
        {
            var newMap = new Dictionary<Point, bool>(Map);
            var paddingPoints = new List<Point>();
            foreach (var point in Map.Keys)
                UpdatePoint(point, newMap, paddingPoints);
            foreach (var point in paddingPoints)
                UpdatePoint(point, newMap, null);
            Map = newMap;
        }

        private void UpdatePoint(Point point, Dictionary<Point, bool> newMap, List<Point> paddingPoints)
        {
            var nActiveNeighbours = NActiveNeighbours(point, paddingPoints);
            if (ActivePoint(point, Map) && 
                nActiveNeighbours != 2 && nActiveNeighbours != 3)
                newMap[point] = false;
            else if (!ActivePoint(point, Map) && nActiveNeighbours == 3)
                newMap[point] = true;
        }

        private int NActiveNeighbours(Point point, List<Point> paddingPoints)
        {
            var count = 0;
            var pointActive = ActivePoint(point, Map);
            foreach (var neighbour in point.Neighbours())
            {
                if (paddingPoints != null && pointActive && !ActivePoint(neighbour, Map))
                    paddingPoints.Add(neighbour); // neighbour might become active this turn so needs to be added to map
                
                if (ActivePoint(neighbour, Map))
                    count += 1;
            }
            return count;
        }

        public string PartTwo()
        {
            LoadMap(4);
            for (var i = 0; i < 6; i++)
                RunCycle();
            return Map.Values.Count(b => b).ToString();
        }
    }
}