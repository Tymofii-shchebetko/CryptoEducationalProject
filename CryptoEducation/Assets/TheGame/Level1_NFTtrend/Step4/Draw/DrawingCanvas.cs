using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.IO;
using System.Collections;

public class DrawingCanvas : MonoBehaviour
{
    public const string FILENAME = "Drawing.png";
    public Image imageCanvas;
    public Color eraseColor = Color.white;
    private TextureDrawingAux textureDrawingAux;
    private Mode mode = Mode.Pencil;
    private int drawingSize = 50;
    private Color drawingColor = Color.white;
    private Vector2 pencilFrom;
    private Vector2 pencilTo;
    private bool drawPencil;

    private Texture2D stickerTexture;
    public NFTSpritesCreator layerManager;
    public DrawingValidation drawingValidation;

    IEnumerator Start()
    {
        int width = (int)imageCanvas.rectTransform.rect.width;
        int height = (int)imageCanvas.rectTransform.rect.height;
        textureDrawingAux = new TextureDrawingAux(width, height, 1);

        textureDrawingAux.Clear(eraseColor);

        // Create a sprite from the texture and display it in imageCanvas
        Sprite sprite = Sprite.Create(textureDrawingAux.Texture, new Rect(0, 0, textureDrawingAux.Width, textureDrawingAux.Height), Vector2.one * 0.5f);
        imageCanvas.sprite = sprite;

        yield return null;

        // If a saved file exists, load it and draw it on the texture, else start a new one
        string filePath = Path.Combine(Application.persistentDataPath, FILENAME);
        if (File.Exists(filePath))
        {
            LoadDrawing(filePath);
        }
    }

    private void LoadDrawing(string filePath)
    {
        try
        {
            byte[] bytes = File.ReadAllBytes(filePath);
            Texture2D texture = new Texture2D(1, 1, TextureFormat.RGBA32, false);
            texture.LoadImage(bytes);
            textureDrawingAux.DrawTexture(textureDrawingAux.Width / 2 - texture.width / 2, textureDrawingAux.Height / 2 - texture.height / 2, texture, false);
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Failed to load drawing: {ex.Message}");
        }
    }

    void Update()
    {
        if (drawPencil)
        {
            Color color = mode == Mode.Eraser ? eraseColor : drawingColor;
            drawPencil = false;
            textureDrawingAux.DrawPencil(pencilFrom, pencilTo, drawingSize, color, mode == Mode.Eraser);
        }
    }

    public void TogglePencilWithSize(int size)
    {
        drawingSize = size;
    }

    public void ToggleSticker(Texture2D sticker)
    {
        mode = Mode.Sticker;
        stickerTexture = sticker;
    }

    public void ToggleEraserWithSize()
    {
        mode = Mode.Eraser;
    }

    public void SetColor(Color color)
    {
        mode = Mode.Pencil;
        drawingColor = color;
    }

    public void ClearCanvas()
    {
        textureDrawingAux.Clear(eraseColor);
    }

    public void SaveToFile()
    {
        textureDrawingAux.SaveToFile(FILENAME);
    }

    public void OnPointerDown(BaseEventData eventData)
    {
        if (drawingValidation.drawingDone)
        {
            drawingValidation.ShowButtonsHighLighted();
            return;
        }

        PointerEventData pointerData = eventData as PointerEventData;
        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(imageCanvas.rectTransform, pointerData.position, pointerData.pressEventCamera, out Vector2 localCursor))
            return;

        localCursor.x += imageCanvas.rectTransform.pivot.x * imageCanvas.rectTransform.rect.width;
        localCursor.y += imageCanvas.rectTransform.pivot.y * imageCanvas.rectTransform.rect.height;

        if (mode == Mode.Sticker)
        {
            if (stickerTexture == null)
            {
                Debug.LogError("Sticker texture not set!");
                return;
            }
            textureDrawingAux.DrawTexture(Mathf.RoundToInt(localCursor.x) - stickerTexture.width / 2, Mathf.RoundToInt(localCursor.y) - stickerTexture.height / 2, stickerTexture, true);
            return;
        }

        // If it's eraser, set the color to white, else use drawing color
        Color color = mode == Mode.Eraser ? eraseColor : drawingColor;
        textureDrawingAux.DrawCircleFill(Mathf.RoundToInt(localCursor.x), Mathf.RoundToInt(localCursor.y), drawingSize, color, mode == Mode.Eraser);

        drawingValidation.DrawingTrue(); // Disable validation check
        drawingValidation.HideError(); // Hide any error message
    }

    public void OnPointerDrag(BaseEventData eventData)
    {
        if (drawingValidation.drawingDone)
        {
            drawingValidation.ShowButtonsHighLighted();
            return;
        }

        if (mode == Mode.Sticker) return;

        PointerEventData pointerData = eventData as PointerEventData;

        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(imageCanvas.rectTransform, pointerData.position, pointerData.pressEventCamera, out Vector2 crtPos)) return;
        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(imageCanvas.rectTransform, pointerData.position - pointerData.delta, pointerData.pressEventCamera, out Vector2 prevPos)) return;

        pencilFrom = prevPos;
        pencilTo = crtPos;

        pencilFrom.x += imageCanvas.rectTransform.pivot.x * imageCanvas.rectTransform.rect.width;
        pencilFrom.y += imageCanvas.rectTransform.pivot.y * imageCanvas.rectTransform.rect.height;

        pencilTo.x += imageCanvas.rectTransform.pivot.x * imageCanvas.rectTransform.rect.width;
        pencilTo.y += imageCanvas.rectTransform.pivot.y * imageCanvas.rectTransform.rect.height;

        drawPencil = true;
    }

    public enum Mode
    {
        Pencil = 0,
        Eraser = 1,
        Sticker = 2
    }

    public void CopySprite(int layerNum)
    {
        layerManager.CreateNewElement(imageCanvas.sprite, layerNum);
        StartCoroutine(Start());
    }
}
