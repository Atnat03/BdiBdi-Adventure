using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerObjectManager : MonoBehaviour
{
    public static PlayerObjectManager instance;

    public static Action<PickUpItem> OnDropItem;
    
    [SerializeField] private Transform spawnObject;
    [SerializeField] public bool alreadyCarryObject;

    [SerializeField] private Button dropButton;

    PickUpItem carriedItem;
    Item currentItem;
    private Item[] itemsCloseToPlayer;

    private void Awake()
    {
        instance = this;
        
        dropButton.onClick.AddListener(DropObject);
    }

    private void Start()
    {
        alreadyCarryObject = false;
        carriedItem = null;
    }

    private void Update()
    {
        dropButton.interactable = alreadyCarryObject;
    }
    
    public void PickItem(Item item)
    {
        alreadyCarryObject = true;
        
        Instantiate(item.DropPrefab, spawnObject.transform.position, Quaternion.identity, spawnObject);
        carriedItem = item.DropPrefab.GetComponent<PickUpItem>();
        currentItem = ItemManager.instance.GetItemById(item.id);
    }

    public void DropObject()
    {
        if(!alreadyCarryObject) return;

        alreadyCarryObject = false;

        SpriteRenderer spriteRenderer = gameObject.transform.GetComponent<SpriteRenderer>();
        float rotateValue = spriteRenderer.flipX ? -0.5f : 0.5f;
        Vector2 pos = new Vector2(transform.position.x + rotateValue, transform.position.y);

        PickUpItem droppedItem = Instantiate(carriedItem.gameObject, pos, Quaternion.identity)
            .GetComponent<PickUpItem>();

        OnDropItem?.Invoke(droppedItem);

        carriedItem = null;
        Destroy(spawnObject.transform.GetChild(0).gameObject);
    }

}
