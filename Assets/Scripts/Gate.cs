using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] int _value;
    [SerializeField] DeformationType _deformationType;
    [SerializeField] GateApperaence _gateApperaence;

    private void OnValidate()
    {
        _gateApperaence.UpdateVisual(_deformationType, _value);
    }
    private void OnTriggerEnter(Collider other)
    {
        PLayerDeformation pLayerDeformation = other.attachedRigidbody.GetComponent<PLayerDeformation>();
        if (pLayerDeformation)
        {
            if (_deformationType == DeformationType.Width)
            {
                pLayerDeformation.AddWidth(_value, false);
            }
            else
            {
                pLayerDeformation.AddHeigth(_value, false);
            }
            Destroy(gameObject);
        }
    }

}
