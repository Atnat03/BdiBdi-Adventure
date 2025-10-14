using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    public int id;
    
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !PlayerObjectManager.instance.alreadyCarryObject)
        {
            Item item = ItemManager.instance.GetItemById(id);
            
            other.GetComponent<PlayerObjectManager>().PickItem(item);
            Destroy(gameObject);
        }
    }
}
