using System.Collections.Generic;
using UnityEngine;

class RoomNode
{
    public Room roomType;
    public List<RoomNode> connectedRooms = new();
}
