using System;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject player;
    public Camera mainCamera;

    [SerializeField] private Text txtRoomName;
    
    private void Awake()
    {
        instance = this;
    }
    
    private void SetUpPlayerPosition(Vector3 pos)
    {
        mainCamera.transform.position = new Vector3(pos.x, pos.y, -10);
        player.transform.position = pos;
    }
    
    private void UpdateUI()
    {
      txtRoomName.text = RoomManager.instance.currentRoom.ToString();
    } 
    
    
    
    
    
    void OnEnable()
    {
        HouseGenerator.OnHouseFinishGeneration += SetUpPlayerPosition;
        RoomManager.OnRoomChange += UpdateUI;
    }

    private void OnDisable()
    {
        HouseGenerator.OnHouseFinishGeneration -= SetUpPlayerPosition;
        RoomManager.OnRoomChange -= UpdateUI;
    }
}
