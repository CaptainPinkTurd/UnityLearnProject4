using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmashIdentifier : MonoBehaviour, IPowerUp
{
    public void Activate(GameObject collector)
    {
        collector.GetComponent<JumpSmashState>().enabled = true;
    }
}
