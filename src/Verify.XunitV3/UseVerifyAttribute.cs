namespace VerifyXunit;

[EditorBrowsable(EditorBrowsableState.Never)]
[AttributeUsage(AttributeTargets.Assembly)]
public sealed class UseVerifyAttribute :
    BeforeAfterTestAttribute
{
    public override ValueTask Before(MethodInfo info, IXunitTest test)
    {
        Recording.InitializeState();
        return default;
    }

    public override ValueTask After(MethodInfo info, IXunitTest test) =>
        default;
}