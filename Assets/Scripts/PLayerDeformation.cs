using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLayerDeformation : MonoBehaviour
{
    [SerializeField] int _width;
    [SerializeField] int _heigth;

    float _widthMultiplayer = 0.0005f;
    float _heightMultiplayer = 0.01f;

    [SerializeField] Renderer _renderer;
    [SerializeField] Transform _topSpine;
    [SerializeField] Transform _bottomSpine;
    [SerializeField] Transform _colliderTransform;
    [SerializeField] AudioSource _down;
    [SerializeField] AudioSource _pump;
    int _intermediateWidth;
    int _intermediateHeight;


    private void Start()
    {
        SetWidth(Progress.Instance.Width);
        SetHeigth(Progress.Instance.Height);
    }

    void Update()
    {
        float offsetY = _heigth * _heightMultiplayer + 0.17f;
        _topSpine.position = _bottomSpine.position + new Vector3(0, offsetY, 0);
        _colliderTransform.localScale = new Vector3(1,1.84f + _heigth * _heightMultiplayer, 1);
        
    }

    public void AddWidth(int value )
    {
        _intermediateWidth = _width;
        _intermediateWidth += value;
        _width = Mathf.Clamp(_intermediateWidth, 0, 600);
        UpdeteWidht();
        if (value < 0)
        {
            _down.Play();
        } 
        else
        {
            _pump.Play();
        } 
    }
    public void AddHeigth(int value )
    {
        _intermediateHeight = _heigth;
        _intermediateHeight += value;
        _heigth =  Mathf.Clamp(_intermediateHeight, 0, 300);
        if (value < 0)
        {
            _down.Play();
        }
        else
        {
            _pump.Play();
        }

    }
    public void SetWidth(int value)
    {
        _width = value;
        UpdeteWidht();
    }
    public void SetHeigth(int value)
    {
        _heigth = value;

    }

    public void HitBarrier()
    {
        if (_heigth > 0)
        {
            _heigth -= 50;
        }
        else if ( _width > 0)
        {
            _width -= 50;
            UpdeteWidht();
        }
        else
        {
            Die();
        }
    }
    void UpdeteWidht()
    {
        _renderer.material.SetFloat("_PushValue", _width * _widthMultiplayer);
    }
    void Die()
    {
        FindObjectOfType<GameManager>().ShowfinishWindowDefeat();
        Destroy(gameObject);
    }
}
