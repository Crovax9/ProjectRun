using UnityEngine;
using System.Collections;

public class DummyMove : MonoBehaviour
{
    private float moveSpeed = 5.0f;

    void Update()
    {
        transform.Translate(0, 0, moveSpeed * Time.deltaTime);
    }
}
