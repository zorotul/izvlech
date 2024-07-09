using blocks;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LvlGeneration : MonoBehaviour
{
    [SerializeField] SpavnPoint _spavnPoint;
    public LvlZone[] lvlZones;
    int heightAppPlayer;
    List<LvlZone> goods = new List<LvlZone>();

    private void Init()
    {
        var _playerData = GameDataManager.GetPlayerData();
        int appToCordinat = 0;
        for (int l = 0; l < 7; l++)
        {
            for (int i = 0; i < lvlZones.Length; i++)
            {
                if (lvlZones[i].maxHeightCoin <= _playerData.activeHeight / 100 + heightAppPlayer / 100 + 2.3f)
                {
                    goods.Add(lvlZones[i]);

                }
            }
            LvlZone goodZone = goods[Random.Range(0, goods.Count)];
            heightAppPlayer += System.Convert.ToInt32(goodZone.appOfMan);
            Instantiate(goodZone, new Vector3(0, 0, _spavnPoint.transform.position.z + appToCordinat), Quaternion.identity);
            appToCordinat += 10;
            goods.Clear();
        }
    }

}
