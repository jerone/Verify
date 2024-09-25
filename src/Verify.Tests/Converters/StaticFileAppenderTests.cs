public class StaticFileAppenderTests
{
    [ModuleInitializer]
    public static void Initialize() =>
        VerifierSettings.RegisterFileAppender(
            _ =>
            {
                if(TestContext.Current.TestClassInstance is StaticFileAppenderTests)
                {
                    return new("txt", "data");
                }

                return null;
            });

    [Fact]
    public Task Text() =>
        Verify("Foo");

    [Fact]
    public Task EmptyString() =>
        Verify(string.Empty);

    [Fact]
    public Task Anon() =>
        Verify(new
        {
            foo = "bar"
        });

    [Fact]
    public Task NullText() =>
        Verify((string?) null);

    [Fact]
    public Task Stream() =>
        Verify(IoHelpers.OpenRead("sample.txt"));

    [Fact]
    public Task File() =>
        VerifyFile("sample.txt");
}