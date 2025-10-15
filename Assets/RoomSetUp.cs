using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoomSetUp : MonoBehaviour
{
    [SerializeField] private Transform[] possiblePoint;
    private List<Transform> lastedPoint = new List<Transform>();
    
    [SerializeField] private FurnitureScriptableObject furnitures;
    private List<FurnitureData> lastedFurnitures;

    [SerializeField] private int miniFurnitureNumber = 3;
    [SerializeField] private int maxiFurnitureNumber = 6;
    
    public void SetUp()
    {
        lastedPoint = new List<Transform>(possiblePoint);
        lastedFurnitures = new List<FurnitureData>(furnitures.furnitures);
        
        int numberFurniture = Random.Range(miniFurnitureNumber, maxiFurnitureNumber + 1);

        for (int i = 0; i < numberFurniture; i++)
        {
            if (lastedFurnitures.Count == 0 || lastedPoint.Count == 0) break;

            FurnitureData furnitureData = GetRandomFromList(lastedFurnitures);
            Transform point = GetRandomFromList(lastedPoint);

            if (furnitureData != null && furnitureData.furniturePrefab != null && point != null)
            {
                Instantiate(furnitureData.furniturePrefab, point.position, Quaternion.identity, transform);
            }
        }
    }

    private T GetRandomFromList<T>(List<T> list)
    {
        if (list.Count == 0) return default;

        int index = Random.Range(0, list.Count);
        T item = list[index];
        list.RemoveAt(index);
        return item;
    }
}
