using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayScript : MonoBehaviour
{
    public Texture2D Image;
    public GameObject Workplace;
    public Camera Test;
    public Vector2Int Size = new Vector2Int(256, 256);
    // Start is called before the first frame update
    void Start()
    {
        Test.targetTexture = new RenderTexture(Size.x, Size.y, 16);
        //Generate(Image, Size);
        InvokeRepeating(nameof(Render), 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Render()
    {
        if (!Test.targetTexture.IsCreated())
            Test.targetTexture.Create();
        // Test.Render();
        var renderTexture = Test.targetTexture;
        Texture2D tex2d = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);

        RenderTexture.active = renderTexture;
        tex2d.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        tex2d.Apply();
        if (pixels.Count == 0)
            Generate(tex2d, Size);
        else
            Update(tex2d, Size);
    }

    public void Generate()
    {
        Generate(Image, Size);
    }

    private readonly Dictionary<Vector2Int, Renderer> pixels = new Dictionary<Vector2Int, Renderer>();
    private void Generate(Texture2D image, Vector2Int size)
    {
        Destroy(Workplace);
        Workplace = new GameObject();
        Workplace.transform.parent = transform;

        pixels.Clear();

        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                var pixel = GameObject.CreatePrimitive(PrimitiveType.Quad);
                pixel.transform.SetParent(Workplace.transform);
                pixel.transform.localScale = Vector3.one;
                pixel.transform.localPosition = new Vector3(x, y);
                pixel.name = $"{x:0000}x{y:0000}";
                var renderer = pixel.GetComponent<Renderer>();
                renderer.material = new Material(renderer.material);
                renderer.material.color = GetPixel(image, size, x, y);
                pixels[new Vector2Int(x, y)] = renderer;
            }
        }
    }

    private void Update(Texture2D image, Vector2Int size)
    {
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                pixels[new Vector2Int(x,y)].material.color = GetPixel(image, size, x, y);
            }
        }
    }

    private Color GetPixel(Texture2D image, Vector2Int size, int x, int y)
    {
        if (image.width == size.x && image.height == size.y)
            return GetExactPixel(image, x, y);
        else if (image.width > size.x && image.height > size.y)
            return GetSmallerPixel(image, size, x, y);
        else if (image.width < size.x && image.height < size.y)
            return GetBiggerPixel(image, size, x, y);
        else
            throw new ArgumentException("One cordinate is smaller and other bigger, can't get pixel");
    }

    // Texture is same size as size
    private Color GetExactPixel(Texture2D image, int x, int y)
    {
        return image.GetPixel(x, y);
    }

    // Texture is bigger than size
    private Color GetSmallerPixel(Texture2D image, Vector2Int size, int x, int y)
    {
        var xRatio = image.width / size.x;
        var yRatio = image.height / size.y;
        var mapX = Mathf.RoundToInt(xRatio * x);
        var mapY = Mathf.RoundToInt(yRatio * y);
        var color = image.GetPixel(mapX, mapY);
        int k = 0;
        for (int i = 0; i < xRatio; i++)
        {
            for (int j = 0; j < yRatio; j++)
            {
                k++;
                color += image.GetPixel(mapX + i, mapY + j);
            }
        }
        color /= k;
        return color;
    }

    // Texture is smaller than size
    private Color GetBiggerPixel(Texture2D image, Vector2Int size, int x, int y)
    {
        var mapX = Mathf.RoundToInt((image.width / size.x) * x);
        var mapY = Mathf.RoundToInt((image.height / size.y) * y);
        return image.GetPixel(mapX, mapY);
    }
}
