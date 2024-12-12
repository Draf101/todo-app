public class TodoItem
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public TodoState State { get; set; }
    public required string Content { get; set; }
    public DateTime CreationDate { get; set; }

    public TodoItem() {}
}

// Enum for state representation
public enum TodoState
{
    Open = 1,
    InProgress = 2,
    Finished = 3
}

