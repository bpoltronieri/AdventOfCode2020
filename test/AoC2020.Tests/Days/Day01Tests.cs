using System.IO;
using System.Reflection;
using Xunit;
using AoC2020.Days;

namespace AoC2020.Tests.Days
{
    public class Day01Tests
    {
        [Fact]
        public void Day01Test1()
        {
            // arrange
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            path = Path.GetFullPath(Path.Combine(path, "..", "..", ".."));

            var inputFile = Directory.GetFiles(path + @"/TestInput", "Day01_1.txt")[0];
            var day01 = new Day01(inputFile);

            // act
            var result1 = day01.PartOne();
            var result2 = day01.PartTwo();

            // assert
            Assert.Equal("514579", result1);
            Assert.Equal("241861950", result2);
        }
    }
}