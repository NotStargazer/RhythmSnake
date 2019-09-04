using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] GameObject gridTile;
    [SerializeField] GameObject objective;

    public int width;
    public int height;

    private void Awake()
    {
        DrawGrid();
    }

    private void Start()
    {
        SpawnObjective(transform);
    }

    private void DrawGrid()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var tile = Instantiate(gridTile, transform.GetChild(0));

                tile.transform.localPosition = new Vector3(x, y);
            }
        }
    }

    public void SpawnObjective(Transform restrictions)
    {
        GameObject obj = Instantiate(objective, transform);

        int randX = Random.Range(0, width);
        int randY = Random.Range(0, height);
        bool badRand;

        do
        {
            badRand = false;
            foreach (Transform child in restrictions)
            {
                if (randX == Mathf.RoundToInt(child.position.x) || randY == Mathf.RoundToInt(child.position.y))
                {
                    randX = Random.Range(0, width);
                    randY = Random.Range(0, height);
                    badRand = true;
                    break;
                }
            }
        } while (badRand);


        obj.transform.localPosition = new Vector3(Random.Range(0, width), Random.Range(0, height));
    }
}
