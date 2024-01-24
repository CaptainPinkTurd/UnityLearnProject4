using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    private int gravityModifier = 7;
    
    private GameObject focalPoint;
    private float forwardInput;

    private Rigidbody rb;

    [SerializeField] GameObject powerUpIndicator;
    private Vector3 indicatorOffset;

    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity *= gravityModifier;
        rb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
        indicatorOffset = transform.position - powerUpIndicator.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        forwardInput = Input.GetAxis("Vertical");
    }
    private void FixedUpdate()
    {
        rb.AddForce(focalPoint.transform.forward * speed * forwardInput);
        powerUpIndicator.transform.position = transform.position - indicatorOffset; 
    }
    private void OnTriggerEnter(Collider other)
    {
        //Stop the cooldown for the current active power up and deactivate it immediately
        StopCoroutine(PowerUpCooldown());
        gameObject.GetComponent<IState>().Deactivate();

        //Activate power up
        Destroy(other.gameObject);
        //Dependency Inversion Principle ? 
        other.GetComponent<IPowerUp>().Activate(gameObject);
        powerUpIndicator.SetActive(true);
        StartCoroutine(PowerUpCooldown());
    }
    IEnumerator PowerUpCooldown()
    {
        yield return new WaitForSeconds(7);
        powerUpIndicator.SetActive(false);

        //Deactivate any kinds of power up
        var powerUps = GetComponents<IState>();
        for(int i = 0; i < powerUps.Length; i++)
        {
            powerUps[i].Deactivate();
        }
    } 
}
