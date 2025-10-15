using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RoomDataScriptable", menuName = "ScriptableObjects/RoomDataScriptable")]
public class RoomDataScriptable : ScriptableObject
{
    public Room type;
    public GameObject roomPrefab;
    public GameObject doorPrefab;
    public List<DoorPoint> doorPoints;
    public List<GameObject> possibleFurniture = new List<GameObject>();
    public ItemSO itemRoomData;
}

public enum DoorDirection
{
    Up,
    Left,
    Right,
    Down,
}

[Serializable]
public class DoorPoint
{
    public Vector3 position;          
    public Vector3 playerSpawnOffset; 
    public DoorDirection direction;   
}
