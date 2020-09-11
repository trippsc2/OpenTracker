using Xunit;
[assembly: CollectionBehavior(DisableTestParallelization = true)]

namespace OpenTracker.UnitTests
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class Tests
    {
    }
}
