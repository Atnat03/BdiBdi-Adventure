

[System.Serializable]
public class RoomConstraint
{
    public Room roomA;
    public Room roomB;
    public bool mustBeAdjacent;
    public bool mustNotBeAdjacent;
}