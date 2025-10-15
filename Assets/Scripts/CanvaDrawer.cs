using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CanvaDrawer : MonoBehaviour
{
    [SerializeField] private Image pixelPrefab;
    [SerializeField] private int gridSize = 8;
    [SerializeField] private float spacing = 2f;

    [SerializeField] private Color[] colors =
        { Color.black, Color.white, Color.red, Color.blue, Color.green, Color.yellow, Color.magenta };

    [SerializeField] private Color currentColor = Color.black;
    [SerializeField] private Image currentImageColor;

    [SerializeField] public Sprite drawing = null;
    [SerializeField] private Image finalImage;

    private Image[,] pixelGrid;

    private Touch controls;
    [HideInInspector] public bool isDrawing = false;

    void Awake()
    {
        controls = new Touch();
    }

    void OnEnable()
    {
        controls.Drawing.Enable();
        controls.Drawing.Draw.performed += _ => isDrawing = true;
        controls.Drawing.Draw.canceled += _ => isDrawing = false;
    }

    void OnDisable()
    {
        controls.Drawing.Draw.performed -= _ => isDrawing = true;
        controls.Drawing.Draw.canceled -= _ => isDrawing = false;
        controls.Drawing.Disable();
    }

    void Start()
    {
        DrawGrid();
    }

    private void DrawGrid()
    {
        pixelGrid = new Image[gridSize, gridSize];
        RectTransform parentRect = GetComponent<RectTransform>();
        float pixelSize = pixelPrefab.rectTransform.sizeDelta.x + spacing;

        for (int y = 0; y < gridSize; y++)
        {
            for (int x = 0; x < gridSize; x++)
            {
                Image image = Instantiate(pixelPrefab, transform);
                RectTransform rect = image.GetComponent<RectTransform>();
                rect.anchoredPosition = new Vector2(x * pixelSize, -y * pixelSize);

                image.color = ((x + y) % 2 == 0) ? Color.gray : Color.white;
                image.gameObject.AddComponent<Pixel>();

                pixelGrid[x, y] = image;
            }
        }

        parentRect.sizeDelta = new Vector2(gridSize * pixelSize, gridSize * pixelSize);
    }

    public void SetCurrentColor(int colorId)
    {
        currentColor = colors[colorId];
        currentImageColor.color = colors[colorId];
    }

    public void DrawPixel(Image image)
    {
        image.color = currentColor;
    }

    public void ResetDrawing()
    {
        foreach (Transform child in transform)
            Destroy(child.gameObject);

        DrawGrid();
    }

    public void CreateSprite()
    {
        drawing = ConvertToSprite();
    }

    public Sprite ConvertToSprite()
    {
        int width = gridSize;
        int height = gridSize;

        Texture2D texture = new Texture2D(width, height);
        texture.filterMode = FilterMode.Point;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Color color = pixelGrid[x, (height - 1) - y].color;
                texture.SetPixel(x, y, color);
            }
        }

        texture.Apply();

        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, width, height), new Vector2(0.5f, 0.5f));
        finalImage.sprite = sprite;
        
        return sprite;
    }
}
