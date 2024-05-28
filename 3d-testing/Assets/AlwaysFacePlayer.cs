using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlwaysFacePlayer : MonoBehaviour
{
    GameObject targetObject;
    Transform target;

    private void Start()
    {
        targetObject = GameObject.Find("Player");
        target = targetObject.transform;
    }
    void Update()
    {
        if (target != null)
        {
            transform.LookAt(target);
        }
    }
}
