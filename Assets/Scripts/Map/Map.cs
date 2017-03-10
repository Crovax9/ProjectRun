using UnityEngine;
using System.Collections;

public class Map : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            MapManager.Instance.SetMaps();
        }
    }
}
