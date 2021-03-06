using System.IO;
using System.Reflection;
using Xunit;
using AoC2020.Days;

namespace AoC2020.Tests.Days
{
    public class Day03Tests
    {
        [Fact]
        public void Day03Test1()
        {
            // arrange
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            path = Path.GetFullPath(Path.Combine(path, "..", "..", ".."));

            var inputFile = Directory.GetFiles(path + @"/TestInput", "Day03_1.txt")[0];
            var day03 = new Day03(inputFile);

            // act
            var result1 = day03.PartOne();
            var result2 = day03.PartTwo();

            // assert
            Assert.Equal("7", result1);
            Assert.Equal("336", result2);
        }
    }
}