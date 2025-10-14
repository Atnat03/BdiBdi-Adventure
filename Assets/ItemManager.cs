using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager instance;
    
    [SerializeField] private ItemSO item_salon;

    private void Awake()
    {
        instance = this;
    }

    public Item GetItemById(int id)
    {
        foreach (Item item in item_salon.items)
        {
            if (item.id == id)
            {
                return item;
            }
        }
        return null;
    }
}

[CreateAssetMenu(fileName = "ItemManager", menuName = "ItemManager")]
public class ItemSO : ScriptableObject
{
    public List<Item> items;
}

[Serializable]
public class Item
{
    public int id;
    public GameObject DropPrefab;
}

