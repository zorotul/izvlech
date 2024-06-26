using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TNT : MonoBehaviour
{
    [SerializeField] PlayerDeformation _playerDeformation;

    private void OnTriggerEnter(Collider other)
    {
        _playerDeformation.AddWidth(-30, true);
        _playerDeformation.AddHeigth(-30, true);
        Destroy(gameObject);
    }
}
