using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Room roomIdToGo;
    [SerializeField] private Transform doorRoomToGo;
    [SerializeField] private Vector3 offset =  new Vector3(0.5f, 0, 0);

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            RoomManager.instance.ChangeRoom(roomIdToGo, offset);
        }
    }
}
