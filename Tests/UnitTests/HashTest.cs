using BLL;
using Xunit;

namespace Tests.UnitTests
{
    public class HashTest
    {
        [Fact]
        public void Hash_string_In_b80467f35b449736162b64cbaa3a2a2d_Out()
        {
            var stringInput = "string";
            var stringExpected = "b80467f3-5b44-9736-162b-64cbaa3a2a2d";
            string stringResult;

            stringResult = Hashing.GetHashString(stringInput).ToString();

            Assert.True(stringExpected.Equals(stringResult));
        }
    }
}