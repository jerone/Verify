// ReSharper disable UnusedParameter.Local

public class Tests
{
    // ReSharper disable once UnusedMember.Local
    static void DerivePathInfo() =>
    #region DerivePathInfoExpecto

        Verifier.DerivePathInfo(
            (sourceFile, projectDirectory, type, method) => new(
                directory: Path.Combine(projectDirectory, "Snapshots"),
                typeName: type,
                methodName: method));
    #endregion


    [Tests]
    public static Test myTest = Runner.TestCase(
        nameof(myTest),
        () => Verify(
            name: nameof(myTest),
            target: "value"));

    [Tests]
    public static Test stringTarget = Runner.TestCase(
        nameof(stringTarget),
        () => Verify(
            name: nameof(stringTarget),
            target: new Target("txt", "Value")));

    [Tests]
    public static Test withTargets = Runner.TestCase(
        nameof(withTargets),
        () => Verify(
            name: nameof(withTargets),
            target: new
            {
                Property = "Value"
            },
            rawTargets: [new("txt", "TextTarget")]));

    [Tests]
    public static Test recordingStartInAsync = Runner.TestCase(
        nameof(recordingStartInAsync),
        async () =>
        {
            await AsyncStart();
            Recording.Add("name", "value");
            await Verify(name: nameof(recordingStartInAsync));
        });

    static async Task AsyncStart()
    {
        await Task.Delay(1);
        Recording.Start();
    }
}