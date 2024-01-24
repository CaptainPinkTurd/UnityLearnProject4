using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    [SerializeField] float rotateSpeed;

    private float horizontalInput;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
    }
    private void FixedUpdate()
    {
        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime * horizontalInput);
    }
}
