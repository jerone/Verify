class RecordingContext
{
    List<ToAppend> items = [];

    internal IReadOnlyCollection<ToAppend> Items => items;

    enum RecordingState
    {
        Pending,
        Recording,
        Paused
    }

    RecordingState state = RecordingState.Pending;
    public bool IsPaused => state == RecordingState.Paused;
    public bool IsRecording => state == RecordingState.Recording;
    public bool IsPending => state == RecordingState.Pending;

    public void Add(string name, object item)
    {
        Guard.NotNullOrEmpty(name);
        if (IsPending)
        {
            throw new("Recording.Start must be called before Recording.Add");
        }

        if (IsPaused)
        {
            return;
        }

        if (Recording.IsIgnored(name))
        {
            return;
        }

        var append = new ToAppend(name, item);
        lock (items)
        {
            items.Add(append);
        }
    }

    public void Pause() =>
        state = RecordingState.Paused;

    public void Start() =>
        state = RecordingState.Recording;

    public void Resume() =>
        state = RecordingState.Recording;

    public void Clear()
    {
        lock (items)
        {
            items.Clear();
        }
    }
}