using System;
using blocks;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LvlGeneration : MonoBehaviour
{
    [SerializeField] private Transform _spawnPointTransform;
    [SerializeField] private ControlledLvlZone[] _controlledLvlZones;
    [SerializeField] private LvlZone[] lvlZones;
    [SerializeField] private Transform _levelTransform;
    
    private int _truePlayerHeight;
    private int _truePlayerWidth;
    private List<LvlZone> _correctZones = new();
    private List<GameObject> _activeZones = new();

    public static LvlGeneration Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    [ContextMenu("GenerateLvl")]
    public void GenerateRandomLvl()
    {
        _truePlayerHeight = 0;
        _truePlayerWidth = 0;
        DestroyAllZones();
        var nextZonePosition = 0;
        try
        {
            for (int l = 0; l < 7; l++)
            {
                if (l == 6 && _truePlayerHeight + _truePlayerWidth < 250)
                {
                    var selectedZone = Instantiate(_controlledLvlZones[Random.Range(0, _controlledLvlZones.Length)],
                        new Vector3(0, 0,
                            _spawnPointTransform.transform.position.z + nextZonePosition),
                        Quaternion.identity);
                    _activeZones.Add(selectedZone.gameObject);
                    selectedZone.transform.parent = _levelTransform;
                    selectedZone.Init(250 - (_truePlayerHeight + _truePlayerWidth));
                }
                else
                {
                    foreach (var t in lvlZones)
                    {
                        if (t.maxHeightCoin <= _truePlayerHeight / 100 + 2.3f)
                        {
                            _correctZones.Add(t);
                        }
                    }   
                    var selectedZone = _correctZones[Random.Range(0, _correctZones.Count)];
                    if (selectedZone.appOfMan >= selectedZone.addWidth)
                    {
                        _truePlayerHeight += (int)selectedZone.appOfMan;
                    }
                    else
                    {
                        _truePlayerWidth += selectedZone.addWidth;
                    }
                    var activeZone = Instantiate(selectedZone, new Vector3(0, 0,
                        _spawnPointTransform.transform.position.z + nextZonePosition), Quaternion.identity);
                    activeZone.transform.parent = _levelTransform;
                    _activeZones.Add(activeZone.gameObject);
                    nextZonePosition += 10;
                    _correctZones.Clear();
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
#if !UNITY_EDITOR
            GenerateRandomLvl();
#endif
            throw;
        }
    }
    
    public void DestroyAllZones()
    {
        foreach (var t in _activeZones)
        {
            Destroy(t);
        }
        _activeZones.Clear();
    }
}
