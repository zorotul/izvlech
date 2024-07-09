using UnityEngine;
using UnityEngine.Events;
using YG;

public class SomethingForAdsButton : MonoBehaviour
{
    [SerializeField] private UnityEvent _afterAds;

    private void OnEnable() => YandexGame.RewardVideoEvent += SomethingForAds;

    private void OnDisable() => YandexGame.RewardVideoEvent -= SomethingForAds;

    public void StartRewardedVideo()
    {
        YandexGame.RewVideoShow((int)VideoAdsId.RewardForAds);
    }

    private void SomethingForAds(int value)
    { 
        if (value == (int)VideoAdsId.RewardForAds)
        {
            _afterAds.Invoke();
        }
    }

    public void ThreeXLevelIncomeReward()
    {
        var reward = CoinManager.Instance.levelsIncome * 2 / 3;
        CoinManager.Instance.AddCoins(reward);
        CoinManager.Instance.levelsIncome += reward;
        GameEvents.UpdateMoneyCount.Invoke();
    }

    public void AddMoney(int amount)
    {
        CoinManager.Instance.AddCoins(amount);
        GameEvents.UpdateMoneyCount.Invoke();
    }

    public void SkipLevel()
    {
        GameEvents.ResetLevelEvent.Invoke();
    }
}