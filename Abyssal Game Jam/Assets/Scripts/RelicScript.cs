using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicScript : MonoBehaviour
{
    public float speed;
    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles += Vector3.forward * speed;
    }
}
