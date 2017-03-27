using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public GameObject target;
    private Vector3 offset;

    void Start () 
    {
        offset = transform.position - target.transform.position;
    }
        
    void FixedUpdate () 
    {
        if (target != null)
        {
            transform.position = target.transform.position + offset;
        }
    }
}
