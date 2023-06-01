using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPath : MonoBehaviour
{
    public GameObject[] points;
    private LineRenderer lr;

    private void Awake()
    {
        lr = gameObject.GetComponent<LineRenderer>();

        // Set all points
        for (int i = 0; i < points.Length; i++)
        {
            Vector3 newPos = points[i].transform.position;
            lr.SetPosition(i, newPos);
            
        }
    }
}
