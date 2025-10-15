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

    [SerializeField] private List<RoomEntry> roomEntries = new List<RoomEntry>();

    private Dictionary<Room, Transform> roomDictionary;

    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject player;

    private void Awake()
    {
        instance = this;
        roomDictionary = new Dictionary<Room, Transform>();

        foreach (var entry in roomEntries)
        {
            if (!roomDictionary.ContainsKey(entry.room))
                roomDictionary.Add(entry.room, entry.transform);
        }

        if (roomDictionary.ContainsKey(Room.Salon))
            mainCamera.transform.position = new Vector3(roomDictionary[Room.Salon].position.x, roomDictionary[Room.Salon].position.y, -10);
    }

    public void ChangeRoom(Room idRoom, Vector3 offset)
    {
        if (!roomDictionary.ContainsKey(idRoom)) return;

        mainCamera.transform.position = new Vector3(roomDictionary[idRoom].position.x, roomDictionary[idRoom].position.y, -10);
        player.transform.position = roomDictionary[idRoom].position + offset;
    }
}