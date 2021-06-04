using System;
using ProjectName.Application.Domain.ValueObjects;
using Xunit;

namespace ProjectName.Domain.Tests.ValueObjects
{
    public class NameTests
    {
        [Fact]
        public void Name_is_not_case_sensitive()
        {
            var upperCaseTitle = new Name("Bob");
            var lowerCaseTitle = new Name("bob");

            Assert.True(lowerCaseTitle == upperCaseTitle);
        }

        [Theory]
        [InlineData("A")]
        [InlineData("TothanandimaAnorhirrondQuenwenfuthornAnorhirrondDehtoiondirAltalarasoGizesabquaParraieforganFlasbhaiprealMonjinnlusneBaregorlaOchianggonciTothanandimaAnorhirrondQuenwenfuthornAnorhirrondDehtoiondirAltalarasoGizesabquaParraieforganFlasbhaiprealMonjinnlusneBaregorlaOchianggonci")]
        public void Name_contains_reasonable_data(string title)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Name(title));
        }

    }
}
