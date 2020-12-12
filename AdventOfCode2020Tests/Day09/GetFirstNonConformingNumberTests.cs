using AdventOfCode2020.Day09;
using FluentAssertions;
using System;
using Xunit;

namespace AdventOfCode2020Tests.Day09
{
    public class GetFirstNonConformingNumberTests
    {
        [Fact]
        public void GetFirstNonConformingNumber_BadInputData()
        {
            var AcceptableLookBehindSize = 2;
            var tooSmall0 = 0;
            var tooSmall1 = 1;
            var tooLarge = 3;
            Solver.GetFirstNonConformingNumber(Array.Empty<long>(), AcceptableLookBehindSize).Should().Be(-1);
            Solver.GetFirstNonConformingNumber(new long[] { 1 }, AcceptableLookBehindSize).Should().Be(-1);
            Solver.GetFirstNonConformingNumber(new long[] { 1, -1 }, AcceptableLookBehindSize).Should().Be(-1);
            Solver.GetFirstNonConformingNumber(new long[] { 1, 1 }, AcceptableLookBehindSize).Should().Be(-1);
            Solver.GetFirstNonConformingNumber(new long[] { 1, 2, 3 }, tooSmall0).Should().Be(-1);
            Solver.GetFirstNonConformingNumber(new long[] { 1, 2, 3 }, tooSmall1).Should().Be(-1);
            Solver.GetFirstNonConformingNumber(new long[] { 1, 2, 3 }, tooLarge).Should().Be(-1);
        }

        [Theory]
        [InlineData(1, 2, 3, -1)]
        [InlineData(2, 1, 3, -1)]
        [InlineData(1, 2, 1, 1)]
        [InlineData(1, 2, 2, 2)]
        [InlineData(1, 2, 4, 4)]
        public void ValidateEncodedNr_2PreviousNrs(int nr1, int nr2, int nr3, int expected)
        {
            var lookBehindSize = 2;
            Solver.GetFirstNonConformingNumber(new long[] { nr1, nr2, nr3 }, lookBehindSize).Should().Be(expected);
        }

        [Theory]
        [InlineData(1, 2, 2, 3, 2)]
        [InlineData(1, 2, 2, 4, 2)]
        [InlineData(1, 2, 3, 4, 4)]
        [InlineData(1, 2, 3, 5, -1)]
        public void GetFirstNonConformingNumber_4NrsWith2StepsLookBack(int nr1, int nr2, int nr3, int nr4, int expected)
        {
            var lookBehindSize = 2;
            Solver.GetFirstNonConformingNumber(new long[] { nr1, nr2, nr3, nr4 }, lookBehindSize).Should().Be(expected);
        }

    }
}
