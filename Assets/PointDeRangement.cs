using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [SerializeField] private Transform emplacementDeRangement;
    
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
        item.transform.position = emplacementDeRangement.position;
    }

    private void Destroyer(GameObject item)
    {
        Destroy(item.gameObject);
    }
}
