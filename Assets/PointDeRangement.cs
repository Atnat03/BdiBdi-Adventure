using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public enum RangementType
{
    Destroyer,
    Placer
}

public class PointDeRangement : MonoBehaviour
{
    [SerializeField] public List<int> itemCanBePutHere;
    [SerializeField] public bool canPutAllItem = false;

    [SerializeField] private RangementType rangementType;

    [SerializeField] private Transform[] emplacementDeRangement;
    private List<Transform> currentUnusePlace = new();

    private void Start()
    {
        foreach (Transform t in emplacementDeRangement)
        {
            currentUnusePlace.Add(t);
        }
    }

    public void OnEnable()
    {
        PlayerObjectManager.OnDropItem += DetectItem;
    }
    
    public void OnDisable()
    {
        PlayerObjectManager.OnDropItem -= DetectItem;
    }
    
    public void DetectItem(PickUpItem item)
    {
        Collider2D zoneCollider = GetComponent<Collider2D>();

        if (zoneCollider.bounds.Contains(item.transform.position))
        {
            if (canPutAllItem)
            {
                if (item.GetComponent<PickUpItem>())
                {
                    PerformAction(item.gameObject);
                }
            }else 
            { 
                if(itemCanBePutHere.Contains(item.id))
                {
                    PerformAction(item.gameObject);
                }
            }
        }
    }

    public bool HasPlace()
    {
        return (currentUnusePlace.Count > 0);
    }

    void PerformAction(GameObject item)
    {
        switch (rangementType)
        {
            case RangementType.Destroyer:
                Destroyer(item);
                break;
            case RangementType.Placer:
                Placer(item);
                break;
        }
    }

    private void Placer(GameObject item)
    {
        if(currentUnusePlace.Count <= 0) return;
        
        Transform newT = currentUnusePlace[Random.Range(0, currentUnusePlace.Count)];
        currentUnusePlace.Remove(newT);
        
        item.transform.position = newT.position;
        item.transform.parent = transform;
        Destroy(item.GetComponent<PickUpItem>());
    }

    private void Destroyer(GameObject item)
    {
        Destroy(item.gameObject);
    }
}
