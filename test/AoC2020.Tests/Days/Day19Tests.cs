using System.IO;
using System.Reflection;
using Xunit;
using AoC2020.Days;

namespace AoC2020.Tests.Days
{
    public class Day19Tests
    {
        [Fact]
        public void Day19Test1()
        {
            // arrange
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            path = Path.GetFullPath(Path.Combine(path, "..", "..", ".."));

            var inputFile = Directory.GetFiles(path + @"/TestInput", "Day19_1.txt")[0];
            var day19 = new Day19(inputFile);

            // act
            var result1 = day19.PartOne();

            // assert
            Assert.Equal("2", result1);
        }
        [Fact]
        public void Day19Test2()
        {
            // arrange
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            path = Path.GetFullPath(Path.Combine(path, "..", "..", ".."));

            var inputFile = Directory.GetFiles(path + @"/TestInput", "Day19_2.txt")[0];
            var day19 = new Day19(inputFile);

            // act
            var result1 = day19.PartOne();
            var result2 = day19.PartTwo();

            // assert
            Assert.Equal("3", result1);
            Assert.Equal("12", result2);
        }
    }
}