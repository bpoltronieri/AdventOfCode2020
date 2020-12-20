using System.IO;
using System.Reflection;
using Xunit;
using AoC2020.Days;

namespace AoC2020.Tests.Days
{
    public class Day07Tests
    {
        [Fact]
        public void Day07Test1()
        {
            // arrange
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            path = Path.GetFullPath(Path.Combine(path, "..", "..", ".."));

            var inputFile = Directory.GetFiles(path + @"/TestInput", "Day07_1.txt")[0];
            var day07 = new Day07(inputFile);

            // act
            var result1 = day07.PartOne();
            var result2 = day07.PartTwo();

            // assert
            Assert.Equal("4", result1);
            Assert.Equal("32", result2);
        }
        [Fact]
        public void Day07Test2()
        {
            // arrange
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            path = Path.GetFullPath(Path.Combine(path, "..", "..", ".."));

            var inputFile = Directory.GetFiles(path + @"/TestInput", "Day07_2.txt")[0];
            var day07 = new Day07(inputFile);

            // act
            var result2 = day07.PartTwo();

            // assert
            Assert.Equal("126", result2);
        }
    }
}