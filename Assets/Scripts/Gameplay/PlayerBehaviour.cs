using DG.Tweening;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private PlayerMove _playerMove;
    [SerializeField] private PreFinishBehaviour _preFinishBehaviour;
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioSource _win;
    [SerializeField] private AudioSource _run;

    public static PlayerBehaviour Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        GameEvents.ResetLevelEvent.AddListener(Init);
    }

    private void Init()
    {
        transform.rotation = Quaternion.identity;
        _animator.SetBool("Dance", false);
        _animator.Play("Idle");
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

    public void WinBehaviour()
    {
        _preFinishBehaviour.enabled = false;
        transform.DORotate(Vector3.up * 180, 0.7f);
        _animator.SetBool("Dance", true);
        _run.Stop();
        _win.Play();
    }

    public void DefeatBehaviour()
    {
        _preFinishBehaviour.enabled = false;
        _animator.SetTrigger("Defeat");
        _run.Stop();
    }
}