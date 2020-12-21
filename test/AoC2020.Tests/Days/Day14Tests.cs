using System.IO;
using System.Reflection;
using Xunit;
using AoC2020.Days;

namespace AoC2020.Tests.Days
{
    public class Day14Tests
    {
        [Fact]
        public void Day14Test1()
        {
            // arrange
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            path = Path.GetFullPath(Path.Combine(path, "..", "..", ".."));

            var inputFile = Directory.GetFiles(path + @"/TestInput", "Day14_1.txt")[0];
            var day14 = new Day14(inputFile);

            // act
            var result1 = day14.PartOne();

            // assert
            Assert.Equal("165", result1);
        }
        [Fact]
        public void Day14Test2()
        {
            // arrange
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            path = Path.GetFullPath(Path.Combine(path, "..", "..", ".."));

            var inputFile = Directory.GetFiles(path + @"/TestInput", "Day14_2.txt")[0];
            var day14 = new Day14(inputFile);

            // act
            var result2 = day14.PartTwo();

            // assert
            Assert.Equal("208", result2);
        }
    }
}