namespace StudioOnlineBugAppender
{
    public interface IBug {
        string Title { get; set; }
        string Description { get; set; }
        string Creator { get; set; }
        string Deserialize();
    }
}