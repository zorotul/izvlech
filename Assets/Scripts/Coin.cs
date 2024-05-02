using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] float _rotationspeed;
    [SerializeField] GameObject _effectPreafab;

    void Update()
    {
        transform.Rotate(0, _rotationspeed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        FindObjectOfType<CoinManager>().AddOne();
        Destroy(gameObject);
        Instantiate(_effectPreafab, transform.position, transform.rotation);
    }
}
