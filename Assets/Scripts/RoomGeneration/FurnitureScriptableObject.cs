using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Furniture")]
public class FurnitureScriptableObject : ScriptableObject
{
    public Room typeRoom;
    public List<FurnitureData> furnitures;
}

[Serializable]
public class FurnitureData
{
    public int id;
    public GameObject furniturePrefab;
}
