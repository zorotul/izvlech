using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishTrigger : MonoBehaviour
{
    [SerializeField] CoinManager _coinManager;
    private void OnTriggerEnter(Collider other)
    {
        PlayerBehaviourScript playerBehaviourScript = other.attachedRigidbody.GetComponent<PlayerBehaviourScript>();
        if (playerBehaviourScript)
        {
            _coinManager.SaveToCoins();
            playerBehaviourScript.FinishBehaviour();
            FindObjectOfType<GameManager>().ShowfinishWindowWin();
        }
    }
}
