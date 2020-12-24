using System.IO;
using System.Collections.Generic;
using System.Linq;
using AoC2020.Days.Day23Utils;

namespace AoC2020.Days
{
    public class Day24 : IDay
    {
        private string[] input;

        private Dictionary<HexTile, bool> TileColours; // true means black, false or not in dict means white.

        public Day24(string file)
        {
            LoadInput(file);
        }

        private void LoadInput(string file)
        {
            input = File.ReadAllLines(file);
        }

        public string PartOne()
        {
            SetTileColours();
            return TileColours.Values.Count(b => b).ToString();
        }

        private void SetTileColours()
        {
            TileColours = new Dictionary<HexTile, bool>();
            var RefTile = new HexTile(0, 0);
            foreach (var line in input)
            {
                var lastTile = RefTile;
                foreach (var direction in DecodeInstruction(line))
                {
                    var nextTile = new HexTile(lastTile, direction);
                    lastTile = nextTile;
                }
                FlipTile(lastTile);
            }
        }

        private void FlipTile(HexTile tile)
        {
            if (TileColours.ContainsKey(tile))
                TileColours[tile] = !TileColours[tile];
            else
                TileColours[tile] = true;
        }

        private IEnumerable<string> DecodeInstruction(string line)
        {
            for (var i = 0; i < line.Length; i++)
            {
                if (line[i] == 'e' || line[i] == 'w')
                    yield return line[i].ToString();
                else // must be two digit direction, e.g. se
                {
                    yield return line.Substring(i, 2);
                    i += 1;
                }
            }
        }

        public string PartTwo()
        {
            SetTileColours();
            
            for (var i = 0; i < 100; i++)
            {
                var newTileColours = new Dictionary<HexTile, bool>();
                var paddingTiles = new List<HexTile>();

                foreach (var tile in TileColours.Keys)
                    UpdateTile(tile, newTileColours, paddingTiles);
                foreach (var tile in paddingTiles)
                {
                    newTileColours[tile] = false;
                    UpdateTile(tile, newTileColours, paddingTiles);
                }
                TileColours = newTileColours;
            }

            return TileColours.Values.Count(b => b).ToString();
        }

        private void UpdateTile(HexTile tile, Dictionary<HexTile, bool> newTileColours, List<HexTile> paddingTiles)
        {
            var nBlackNeighbours = GetNBlackNeighbours(tile, paddingTiles);
            if (BlackTile(tile))
                newTileColours[tile] = !(nBlackNeighbours == 0 || nBlackNeighbours > 2);
            else
                newTileColours[tile] = nBlackNeighbours == 2;
        }

        private bool BlackTile(HexTile tile)
        {
            return TileColours.ContainsKey(tile) && TileColours[tile];
        }

        private int GetNBlackNeighbours(HexTile tile, List<HexTile> paddingTiles)
        {
            var tileBlack = BlackTile(tile);
            var neighbourDirns = "eseswwnwne";
            var nBlack = 0;

            foreach (var dir in DecodeInstruction(neighbourDirns))
            {
                var neighbour = new HexTile(tile, dir);

                if (tileBlack && !TileColours.ContainsKey(neighbour))
                    paddingTiles.Add(neighbour); // this white tile might change this round so needs to be added to map

                if (BlackTile(neighbour))
                    nBlack += 1;
            }

            return nBlack;
        }
    }
}