using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class MapManager : MonoBehaviour
{
    private const float DISTANCE_MAPS_Z = 10.0F;
    private static Vector3 setPosition = new Vector3(0.0f, 0.0f, 83.0f);

    private static MapManager _instance = null;

    public static MapManager Instance
    {
        get
        {
            return _instance;
        }
    }
    
    public List<GameObject> mapsLevel1;
    public List<GameObject> mapsLevel2;
    public List<GameObject> mapsLevel3;
    public GameObject cheeseItem;
    public List<GameObject> parent;

    private static List<GameObject> poolMap = new List<GameObject>();
    private static List<GameObject> poolCheese = new List<GameObject>();

    private int[] probability = new int[3] {50, 30, 20};

    private const int level1 = 0;
    private const int level2 = 1;
    private const int level3 = 2;


    void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        MapInit();
    }

    public void MapInit()
    {
        mapsLevel1.ForEach(level1 => poolMap.Add((GameObject)Instantiate(level1, Vector3.zero, Quaternion.identity)));
        mapsLevel2.ForEach(level2 => poolMap.Add((GameObject)Instantiate(level2, Vector3.zero, Quaternion.identity)));
        mapsLevel3.ForEach(level3 => poolMap.Add((GameObject)Instantiate(level3, Vector3.zero, Quaternion.identity)));

        poolMap.ForEach(map => map.SetActive(false));

        for (int i = 0; i < 10; i++)
        {
            poolMap[i].transform.SetParent(parent[0].transform);
        }
        for (int i = 10; i < 20; i++)
        {
            poolMap[i].transform.SetParent(parent[1].transform);
        }
        for (int i = 20; i < 30; i++)
        {
            poolMap[i].transform.SetParent(parent[2].transform);
        }

        for (int i = 0; i < 100; i++)
        {
            poolCheese.Add((GameObject)Instantiate(cheeseItem, Vector3.zero, Quaternion.identity));
        }
        poolCheese.ForEach(item => item.transform.SetParent(parent[3].transform));
        poolCheese.ForEach(item => item.SetActive(false));
    }

    public void MapSpawn(float distance)
    {
        int number = MapSpawndProbability();
        if (distance < 300f)
        {
            MapSpawn(level1);
        }
        else if (distance >= 300f && distance < 600f)
        {
            if (number != 1)
            {
                MapSpawn(level1);
            }
            else
            {
                MapSpawn(level2);
            }
        }
        else if (distance >= 600f)
        {
            if (number == 0)
            {
                MapSpawn(level1);   
            }
            else if (number == 1)
            {
                MapSpawn(level2);
            }
            else
            {
                MapSpawn(level3);
            }
        }
    }

    public GameObject GetPooledItem()
    {
        for (int i = 0; i < poolCheese.Count; i++)
        {
            if (!poolCheese[i].activeInHierarchy)
            {
                return poolCheese[i];
            }
        }
        return null;
    }
        
    private int MapSpawndProbability()
    {
        float total = 0f;
        foreach (float elem in probability)
        {
            total += elem;
        }

        float randomPoint = Random.value * total;

        for (int i = 0; i < probability.Length; i++)
        {
            if (randomPoint < probability[i])
            {
                return i;
            }
            else
            {
                randomPoint -= probability[i];
            }
        }
        return probability.Length - 1;
    }

    private void MapSpawn(int probability)
    {
        int index = 0;
        switch (probability)
        {
            case level1:
                index = Random.Range(0, 10);
                break;

            case level2:
                index = Random.Range(10, 20);
                break;

            case level3:
                index = Random.Range(20, 30);
                break;

            default:

                break;
        }
        if (!poolMap[index].activeInHierarchy)
        {
            poolMap[index].transform.position = setPosition;
            poolMap[index].SetActive(true);
            setPosition.z += DISTANCE_MAPS_Z;
        }
        else
        {
            MapSpawn(probability);
        }
    }
}
