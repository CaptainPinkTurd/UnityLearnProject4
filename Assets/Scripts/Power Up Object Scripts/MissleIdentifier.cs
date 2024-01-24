using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissleIdentifier : MonoBehaviour, IPowerUp
{
    public void Activate(GameObject collector)
    {
        collector.GetComponent<MissleState>().enabled = true;
    }
}
