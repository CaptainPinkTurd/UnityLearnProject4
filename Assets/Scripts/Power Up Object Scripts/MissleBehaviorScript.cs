using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MissleBehaviorScript : MonoBehaviour
{
    private Enemy nearestEnemy;
    public int projectileSpeed = 10;
    private int pushForce = 10;
    // Start is called before the first frame update
    void Start()
    {
        nearestEnemy = 
            FindObjectsOfType<Enemy>().OrderBy(t => 
            Vector3.Distance(transform.position, t.transform.position))
            .FirstOrDefault();
    }

    // Update is called once per frame
    void Update()
    {
        int enemyCount = FindObjectsOfType<Enemy>().Length;

        if (nearestEnemy == null)
        {
            //switch target if the current enemy fell off
            nearestEnemy =
            FindObjectsOfType<Enemy>().OrderBy(t =>
            Vector3.Distance(transform.position, t.transform.position))
            .FirstOrDefault();
        }
        else if(enemyCount == 0)
        {
            //destroy all bullet when there's no enemy left on the field
            Destroy(gameObject);
        }
        else
        {
            var enemyDir = nearestEnemy.transform.position - transform.position;

            transform.Translate(enemyDir.normalized * projectileSpeed * Time.deltaTime);

            //i tried to make the bullet rotate toward the enemy but they're kinda shit 
            float facingEnemyDir = Mathf.Sqrt(Mathf.Pow(enemyDir.x, 2) + Mathf.Pow(enemyDir.z, 2));
            transform.rotation = Quaternion.Euler(new Vector3(0f, facingEnemyDir, 0f));
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            var enemyPushedDir = collision.transform.position - transform.position;

            enemyRb.AddForce(enemyPushedDir * pushForce, ForceMode.Impulse);

            Destroy(gameObject);
        }
    }
}
