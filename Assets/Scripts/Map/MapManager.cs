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
    public List<GameObject> mapsLevel2;
    public List<GameObject> mapsLevel3;

    public List<GameObject> parent;

    private static List<GameObject> poolLevel1 = new List<GameObject>();
    private static List<GameObject> poolLevel2 = new List<GameObject>();
    private static List<GameObject> poolLevel3 = new List<GameObject>();

    private int[] probability = new int[3] {50, 30, 20};

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
        mapsLevel1.ForEach((c, index) => poolLevel1.Add((GameObject)Instantiate(c, Vector3.zero, Quaternion.identity)));
        poolLevel1.ForEach((c, index) => c.transform.SetParent(parent[0].transform));

        poolLevel1.ForEach(c => c.SetActive(false));

        mapsLevel2.ForEach((c, index) => poolLevel2.Add((GameObject)Instantiate(c, Vector3.zero, Quaternion.identity)));
        poolLevel2.ForEach((c, index) => c.transform.SetParent(parent[1].transform));

        poolLevel2.ForEach(c => c.SetActive(false));

        mapsLevel3.ForEach((c, index) => poolLevel3.Add((GameObject)Instantiate(c, Vector3.zero, Quaternion.identity)));
        poolLevel3.ForEach((c, index) => c.transform.SetParent(parent[2].transform));

        poolLevel3.ForEach(c => c.SetActive(false));
    }

    public void MapSpawn(float distance)
    {
        int number = SelectedProbability();
        if (distance < 300f)
        {
            Level1MapSetting();
        }
        else if (distance >= 300f && distance < 600f)
        {
            if (number != 1)
            {
                Level1MapSetting();
            }
            else
            {
                Level2MapSetting();
            }
        }
        else if (distance >= 600f)
        {
            if (number == 1)
            {
                Level1MapSetting();
            }
            else if (number == 2)
            {
                Level2MapSetting();
            }
            else
            {
                Level3MapSetting();
            }
        }
    }

    private int SelectedProbability()
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

    private void Level1MapSetting()
    {
        int index = Random.Range(0, poolLevel1.Count);
        if (poolLevel1[index].activeInHierarchy)
        {
            Level1MapSetting();
            return;
        }
        poolLevel1[index].transform.position = setPosition;
        poolLevel1[index].SetActive(true);
        setPosition.z += DISTANCE_MAPS_Z;
    }

    private void Level2MapSetting()
    {
        int index = Random.Range(0, poolLevel2.Count);
        if (poolLevel2[index].activeInHierarchy)
        {
            Level2MapSetting();
            return;
        }
        poolLevel2[index].transform.position = setPosition;
        poolLevel2[index].SetActive(true);
        setPosition.z += DISTANCE_MAPS_Z;
    }

    private void Level3MapSetting()
    {
        int index = Random.Range(0, poolLevel3.Count);
        if (poolLevel3[index].activeInHierarchy)
        {
            Level3MapSetting();
            return;
        }
        poolLevel3[index].transform.position = setPosition;
        poolLevel3[index].SetActive(true);
        setPosition.z += DISTANCE_MAPS_Z;
    }
}
