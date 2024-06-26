using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private Transform _target;

    private void Start()
    {
        transform.parent = null;
    }

    private void Update()
    {
        if (_target)
        {
            transform.position = _target.position;
        }
    }
}