using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissleState : MonoBehaviour, IState
{
    [SerializeField] GameObject missle;
    private int enemyCount;

    private void Start()
    {
        gameObject.GetComponent<MissleState>().enabled = false;
    }
    private void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ActivateMissle();
        }
    }
    private void ActivateMissle()
    {
        Enemy[] allEnemy = FindObjectsOfType<Enemy>();

        for (int i = 0; i < enemyCount; i++)
        {
            //get the direction from all enemy to spread the bullet spawn position
            Vector3 dirFromEnemy = (allEnemy[i].transform.position - transform.position).normalized;
            Instantiate(missle, transform.position + dirFromEnemy * 2, Quaternion.identity);
        }
    }
    public void Deactivate()
    {
        gameObject.GetComponent<MissleState>().enabled = false;
    }
}
