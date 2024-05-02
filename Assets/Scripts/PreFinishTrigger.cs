using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreFinishTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerBehaviourScript playerBehaviourScript = other.attachedRigidbody.GetComponent<PlayerBehaviourScript>();
        if (playerBehaviourScript)
        {
            playerBehaviourScript.StartPreFinishBehaviour();
        }
    }

}
