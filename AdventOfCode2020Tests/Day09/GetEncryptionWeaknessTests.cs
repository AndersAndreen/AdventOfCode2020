using AdventOfCode2020.Day09;
using FluentAssertions;
using System;
using Xunit;

namespace AdventOfCode2020Tests.Day09
{
    public class GetEncryptionWeaknessTests
    {
        [Fact]
        public void GetEncryptionWeaknessTests_BadInputData()
        {
            var ok = 3;
            var tooSmall0 = 0;
            var tooSmall1 = 1;
            var tooSmall2 = 2;
            var failed = -1;
            Solver.GetEncryptionWeakness(Array.Empty<long>(), ok).Should().Be(-1);
            Solver.GetEncryptionWeakness(new long[] { 1 }, ok).Should().Be(-1);
            Solver.GetEncryptionWeakness(new long[] { 1, 3 }, failed).Should().Be(-1);
            Solver.GetEncryptionWeakness(new long[] { 1, 3 }, tooSmall0).Should().Be(-1);
            Solver.GetEncryptionWeakness(new long[] { 1, 3 }, tooSmall1).Should().Be(-1);
            Solver.GetEncryptionWeakness(new long[] { 1, 3 }, tooSmall2).Should().Be(-1);
        }

        [Theory]
        [InlineData(1, 2, 3, 4, 1, -1)]
        [InlineData(1, 2, 3, 4, 2, -1)]
        [InlineData(1, 2, 3, 4, 3, 3)]
        [InlineData(1, 2, 3, 4, 4, -1)]
        [InlineData(1, 2, 3, 4, 5, 5)]
        [InlineData(1, 2, 3, 4, 6, 4)]
        [InlineData(1, 2, 3, 4, 7, 7)]
        [InlineData(1, 2, 3, 4, 8, -1)]
        [InlineData(1, 2, 3, 4, 9, 6)]
        [InlineData(1, 2, 3, 4, 10, 5)]
        [InlineData(1, 2, 3, 4, 11, -1)]
        public void ValidateEncodedNr_2PreviousNrs(int nr1, int nr2, int nr3, int nr4, int resultStep1, long expected)
        {
            Solver.GetEncryptionWeakness(new long[] { nr1, nr2, nr3, nr4 }, resultStep1).Should().Be(expected);
        }

        [Theory]
        [InlineData(1, 4, 3, 2, 8, 5)]
        [InlineData(1, 4, 3, 2, 9, 6)]
        public void ValidateEncodedNr_2PreviousNrsInRandomOrder(int nr1, int nr2, int nr3, int nr4, int resultStep1, long expected)
        {
            Solver.GetEncryptionWeakness(new long[] { nr1, nr2, nr3, nr4 }, resultStep1).Should().Be(expected);
        }
    }
}
