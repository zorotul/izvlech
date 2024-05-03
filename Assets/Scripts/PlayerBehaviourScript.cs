using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerBehaviourScript : MonoBehaviour
{
    [SerializeField] PlayerMove _playerMove;
    [SerializeField] PreFinishBehaviour _preFinishBehaviour;
    [SerializeField] Animator _animator;
    [SerializeField] AudioSource _win;
    [SerializeField] AudioSource _run;

    void Start()
    {
        _playerMove.enabled = false;
        _preFinishBehaviour.enabled = false;
    }

    public void Play()
    {
        _playerMove.enabled = true;
    }
    public void StartPreFinishBehaviour() 
    {
        _playerMove.enabled = false;
        _preFinishBehaviour.enabled = true;
    }
    public void FinishBehaviour()
    {
        _preFinishBehaviour.enabled = false;
        _animator.SetTrigger("Dance");
        _run.Stop();
        _win.Play();
    }

}
