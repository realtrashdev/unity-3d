using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManagement : MonoBehaviour
{
    public static Vector3 itemDropLocation = new Vector3(GameObject.Find("Player").transform.position.x, 1, GameObject.Find("Player").transform.position.z);
}
