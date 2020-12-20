using System.IO;
using System.Reflection;
using Xunit;
using AoC2020.Days;

namespace AoC2020.Tests.Days
{
    public class Day09Tests
    {
        [Fact]
        public void Day09Test1()
        {
            // arrange
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            path = Path.GetFullPath(Path.Combine(path, "..", "..", ".."));

            var inputFile = Directory.GetFiles(path + @"/TestInput", "Day09_1.txt")[0];
            var day09 = new Day09(inputFile, 5);

            // act
            var result1 = day09.PartOne();
            var result2 = day09.PartTwo();

            // assert
            Assert.Equal("127", result1);
            Assert.Equal("62", result2);
        }
    }
}