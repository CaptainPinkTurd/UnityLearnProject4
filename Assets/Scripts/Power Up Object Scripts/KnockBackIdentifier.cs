using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBackIdentifier : MonoBehaviour, IPowerUp
{
    public void Activate(GameObject collector)
    {
        collector.GetComponent<KnockBackState>().enabled = true;
    }
}
