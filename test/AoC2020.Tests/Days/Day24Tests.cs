using System.IO;
using System.Reflection;
using Xunit;
using AoC2020.Days;

namespace AoC2020.Tests.Days
{
    public class Day24Tests
    {
        [Fact]
        public void Day24Test1()
        {
            // arrange
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            path = Path.GetFullPath(Path.Combine(path, "..", "..", ".."));

            var inputFile = Directory.GetFiles(path + @"/TestInput", "Day24_1.txt")[0];
            var day24 = new Day24(inputFile);

            // act
            var result1 = day24.PartOne();
            var result2 = day24.PartTwo();

            // assert
            Assert.Equal("10", result1);
            Assert.Equal("2208", result2);
        }
    }
}