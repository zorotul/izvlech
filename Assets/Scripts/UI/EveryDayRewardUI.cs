using blocks;
using System;
using UnityEngine;

[Serializable]
public struct EveryDayRewardData
{
    public Sprite image;
    public string text;
}

public class EveryDayRewardUI : MonoBehaviour
{
    [SerializeField] private EveryDayRewardData[] _rewardDatasForDay;
    [SerializeField] private EveryDayRewardItem _itemPrefab;
    [SerializeField] private Transform _rewardsContainer;
 
    public static EveryDayRewardUI Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void Init()
    {
        var playerData = GameDataManager.GetPlayerData();
        for (int i = 0; i < _rewardDatasForDay.Length; i++)
        {
            var reward = Instantiate(_itemPrefab, _rewardsContainer);
            reward.Init(playerData.everyDayRewardsInfo[i], _rewardDatasForDay[i].text, _rewardDatasForDay[i].image);
        }
    }
}