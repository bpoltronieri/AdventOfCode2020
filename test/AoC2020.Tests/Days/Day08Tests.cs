using System.IO;
using System.Reflection;
using Xunit;
using AoC2020.Days;

namespace AoC2020.Tests.Days
{
    public class Day08Tests
    {
        [Fact]
        public void Day08Test1()
        {
            // arrange
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            path = Path.GetFullPath(Path.Combine(path, "..", "..", ".."));

            var inputFile = Directory.GetFiles(path + @"/TestInput", "Day08_1.txt")[0];
            var day08 = new Day08(inputFile);

            // act
            var result1 = day08.PartOne();
            var result2 = day08.PartTwo();

            // assert
            Assert.Equal("5", result1);
            Assert.Equal("8", result2);
        }
    }
}