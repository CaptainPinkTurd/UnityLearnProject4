using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class JumpSmashState : MonoBehaviour, IState
{
    public float jumpForce = 30;
    private bool hasJumped = false;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gameObject.GetComponent<JumpSmashState>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ActivateJumpSmash(hasJumped);
        }
    }
    private void ActivateJumpSmash(bool jump)
    {
        if (!jump)
        {
            rb.velocity = Vector3.zero;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            hasJumped = true;
        }
    }
    private void GroundSmash()
    {
        List<Enemy> enemies =
            FindObjectsOfType<Enemy>().OrderBy(t => Vector3.Distance(transform.position, t.transform.position)).ToList();

        float groundPoundRadius = 12f;
        float liftForce = 1.5f;

        for (int i = 0; i < enemies.Count; i++)
        {
            Rigidbody enemyRb = enemies[i].GetComponent<Rigidbody>();
            var dirToPlayer = (transform.position - enemies[i].transform.position).normalized;

            float enemiesDistanceFromPlayer = Vector3.Distance(enemies[i].transform.position, transform.position);

            //knockback force depending on how far the enemy is to the player
            float knockbackForce = 100f / enemiesDistanceFromPlayer;

            //AddExplosionForce is so good wtf
            enemyRb.AddExplosionForce(knockbackForce, transform.position, groundPoundRadius, liftForce, ForceMode.Impulse);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        //when player land on the ground and has jump smash power up
        if (collision.gameObject.CompareTag("Ground") && hasJumped)
        {
            GroundSmash();
            hasJumped = false;
        }
    }
    public void Deactivate()
    {
        gameObject.GetComponent<JumpSmashState>().enabled = false;    
    }
}
