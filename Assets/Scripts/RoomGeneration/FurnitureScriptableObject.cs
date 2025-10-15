using System;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureScriptableObject : ScriptableObject
{
    public Room typeRoom;
    public List<FurnitureScriptableObject> furnitures;
}

[Serializable]
public class FurnitureData
{
    public int id;
    public GameObject furniturePrefab;
}
