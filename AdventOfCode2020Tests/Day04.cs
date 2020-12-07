using System;
using AdventOfCode2020.Day04;
using AdventOfCode2020.Day05;
using FluentAssertions;
using Xunit;

namespace AdventOfCode2020Tests
{
    public class Day04
    {
        [Fact]
        public void IsWithinRange_BoundariesTes()
        {
            AocDay04.IsWithinRange(5, 6, 8).Should().BeFalse();
            AocDay04.IsWithinRange(6, 6, 8).Should().BeTrue();
            AocDay04.IsWithinRange(7, 6, 8).Should().BeTrue();
            AocDay04.IsWithinRange(8, 6, 8).Should().BeTrue();
            AocDay04.IsWithinRange(9, 6, 8).Should().BeFalse();
        }

        [Fact]
        public void ValidatePassportId_OnlyDigits()
        {
            AocDay04.ValidatePassportId("123456789").Should().BeTrue();
            AocDay04.ValidatePassportId("12345,789").Should().BeFalse();
            AocDay04.ValidatePassportId("12345a789").Should().BeFalse();
            AocDay04.ValidatePassportId("12345.789").Should().BeFalse();
        }

        [Fact]
        public void ValidatePassportId_OnlyNineDigits()
        {
            AocDay04.ValidatePassportId("1234567890").Should().BeFalse();
            AocDay04.ValidatePassportId("12345678").Should().BeFalse();
            AocDay04.ValidatePassportId("12345,789").Should().BeFalse();
            AocDay04.ValidatePassportId("12345a789").Should().BeFalse();
            AocDay04.ValidatePassportId("12345.789").Should().BeFalse();
        }

        [Fact]
        public void ValidateEyeColor_AcceptedValues()
        {
            AocDay04.ValidateEyeColor("amb").Should().BeTrue();
            AocDay04.ValidateEyeColor("blu").Should().BeTrue();
            AocDay04.ValidateEyeColor("brn").Should().BeTrue();
            AocDay04.ValidateEyeColor("gry").Should().BeTrue();
            AocDay04.ValidateEyeColor("grn").Should().BeTrue();
            AocDay04.ValidateEyeColor("hzl").Should().BeTrue();
            AocDay04.ValidateEyeColor("oth").Should().BeTrue();
        }

        [Fact]
        public void ValidateEyeColor_NonAcceptedValues()
        {
            AocDay04.ValidateEyeColor("ambz").Should().BeFalse();
            AocDay04.ValidateEyeColor("zamb").Should().BeFalse();
            AocDay04.ValidateEyeColor("zambz").Should().BeFalse();
            AocDay04.ValidateEyeColor("ambamb").Should().BeFalse();
            AocDay04.ValidateEyeColor("bluamb").Should().BeFalse();
            AocDay04.ValidateEyeColor("blu amb").Should().BeFalse();
            AocDay04.ValidateEyeColor("blu,amb").Should().BeFalse();
        }

        [Fact]
        public void ValidateHairColor_NonAcceptedValues()
        {
            AocDay04.ValidateHairColor("#").Should().BeFalse();
            AocDay04.ValidateHairColor("#1").Should().BeFalse();
            AocDay04.ValidateHairColor("#1234567").Should().BeFalse();
            AocDay04.ValidateHairColor("##1234567").Should().BeFalse();
            AocDay04.ValidateHairColor("12345678").Should().BeFalse();
            AocDay04.ValidateHairColor("1234567").Should().BeFalse();
            AocDay04.ValidateHairColor("1234567#").Should().BeFalse();
        }
    }
}
