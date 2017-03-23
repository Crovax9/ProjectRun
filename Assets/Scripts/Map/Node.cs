using UnityEngine;
using System.Collections;

public class Node
{
    public bool installable;
    public Vector3 worldPosition;

    public int gridX;
    public int gridY;

    public Node parent;

/*    public int gCost;
    public int hCost;

    public int fCost
    {
        get{ return gCost + hCost; }
    }*/

    public Node(bool _installable, Vector3 _worldPos, int _gridX, int _gridY)
    {
        installable = _installable;
        worldPosition = _worldPos;
        gridX = _gridX;
        gridY = _gridY;
    }

}
