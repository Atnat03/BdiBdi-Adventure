using UnityEngine;

public class Door : MonoBehaviour
{
    private Room roomIdToGo;
    private Vector3 playerSpawnOffset;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            RoomManager.instance.ChangeRoom(roomIdToGo, playerSpawnOffset);
        }
    }

    public void SetTargetRoom(Room targetRoom, Vector3 spawnOffset)
    {
        roomIdToGo = targetRoom;
        playerSpawnOffset = spawnOffset;
    }
}