using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomColor : MonoBehaviour
{
    private SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        ChangeColor();
    }

    [ContextMenu("ChangeColor")]
    private void ChangeColor()
    {
        sr.color = Random.ColorHSV(
            0f, 1f,   
            0.6f, 0.6f,
            1f, 1f
        );
    }
}