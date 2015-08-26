namespace csUnit
{
    [TestFixture]
    public class GetDifferenceMethodTest
    {
        [Test]
        public void TestForDotMemoryApiClass()
        {
            Generic.GetDifferenceTests.DotMemoryApiGetDifference(Assert.True);
        }

        [Test]
        public void TestForDotMemoryClass()
        {
            Generic.GetDifferenceTests.DotMemoryGetDifference(Assert.True);
        }

    }
}
