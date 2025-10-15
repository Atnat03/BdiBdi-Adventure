using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[System.Serializable]
public class MandatoryFurniture
{
    public GameObject furnitureData;
    public Transform point;
}

public class RoomSetUp : MonoBehaviour
{
    public RoomDataScriptable data;
    
    [SerializeField] private Transform[] possiblePoint;
    private List<Transform> lastedPoint = new List<Transform>();
    
    private List<GameObject> lastedFurnitures;

    [SerializeField] private int miniFurnitureNumber = 3;
    [SerializeField] private int maxiFurnitureNumber = 6;

    [SerializeField] private List<MandatoryFurniture> mandatoryFurnitures;

    public void SetUp()
    {
        lastedPoint = new List<Transform>(possiblePoint);
        lastedFurnitures = new List<GameObject>(data.possibleFurniture);

        foreach (var mandatory in mandatoryFurnitures)
        {
            if (mandatory.furnitureData != null && mandatory.furnitureData != null &&
                mandatory.point != null)
            {
                Instantiate(mandatory.furnitureData, mandatory.point.position, Quaternion.identity, transform);

                lastedPoint.Remove(mandatory.point);
                lastedFurnitures.Remove(mandatory.furnitureData);
            }
        }

        int numberFurniture = Random.Range(miniFurnitureNumber, maxiFurnitureNumber + 1);
        for (int i = 0; i < numberFurniture; i++)
        {
            if (lastedFurnitures.Count == 0 || lastedPoint.Count == 0) break;

            GameObject furnitureData = GetRandomFromList(lastedFurnitures);
            Transform point = GetRandomFromList(lastedPoint);

            if (furnitureData != null && furnitureData != null && point != null)
            {
                Instantiate(furnitureData, point.position, Quaternion.identity, transform);
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
