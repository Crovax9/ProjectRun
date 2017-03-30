using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid : MonoBehaviour
{
    public LayerMask installableMask;
    public Vector2 gridWorldSize;
    public float nodeRadius;

    Node[,] grid;

    private float nodeDiameter;
    private int gridSizeX;
    private int gridSizeY;

    private GameObject[] itemList = new GameObject[5];

    private int num = 0;

    void Awake()
    {
        nodeDiameter = nodeRadius * 2;

        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);

        grid = new Node[gridSizeX, gridSizeY];
    }

    void OnEnable()
    {
        CreateGrid();
        if (num > 0)
        {
            SetItem();
        }
    }

    void OnDisable()
    {
        if (num > 0)
        {
            itemList.ForEach(i => i.SetActive(false));
        }
        num += 1;   
    }

    private void CreateGrid()
    {
        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;
    
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft + new Vector3(x * nodeDiameter + nodeRadius, 0.5f, y * nodeDiameter + nodeRadius);

                bool installable = !(Physics.CheckSphere(worldPoint, nodeRadius, installableMask));
                grid[x, y] = new Node(installable, worldPoint, x, y);
            }
        }
    }

    private void SetItem()
    {
        int index = Random.Range(0, gridSizeX);
        int ListNum = 0;
        for (int y = 0; y < gridSizeY; y+=2)
        {
            GameObject item = MapManager.Instance.GetPooledItem();

            if (item == null)
                return;

            if (grid[index, y].installable)
            {
                item.transform.position = grid[index, y].worldPosition;
                item.SetActive(true);
            }
            itemList[ListNum] = item;
            ListNum++;
        }
    }
}
