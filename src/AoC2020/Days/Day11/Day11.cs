using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC2020.Days
{
    public class Day11 : IDay
    {
        private char[][] Seats;

        public Day11(string file)
        {
            LoadInput(file);
        }

        private void LoadInput(string file)
        {
           Seats = File.ReadAllLines(file).Select(x => x.ToCharArray()).ToArray();
        }     

        // Updates map following cellular automaton rules until equilibrium.
        // Returns number of occupied seats.
        public string PartOne()
        {
            var currentSeats = Seats;
            var newSeats = Seats;
            do
            {
                currentSeats = newSeats;
                newSeats = UpdateSeatMap1(currentSeats);
            } while (!EqualMaps(newSeats, currentSeats));

            return newSeats.SelectMany(x => x).Count(c => c == '#').ToString();
        }

        private bool EqualMaps(char[][] map1, char[][] map2)
        {
            for (var i = 0; i < map1.Length; i++)
            {
                var row1 = map1[i];
                var row2 = map2[i];
                if (!row1.SequenceEqual(row2))
                    return false;
            }
            return true;
        }

        private char[][] UpdateSeatMap1(char[][] currentSeats)
        {
            var newSeats = currentSeats.Select(x => (char[]) x.Clone()).ToArray();
            for (var row_i = 0; row_i < currentSeats.Length; row_i++)
            {
                var row = currentSeats[row_i];
                for (var seat_i = 0; seat_i < row.Length; seat_i++)
                {
                    var seat = row[seat_i];
                    var nOccupiedNbours = CountAdjacentOccupied(currentSeats, row_i, seat_i);

                    if (seat == 'L' && nOccupiedNbours == 0)
                        newSeats[row_i][seat_i] = '#';
                    else if (seat == '#' && nOccupiedNbours >= 4)
                        newSeats[row_i][seat_i] = 'L';
                }
            }
            return newSeats;
        }

        private int CountAdjacentOccupied(char[][] seats, int row_i, int seat_i)
        {
            var directions = new int[] {-1, 0, 1};
            var nOccupied = 0;
            foreach (var di in directions)
                foreach (var dj in directions)
                {
                    var new_i = row_i + di;
                    var new_j = seat_i + dj;
                    if (InvalidNeighbour(seats, di, dj, new_i, new_j)) continue;
                                          
                    if (seats[new_i][new_j] == '#')
                        nOccupied += 1;
                }
            return nOccupied;
        }

        // checks whether we are checking an invalid neighbour: 
        // either checking the original seat itself (i.e. not a neighbour) or out of bounds
        private bool InvalidNeighbour(char[][] seats, int di, int dj, int new_i, int new_j)
        {
            return (di == 0 && dj == 0) || 
                    new_i < 0 || new_j < 0 ||
                    new_i >= seats.Length || new_j >= seats[0].Length;
        }

        // Same as part one but now count visible occupied neighbours rather than just adjacent ones.
        public string PartTwo()
        {
            var currentSeats = Seats;
            var newSeats = Seats;
            do
            {
                currentSeats = newSeats;
                newSeats = UpdateSeatMap2(currentSeats);
            } while (!EqualMaps(newSeats, currentSeats));

            return newSeats.SelectMany(x => x).Count(c => c == '#').ToString();
        }

        private char[][] UpdateSeatMap2(char[][] currentSeats)
        {
            var newSeats = currentSeats.Select(x => (char[]) x.Clone()).ToArray();
            for (var row_i = 0; row_i < currentSeats.Length; row_i++)
            {
                var row = currentSeats[row_i];
                for (var seat_i = 0; seat_i < row.Length; seat_i++)
                {
                    var seat = row[seat_i];
                    var nOccupiedNbours = CountVisibleOccupied(currentSeats, row_i, seat_i);

                    if (seat == 'L' && nOccupiedNbours == 0)
                        newSeats[row_i][seat_i] = '#';
                    else if (seat == '#' && nOccupiedNbours >= 5)
                        newSeats[row_i][seat_i] = 'L';
                }
            }
            return newSeats;
        }

        // Similar to CountAdjacentOccupied but looks at the first visible seat in each direction.
        private int CountVisibleOccupied(char[][] seats, int row_i, int seat_i)
        {
            var directions = new int[] {-1, 0, 1};
            var nOccupied = 0;
            foreach (var di in directions)
                foreach (var dj in directions)
                {
                    var factor = 1;
                    while (true)
                    {
                        var new_i = row_i + di * factor;
                        var new_j = seat_i + dj * factor;
                        if (InvalidNeighbour(seats, di, dj, new_i, new_j)) break;
     
                        if (seats[new_i][new_j] == '#')
                            nOccupied += 1;

                        if (seats[new_i][new_j] != '.') break;
                        factor += 1;
                    }
                }
            return nOccupied;
        }
    }
}