using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover_03 : MonoBehaviour
{
    public float speed = 10f;
    public Vector3 targetPosition;

    private void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
    }
}
