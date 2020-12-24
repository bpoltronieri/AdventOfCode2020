using System.IO;
using System.Reflection;
using Xunit;
using AoC2020.Days;

namespace AoC2020.Tests.Days
{
    public class Day22Tests
    {
        [Fact]
        public void Day22Test1()
        {
            // arrange
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            path = Path.GetFullPath(Path.Combine(path, "..", "..", ".."));

            var inputFile = Directory.GetFiles(path + @"/TestInput", "Day22_1.txt")[0];
            var day22 = new Day22(inputFile);

            // act
            var result1 = day22.PartOne();
            var result2 = day22.PartTwo();

            // assert
            Assert.Equal("306", result1);
            Assert.Equal("291", result2);
        }
    }
}