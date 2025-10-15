using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerObjectManager : MonoBehaviour
{
    public static PlayerObjectManager instance;
    public static Action<PickUpItem> OnDropItem;

    [SerializeField] private Transform spawnObject;
    [SerializeField] public bool alreadyCarryObject;
    [SerializeField] private Button dropButton;

    private PickUpItem carriedItem;
    private Item currentItem;
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
        dropButton.interactable = alreadyCarryObject && carriedItem != null && CheckEmplacement();
    }

    public void PickItem(Item item)
    {
        alreadyCarryObject = true;
        GameObject itemInstance = Instantiate(item.DropPrefab, spawnObject.position, Quaternion.identity, spawnObject);
        carriedItem = itemInstance.GetComponent<PickUpItem>();
        currentItem = ItemManager.instance.GetItemById(item.id);
    }

    public void DropObject()
    {
        if (!alreadyCarryObject || carriedItem == null) return;
        if (!CheckEmplacement()) return;

        alreadyCarryObject = false;

        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        float rotateValue = spriteRenderer.flipX ? -0.5f : 0.5f;
        Vector2 pos = new Vector2(transform.position.x + rotateValue, transform.position.y);

        PickUpItem droppedItem = Instantiate(carriedItem.gameObject, pos, Quaternion.identity)
            .GetComponent<PickUpItem>();

        OnDropItem?.Invoke(droppedItem);

        carriedItem = null;
        Destroy(spawnObject.GetChild(0).gameObject);
    }

    public bool CheckEmplacement()
    {
        Vector2 direction = GetComponent<SpriteRenderer>().flipX ? Vector2.left : Vector2.right;
        Vector2 origin = GetComponent<SpriteRenderer>().flipX ?
            new Vector2(transform.position.x - 0.35f, transform.position.y) :
            new Vector2(transform.position.x + 0.35f, transform.position.y);

        RaycastHit2D hit = Physics2D.Raycast(origin, direction, 0.2f);
        Debug.DrawRay(origin, direction * 0.2f, Color.red, 0.2f);

        if (hit.collider != null)
        {
            PointDeRangement p = hit.collider.GetComponent<PointDeRangement>();
            if (p != null)
            {
                bool itemCompatible = p.canPutAllItem || p.itemCanBePutHere.Contains(carriedItem.id);
                return itemCompatible && p.HasPlace();
            }
        }

        return true;
    }

}
