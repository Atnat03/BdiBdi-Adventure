using UnityEngine;
using System;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "ItemManager", menuName = "ItemSO")]
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