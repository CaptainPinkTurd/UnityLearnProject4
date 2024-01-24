using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotateScript : MonoBehaviour
{
    [SerializeField] int rotateSpeed = 75;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * rotateSpeed);
    }
}
