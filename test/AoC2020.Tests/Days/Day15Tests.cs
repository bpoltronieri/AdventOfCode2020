using System.IO;
using System.Reflection;
using Xunit;
using AoC2020.Days;

namespace AoC2020.Tests.Days
{
    public class Day15Tests
    {
        [Fact]
        public void Day15Test1()
        {
            // arrange
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            path = Path.GetFullPath(Path.Combine(path, "..", "..", ".."));

            var inputFile = Directory.GetFiles(path + @"/TestInput", "Day15_1.txt")[0];
            var day15 = new Day15(inputFile);

            // act
            var result1 = day15.PartOne();
            var result2 = day15.PartTwo();

            // assert
            Assert.Equal("436", result1);
            Assert.Equal("175594", result2);
        }
    }
}