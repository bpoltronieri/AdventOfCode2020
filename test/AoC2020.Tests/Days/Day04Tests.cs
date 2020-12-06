using System.IO;
using System.Reflection;
using Xunit;
using AoC2020.Days;

namespace AoC2020.Tests.Days
{
    public class Day04Tests
    {
        [Fact]
        public void Day04Test1()
        {
            // arrange
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            path = Path.GetFullPath(Path.Combine(path, "..", "..", ".."));

            var inputFile = Directory.GetFiles(path + @"/TestInput", "Day04_1.txt")[0];
            var day04 = new Day04(inputFile);

            // act
            var result1 = day04.PartOne();

            // assert
            Assert.Equal("2", result1);
        }

        [Fact]
        public void Day04Test2()
        {
            // arrange
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            path = Path.GetFullPath(Path.Combine(path, "..", "..", ".."));

            var inputFile = Directory.GetFiles(path + @"/TestInput", "Day04_2.txt")[0];
            var day04 = new Day04(inputFile);

            // act
            var result1 = day04.PartTwo();

            // assert
            Assert.Equal("0", result1);
        }
         [Fact]
        public void Day04Test3()
        {
            // arrange
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            path = Path.GetFullPath(Path.Combine(path, "..", "..", ".."));

            var inputFile = Directory.GetFiles(path + @"/TestInput", "Day04_3.txt")[0];
            var day04 = new Day04(inputFile);

            // act
            var result1 = day04.PartTwo();

            // assert
            Assert.Equal("4", result1);
        }
    }
}