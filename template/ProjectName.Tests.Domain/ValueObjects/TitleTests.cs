using System;
using ProjectName.Application.Domain.ValueObjects;
using Xunit;

namespace ProjectName.Domain.Tests.ValueObjects
{
    public class TitleTests
    {
        [Fact]
        public void Title_is_not_case_sensitive()
        {
            var upperCaseTitle = new Title("Mr");
            var lowerCaseTitle = new Title("mr");

            Assert.True(lowerCaseTitle == upperCaseTitle);
        }

        [Theory]
        [InlineData("")]
        [InlineData("    ")]
        public void Title_contains_reasonable_data(string title)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Title(title));
        }

        [Theory]
        [InlineData("cheese")]
        public void Title_casts_to_string(string title)
        {
            var value = new Title(title);

            string stringValue = value;

            Assert.True(title == stringValue);
        }

    }
}
