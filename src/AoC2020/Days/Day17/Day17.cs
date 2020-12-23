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
            PadMapWithInactiveNeighbours(Map);
        }

        // Pads map by adding inactive neighbours to all active points,
        // unless its neighbours are already in the map
        // could be sped up a lot... should only have to consider points
        // on the edge of the map but I'm not sure how to do that given that
        // I haven't padded evenly in every direction. Probably would be
        // faster to just do that.
        // Or instead of a dictionary I should just use a 2D array.
        // Given we know the number of turns we'll run, we know the maximum
        // size the map will need to be.
        private void PadMapWithInactiveNeighbours(Dictionary<Point, bool> map)
        {
            foreach (var point in map.Keys.Where(k => ActivePoint(k, map)).ToArray())
                foreach (var neighbour in point.Neighbours())
                {
                    if (!map.ContainsKey(neighbour))
                        map.Add(neighbour, false);
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
            foreach (var point in Map.Keys)
            {
                var nActiveNeighbours = NActiveNeighbours(point, Map);
                if (ActivePoint(point, Map) && 
                    nActiveNeighbours != 2 && nActiveNeighbours != 3)
                    newMap[point] = false;
                else if (!ActivePoint(point, Map) && nActiveNeighbours == 3)
                    newMap[point] = true;
            }
            Map = newMap;
            PadMapWithInactiveNeighbours(Map);
        }

        private int NActiveNeighbours(Point point, Dictionary<Point, bool> map)
        {
            return point.Neighbours().Count(n => ActivePoint(n, Map));
        }

        // Part Two is very slow due to PadMapWithInactiveNeighbours, see note there for solutions.
        public string PartTwo()
        {
            LoadMap(4);
            for (var i = 0; i < 6; i++)
                RunCycle();
            return Map.Values.Count(b => b).ToString();
        }
    }
}