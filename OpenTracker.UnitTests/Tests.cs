using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace OpenTracker.UnitTests
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class Tests
    {
    }

    [CollectionDefinition("TestTest", DisableParallelization = true)]
    public class TestTest
    {
    }

    [Collection("TestTest")]
    public class TestTestTests
    {
        [Theory]
        [MemberData(nameof(TestData))]
        public async Task Test(int test)
        {
            await Task.Delay(TimeSpan.FromSeconds(5));
            Assert.True(true);
        }

        public static IEnumerable<object[]> TestData =>
            new List<object[]>
            {
                new object[] { 0 },
                new object[] { 1 }
            };
    }
}
