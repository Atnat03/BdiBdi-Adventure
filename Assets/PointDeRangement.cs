using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointDeRangement : MonoBehaviour
{
    [SerializeField] private List<int> itemCanBePutHere;

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
            Debug.Log("Detect item");

            if(itemCanBePutHere.Contains(item.id))
            {
                Debug.Log("L'objet est dans la zone !");
            }
        }
    }
}
