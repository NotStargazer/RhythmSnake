using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] GameObject gridTile;

    public int width;
    public int height;

    private void Awake()
    {
        DrawGrid();
    }

    private void DrawGrid()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var tile = Instantiate(gridTile, transform);

                tile.transform.localPosition = new Vector3(x, y);
            }
        }
    }
}
