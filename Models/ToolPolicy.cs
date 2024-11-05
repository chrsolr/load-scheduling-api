public record ToolPolicy
{
    public string LoadSchedulingReader { get; } = "loadScheduling:reader";
    public string LoadSchedulingEditor { get; } = "loadScheduling:editor";
    public string LoadSchedulingIBT { get; } = "loadScheduling:ibt";
}
