using System.IO;
using System.Reflection;
using Xunit;
using AoC2020.Days;

namespace AoC2020.Tests.Days
{
    public class Day06Tests
    {
        [Fact]
        public void Day06Test1()
        {
            // arrange
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            path = Path.GetFullPath(Path.Combine(path, "..", "..", ".."));

            var inputFile = Directory.GetFiles(path + @"/TestInput", "Day06_1.txt")[0];
            var day06 = new Day06(inputFile);

            // act
            var result1 = day06.PartOne();

            // assert
            Assert.Equal("6", result1);
        }
        [Fact]
        public void Day06Test2()
        {
            // arrange
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            path = Path.GetFullPath(Path.Combine(path, "..", "..", ".."));

            var inputFile = Directory.GetFiles(path + @"/TestInput", "Day06_2.txt")[0];
            var day06 = new Day06(inputFile);

            // act
            var result1 = day06.PartOne();
            var result2 = day06.PartTwo();

            // assert
            Assert.Equal("11", result1);
            Assert.Equal("6", result2);
        }
    }
}