using System.IO;
using AoC2020.Days.Day20Utils;
using System.Collections.Generic;
using System.Linq;
using System;

namespace AoC2020.Days
{
    public class Day20 : IDay
    {
        private string[] input;
        private List<Tile> UnassignedTiles;
        private Tile[,] Arrangement;

        public Day20(string file)
        {
            LoadInput(file);
        }

        private void LoadInput(string file)
        {
            input = File.ReadAllLines(file);
        }

        private void LoadTiles()
        {
            UnassignedTiles = new List<Tile>();
            var i = 0;
            while (i < input.Length)
            {
                var tileID = int.Parse(input[i].Split()[1].TrimEnd(':'));
                var image = new string[10];
                for (var j = 0; j < 10; j++)
                    image[j] = input[i + 1 + j];
                UnassignedTiles.Add(new Tile(tileID, image));
                i += 12;
            }
            var squareLength = (int)Math.Sqrt(UnassignedTiles.Count);
            Arrangement = new Tile[squareLength, squareLength];
        }

        public string PartOne()
        {
            LoadTiles();
            FindArrangement();
            var L = Arrangement.GetLength(0) - 1;
            var answer = (Int64)Arrangement[0,0].ID * (Int64)Arrangement[0,L].ID * 
                         (Int64)Arrangement[L,0].ID * (Int64)Arrangement[L,L].ID;
            return answer.ToString();
        }

        // Can assume each tile only matches one other tile on each side
        // otherwise much harder as we could hit a dead end.
        private void FindArrangement()
        {
            var squareLength = Arrangement.GetLength(0);
            SetBottomLeftCorner();

            // find tiles going left to right, one row at a time
            for (var y = 0; y < squareLength; y++)
            {
                for (var x = 1; x < squareLength; x++)
                {
                    var rightTile = FindRightTile(Arrangement[y, x-1]); 
                    Arrangement[y,x] = rightTile;
                    UnassignedTiles.Remove(rightTile);
                }
                if (y != squareLength - 1)
                {
                    var upTile = FindUpTile(Arrangement[y, 0]);
                    Arrangement[y+1, 0] = upTile;
                    UnassignedTiles.Remove(upTile);
                }
            }
        }

        private Tile FindUpTile(Tile tile)
        {
            foreach (var tile2 in UnassignedTiles)
                for (var orient = 0; orient < 8; orient++)
                {
                    if (tile2.Bottom() == tile.Top())
                        return tile2;
                    tile2.SetNextOrientation();
                }
            throw new InvalidDataException();
        }

        private Tile FindRightTile(Tile tile)
        {
            foreach (var tile2 in UnassignedTiles)
                for (var orient = 0; orient < 8; orient++)
                {
                    if (tile2.Left() == tile.Right())
                        return tile2;
                    tile2.SetNextOrientation();
                }
            throw new InvalidDataException();
        }

        // Finds a corner tile oriented to be the bottom left tile and puts it in Arrangement[0,0]
        private void SetBottomLeftCorner()
        {
            Tile bottomLeftTile = null;
            foreach (var tile in UnassignedTiles)
            {
                for (var orient = 0; orient < 8; orient++)
                {
                    if (NoBottomMatch(tile) && NoLeftMatch(tile))
                    {
                        bottomLeftTile = tile;
                        Arrangement[0,0] = bottomLeftTile;
                        break;
                    }
                    tile.SetNextOrientation();
                }
                if (bottomLeftTile != null) break;
            }
            if (bottomLeftTile == null)
                throw new InvalidOperationException();
            else
                UnassignedTiles.Remove(bottomLeftTile);
        }

        private bool NoLeftMatch(Tile tile)
        {
            foreach (var tile2 in UnassignedTiles)
            {
                if (tile2 == tile) continue;
                for (var orient = 0; orient < 8; orient++)
                {
                    if (tile2.Right() == tile.Left())
                        return false;
                    tile2.SetNextOrientation();
                }
            }
            return true;
        }

        private bool NoBottomMatch(Tile tile)
        {
            foreach (var tile2 in UnassignedTiles)
            {
                if (tile2 == tile) continue;
                for (var orient = 0; orient < 8; orient++)
                {
                    if (tile2.Top() == tile.Bottom())
                        return false;
                    tile2.SetNextOrientation();
                }
            }
            return true;
        }

        public string PartTwo()
        {
            LoadTiles();
            FindArrangement();
            var Map = MakeSingleMapTile();

            for (var orient = 0; orient < 8; orient++)
            {
                var nSeaMonsters = FindSeaMonsters(Map);
                if (nSeaMonsters > 0) 
                    return (NHashes(Map) - (15 * nSeaMonsters)).ToString();
                Map.SetNextOrientation();
            }
            throw new InvalidDataException();
        }

        private int NHashes(Tile map)
        {
            return map.Image
                .Select(row => row.Count(x => x == '#'))
                .Sum();
        }

        private int FindSeaMonsters(Tile map)
        {  
            var nSeaMonsters = 0;
            var image = map.Image;
            for (var y = 0; y < image.Length - 2; y++)      // sea monster is 3 rows tall
                for (var x = 0; x < image.Length - 19; x++) // and 20 units long
                {
                    if (SeaMonsterRow1(image[y], x) && SeaMonsterRow2(image[y+1], x) && SeaMonsterRow3(image[y+2], x))
                        nSeaMonsters += 1;
                }
            return nSeaMonsters;
        }
        private bool SeaMonsterRow1(string row, int x)
        {
            return row[x+18] == '#';
        }

        private bool SeaMonsterRow2(string row, int x)
        {
            return row[x] == '#' &&
                   row[x+5] == '#' &&
                   row[x+6] == '#' &&
                   row[x+11] == '#' &&
                   row[x+12] == '#' &&
                   row[x+17] == '#' &&
                   row[x+18] == '#' &&
                   row[x+19] == '#';
        }

        private bool SeaMonsterRow3(string row, int x)
        {
            return row[x+1] == '#' &&
                   row[x+4] == '#' &&
                   row[x+7] == '#' &&
                   row[x+10] == '#' &&
                   row[x+13] == '#' &&
                   row[x+16] == '#';
        }

        // Uses Arrangement to make a single Map tile by removing all the tile borders and glueing them together.
        private Tile MakeSingleMapTile()
        {
            var nTiles = Arrangement.GetLength(0);
            var tileLengthWithoutBorders = Arrangement[0,0].Image[0].Length - 2;
            var mapLength = nTiles * tileLengthWithoutBorders;
            var newMap = new string[mapLength];

            for (var row = 0; row < mapLength; row++)
            {
                var tile_y = nTiles - 1 - (row / tileLengthWithoutBorders);
                var tileRow = (row % tileLengthWithoutBorders) + 1;
                var mapRow = "";
                for (var tile_x = 0; tile_x < nTiles; tile_x++)
                {
                    var tile = Arrangement[tile_y, tile_x];
                    var tileRowStr = tile.Image[tileRow];
                    var trimmedStr = tileRowStr.Substring(1, tileRowStr.Length - 2);
                    mapRow += trimmedStr;
                }
                newMap[row] = mapRow;
            }
            return new Tile(0, newMap);
        }
    }
}