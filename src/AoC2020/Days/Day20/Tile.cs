using System;
using System.Collections.Generic;

namespace AoC2020.Days.Day20Utils
{
    class Tile
    {
        public int ID { get; }
        public string[] Image { get; set; }
        private bool Reflected = false;
        private int Rotated = 0; // can be 0, 1, 2, 3

        public Tile( int id, string[] image )
        {
            ID = id;
            Image = image;
        }

        public string Bottom()
        {
            return Image[Image.Length - 1];
        }
        public string Top()
        {
            return Image[0];
        }
        public string Left()
        {
            var leftString = "";
            foreach (var line in Image)
                leftString += line[0];
            return leftString;
        }
        public string Right()
        {
            var rightString = "";
            foreach (var line in Image)
                rightString += line[line.Length - 1];
            return rightString;
        }
        public void SetNextOrientation()
        {
            var needReflection = Rotated == 3;
            Rotate();
            if (needReflection) Reflect();
        }

        private void Reflect()
        {
            for (var i = 0; i < Image.Length; i++)
                Image[i] = Reverse(Image[i]);
            Reflected = !Reflected;
        }

        private string Reverse( string s )
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse( charArray );
            return new string( charArray );
        }

        private void Rotate()
        {
            var squareLength = Image.Length;
            var newImage = new string[squareLength];
            for (var i = 0; i < squareLength; i++) // build up new string for each row
            {
                var newRow = "";
                for (var j = 0; j < squareLength; j++)
                    newRow += Image[squareLength - 1 - j][i];
                newImage[i] = newRow;
            }
            Image = newImage;
            Rotated += 1;
            Rotated %= 4;
        }
    }
}