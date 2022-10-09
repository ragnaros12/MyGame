namespace MyGame;

public enum Status
{
    NoRotate,
    Rotate
}
public class Item
{ 
    public Status Status { get; set; }
    
    public void Rotate()
    {
        Status = Status == Status.Rotate ? Status.NoRotate : Status.Rotate;
    }
}