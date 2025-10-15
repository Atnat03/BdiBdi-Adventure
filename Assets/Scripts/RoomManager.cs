using System;
using System.Collections.Generic;
using UnityEngine;

public enum Room
{
    Salon,
    Cuisine,
    Chambre,
    SalleDeBain
}

[Serializable]
public struct RoomEntry
{
    public Room room;
    public Transform transform;
}

public class RoomManager : MonoBehaviour
{
    public static RoomManager instance;
    
    private Dictionary<Room, Transform> roomDictionary;

    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject player;

    private void Awake()
    {
        instance = this;
        roomDictionary = new Dictionary<Room, Transform>();
    }
    
    public Transform GetRoomTransform(Room idRoom)
    {
        if (roomDictionary.TryGetValue(idRoom, out Transform t))
            return t;
        return null;
    }
    
    public void RegisterRoom(Room idRoom, Transform roomTransform)
    {
        if (!roomDictionary.ContainsKey(idRoom))
            roomDictionary.Add(idRoom, roomTransform);
    }

    public void ChangeRoom(Room idRoom, Vector3 offset)
    {
        if (!roomDictionary.ContainsKey(idRoom)) return;

        mainCamera.transform.position = new Vector3(roomDictionary[idRoom].position.x, roomDictionary[idRoom].position.y, -10);
        player.transform.position = roomDictionary[idRoom].position + offset;
    }
}