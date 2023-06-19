using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public GameObject movingObject;
    public GameObject target;

    private void Awake()
    {
        if (!movingObject) { movingObject = gameObject; }
    }

    private void Update()
    {
        movingObject.transform.position = target.transform.position;
    }

}
