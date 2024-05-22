using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TNT : MonoBehaviour
{
    [SerializeField] PLayerDeformation _playerDeformation;

    private void OnTriggerEnter(Collider other)
    {
        _playerDeformation.AddWidth(-30);
        _playerDeformation.AddHeigth(-30);
        Destroy(gameObject);
    }
}
