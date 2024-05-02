using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    [SerializeField] GameObject _bricksEffectPreafad;
    private void OnTriggerEnter(Collider other)
    {
        PLayerDeformation pLayerDeformation = other.attachedRigidbody.GetComponent<PLayerDeformation>();
        if (pLayerDeformation)
        {
            pLayerDeformation.HitBarrier();
            Destroy(gameObject);
            Instantiate(_bricksEffectPreafad, transform.position, transform.rotation);
        }
    }

}
