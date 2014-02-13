namespace Cr.ArgParse.Tests
{
    public class TestDefaultSuppress : ParserTestCase
    {
        public TestDefaultSuppress()
        {
            ArgumentSignatures = new[]
            {
                new Argument("foo") {SuppressDefaultValue = true, ValueCount = new ValueCount("?")},
                new Argument("bar") {SuppressDefaultValue = true, ValueCount = new ValueCount("*")},
                new Argument("--baz") {ActionName = "store_true", SuppressDefaultValue = true}
            };
            Failures = new[] {"-x"};
            Successes = new SuccessCollection
            {
                {"", new ParseResult {}},
                {"a", new ParseResult {{"foo", "a"}}},
                {"a b", new ParseResult {{"bar", new[] {"b"}}, {"foo", "a"}}},
                {"--baz", new ParseResult {{"baz", true}}},
                {"a --baz", new ParseResult {{"baz", true}, {"foo", "a"}}},
                {"--baz a b", new ParseResult {{"bar", new[] {"b"}}, {"baz", true}, {"foo", "a"}}}
            };
        }
    }
}