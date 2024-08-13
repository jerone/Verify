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
    public bool Paused => state == RecordingState.Paused;

    public void Add(string name, object item)
    {
        Guard.NotNullOrEmpty(name);
        if (Paused)
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