using System;
using ProjectName.Application.Domain.ValueObjects;
using Xunit;

namespace ProjectName.Domain.Tests.ValueObjects
{
    public class NoteTests
    {

        [Theory]
        [InlineData("A")]
        public void Note_contains_reasonable_data(string note)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Note(note));
        }

    }
}
