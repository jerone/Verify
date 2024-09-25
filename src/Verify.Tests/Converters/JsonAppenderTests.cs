public class JsonAppenderTests
{
    [ModuleInitializer]
    public static void Initialize() =>
    #region RegisterJsonAppender
        VerifierSettings.RegisterJsonAppender(
            _ =>
            {
                if(TestContext.Current.TestClassInstance is JsonAppenderTests)
                {
                    return new ToAppend("theData", "theValue");
                }

                return null;
            });
    #endregion

    #region JsonAppender

    [Fact]
    public Task WithJsonAppender() =>
        Verify("TheValue");

    #endregion

    #region JsonLocalAppender

    [Fact]
    public Task WithLocalJsonAppender() =>
        Verify("TheValue")
            .AppendValue("name", "value");

    #endregion

    [Fact]
    public Task WithDuplicate() =>
        Verify("TheValue")
            .AppendValue("duplicate", "value1")
            .AppendValue("duplicate", "value2");

    [Fact]
    public Task NullText() =>
        Verify((string) null!);

    [Fact]
    public Task Anon() =>
        Verify(new
        {
            foo = "bar"
        });

    #region JsonAppenderStream

    [Fact]
    public Task Stream() =>
        Verify(IoHelpers.OpenRead("sample.txt"));

    #endregion

    [Fact]
    public Task StringInfoAndStreamTarget() =>
        Verify("info", [new("bin", new MemoryStream([1]))]);

    [Fact]
    public Task File() =>
        VerifyFile("sample.txt");

    [Fact]
    public Task OnlyJsonAppender() =>
        Verify();
}