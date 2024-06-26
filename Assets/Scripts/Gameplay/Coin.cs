using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] float _rotationspeed;
    [SerializeField] GameObject _effectPreafab;

    private void Update()
    {
        transform.Rotate(0, _rotationspeed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        CoinManager.Instance.AddOne();
        Destroy(gameObject);
        Instantiate(_effectPreafab, transform.position, transform.rotation);
    }
}
