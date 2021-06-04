using System.Threading.Tasks;
using NUnit.Framework;
using static ProjectName.Application.Integration.Tests.Testing;

namespace ProjectName.Application.Integration.Tests
{
    public class TestBase
    {
        [SetUp]
        public async Task TestSetUp()
        {
            await ResetState().ConfigureAwait(false);
        }
    }
}
