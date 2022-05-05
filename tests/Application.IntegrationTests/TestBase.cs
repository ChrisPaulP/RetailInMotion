using NUnit.Framework;
using static RetailInMotion.Application.IntegrationTests.Testing;

namespace RetailInMotion.Application.IntegrationTests
{
    public class TestBase
    {
        [SetUp]
        public async Task TestSetUp()
        {
            await ResetState();
        }
    }
}