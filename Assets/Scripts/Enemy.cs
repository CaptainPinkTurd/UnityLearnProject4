using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 15f;
    private Rigidbody enemyRb;
    private GameObject player;
    public int forceAppliedOnPlayer = 1;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        var dirToPlayer = player.transform.position - enemyRb.transform.position;
        enemyRb.AddForce(dirToPlayer.normalized * speed);
        
        if(transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody playerRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 collisionDir = collision.transform.position - transform.position;

            playerRb.AddForce(collisionDir * forceAppliedOnPlayer, ForceMode.Impulse);
        }
    }
}
