using System.IO;
using System.Reflection;
using Xunit;
using AoC2020.Days;

namespace AoC2020.Tests.Days
{
    public class Day11Tests
    {
        [Fact]
        public void Day11Test1()
        {
            // arrange
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            path = Path.GetFullPath(Path.Combine(path, "..", "..", ".."));

            var inputFile = Directory.GetFiles(path + @"/TestInput", "Day11_1.txt")[0];
            var day11 = new Day11(inputFile);

            // act
            var result1 = day11.PartOne();
            var result2 = day11.PartTwo();

            // assert
            Assert.Equal("37", result1);
            Assert.Equal("26", result2);
        }
    }
}