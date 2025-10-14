using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Pixel : MonoBehaviour, IPointerEnterHandler
{
    private Image image;
    private CanvaDrawer drawer;

    void Awake()
    {
        image = GetComponent<Image>();
        drawer = GetComponentInParent<CanvaDrawer>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (drawer.isDrawing)
            drawer.DrawPixel(image);
    }
}