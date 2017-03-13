using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class MapManager : MonoBehaviour
{
    private const float DISTANCE_MAPS_Z = 10.0F;

    private static Vector3 setPosition = new Vector3(0.0f, 0.0f, 43.0f);


    private static MapManager _instance = null;

    public static MapManager Instance
    {
        get
        {
            return _instance;
        }
    }
    
    public List<GameObject> mapsLevel1;

    public GameObject parent;

    private static List<GameObject> poolLevel1 = new List<GameObject>();

    void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        MapsSet();
    }

    public void MapsSet()
    {
        mapsLevel1.ForEach((c, index) => poolLevel1.Add((GameObject)Instantiate(c, Vector3.zero, Quaternion.identity)));
        poolLevel1.ForEach((c, index) => c.transform.SetParent(parent.transform));

        poolLevel1.ForEach(c => c.SetActive(false));
    }

    public void SetMaps()
    {
        int index = Random.Range(0, 10);
        if (poolLevel1[index].activeInHierarchy)
        {
            SetMaps();
            return;
        }
        poolLevel1[index].transform.position = setPosition;
        poolLevel1[index].SetActive(true);
        setPosition.z += DISTANCE_MAPS_Z;
    }

    public void ObstaclesJudgement(GameObject obstacles)
    {
        switch (obstacles.name)
        {
            case "Stone":
                obstacles.GetComponent<MeshSplit>().enabled = true;
                break;

            case "Stump":
                obstacles.GetComponent<MeshSplit>().enabled = true;
                break;

            default:

                break;
        }
    }
}
