using Xunit;

namespace Gmtl.Components.Web.Tests
{
    public class TestApiComponentTests
    {
        [Fact]
        public void TestApiComponent_String_ReturnsSameString()
        {

            var result = new TestApiComponent("hello").GetFormatValueWithHtml();
            Assert.Equal("hello", result);
        }

        [Fact]
        public void TestApiComponent_Integer_ReturnsIntegerAsString()
        {
            var result = new TestApiComponent(123).GetFormatValueWithHtml();
            Assert.Equal("123", result);
        }

        [Fact]
        public void TestApiComponent_ListOfStrings_ReturnsCommaSeparatedString()
        {
            var input = new List<string> { "one", "two", "three" };
            var result = new TestApiComponent(input).GetFormatValueWithHtml();
            Assert.Equal("one<br />two<br />three", result);
        }

        [Fact]
        public void TestApiComponent_ListOfIntegers_ReturnsCommaSeparatedString()
        {
            var input = new List<int> { 1, 2, 3 };
            var result = new TestApiComponent(input).GetFormatValueWithHtml();
            Assert.Equal("1<br />2<br />3", result);
        }

        [Fact]
        public void TestApiComponent_NestedLists_ReturnsFlattenedCommaSeparatedString()
        {
            var input = new List<object>
        {
            "hello",
            new List<string> { "world", "nested" },
            42,
            new List<object> { "deep", new List<int> { 1, 2, 3 } }
        };

            var result = new TestApiComponent(input).GetFormatValueWithHtml();
            Assert.Equal("hello<br />world<br />nested<br />42<br />deep<br />1<br />2<br />3", result);
        }

        [Fact]
        public void TestApiComponent_EmptyList_ReturnsEmptyString()
        {
            var input = new List<object>();
            var result = new TestApiComponent(input).GetFormatValueWithHtml();
            Assert.Equal(string.Empty, result);
        }

        [Fact]
        public void TestApiComponent_Null_ReturnsEmptyString()
        {
            var result = new TestApiComponent(null).GetFormatValueWithHtml();
            Assert.Equal(string.Empty, result);
        }
    }

    public class TestApiComponent : ApiComponent
    {
        ComponentStatusInfo info;

        public TestApiComponent(object infoValue)
        {
            info = new ComponentStatusInfo { };
            info.AddInfo("info-key", infoValue);
        }

        //This is a small hack. I don't want to parse "AsHtml" method result. So I just call protected FormatValueWithHtml and check the result.  
        public string GetFormatValueWithHtml() => FormatValueWithHtml(info.ComponentInfo["info-key"]);
    }
}
