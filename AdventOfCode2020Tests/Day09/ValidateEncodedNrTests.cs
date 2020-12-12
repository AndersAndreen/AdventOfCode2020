using AdventOfCode2020.Day09;
using FluentAssertions;
using System;
using Xunit;

namespace AdventOfCode2020Tests.Day09
{
    public class ValidateEncodedNrTests
    {
        [Fact]
        public void ValidateEncodedNr_BadInputData_ReturnsFalse()
        {
            Solver.ValidateEncodedNr(Array.Empty<long>(), 0).Should().BeFalse();
            Solver.ValidateEncodedNr(new long[] { 1 }, 0).Should().BeFalse();
            Solver.ValidateEncodedNr(new long[] { 1 }, 1).Should().BeFalse();
        }

        [Theory]
        [InlineData(1, 1, 2)]
        [InlineData(2, 2, 4)]
        public void ValidateEncodedNr_2IdenticalPreviousNrs_ReturnsFalse(int prev1, int prev2, int number)
        {
            Solver.ValidateEncodedNr(new long[] { prev1, prev2 }, number).Should().BeFalse();
        }

        [Theory]
        [InlineData(1, 2, 3, true)]
        [InlineData(2, 1, 3, true)]
        [InlineData(1, 2, 1, false)]
        [InlineData(1, 2, 2, false)]
        [InlineData(1, 2, 4, false)]
        public void ValidateEncodedNr_2PreviousNrs(int prev1, int prev2, int number, bool expected)
        {
            Solver.ValidateEncodedNr(new long[] { prev1, prev2 }, number).Should().Be(expected);
        }

        [Theory]
        [InlineData(1, 2, 3, 1, false)]
        [InlineData(1, 2, 3, 2, false)]
        [InlineData(1, 2, 3, 3, true)]
        [InlineData(1, 2, 3, 4, true)]
        [InlineData(1, 2, 3, 5, true)]
        [InlineData(1, 2, 3, 6, false)]
        public void ValidateEncodedNr_3PreviousNrs(int prev1, int prev2, int prev3, int number, bool expected)
        {
            Solver.ValidateEncodedNr(new long[] { prev1, prev2, prev3 }, number).Should().Be(expected);
        }

    }
}
