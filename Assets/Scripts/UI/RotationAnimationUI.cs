using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationAnimationUI : MonoBehaviour
{
   
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, 2f, Space.World);
    }
}
