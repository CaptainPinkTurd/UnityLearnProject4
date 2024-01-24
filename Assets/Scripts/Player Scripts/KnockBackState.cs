using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBackState : MonoBehaviour, IState
{
    private float powerUpStrength = 20f;

    private void Start()
    {
        gameObject.GetComponent<KnockBackState>().enabled = false;
    }
    private void OnCollisionEnter(Collision collision) //on collision will run even when the script is not activated
    {
        if (collision.gameObject.CompareTag("Enemy") && gameObject.GetComponent<KnockBackState>().enabled)
        {
            //local component variables, we get the enemy component locally
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 collisionDir = collision.transform.position - transform.position;

            enemyRb.AddForce(collisionDir * powerUpStrength, ForceMode.Impulse);
        }
    }
    public void Deactivate()
    {
        gameObject.GetComponent<KnockBackState>().enabled = false; 
    }
}
