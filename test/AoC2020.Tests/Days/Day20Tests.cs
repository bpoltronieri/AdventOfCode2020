using System.IO;
using System.Reflection;
using Xunit;
using AoC2020.Days;

namespace AoC2020.Tests.Days
{
    public class Day20Tests
    {
        [Fact]
        public void Day20Test1()
        {
            // arrange
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            path = Path.GetFullPath(Path.Combine(path, "..", "..", ".."));

            var inputFile = Directory.GetFiles(path + @"/TestInput", "Day20_1.txt")[0];
            var day20 = new Day20(inputFile);

            // act
            var result1 = day20.PartOne();
            var result2 = day20.PartTwo();

            // assert
            Assert.Equal("20899048083289", result1);
            Assert.Equal("273", result2);
        }
    }
}