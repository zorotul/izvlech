using blocks;
using ShopMechanics;
using System;
using System.Collections;
using UI;
using UnityEngine;
using YG;

namespace Flatformer.GameData
{
    public class YandexControl : MonoBehaviour
    {
#if UNITY_EDITOR 
        [SerializeField] private bool _needResetData = true;
#endif
        private void Awake()
        {
            StartCoroutine(YandexSDKEnabledCoroutine());
        }

        public IEnumerator YandexSDKEnabledCoroutine()
        {
            yield return new WaitUntil(() => YandexGame.SDKEnabled);
#if UNITY_EDITOR 
            if(_needResetData) YandexGame.ResetSaveProgress();
#endif
            MultiTextUI.lang = YandexGame.lang;
            YandexGame.InitEnvirData();
            GameDataManager.InitData();
            ShopManager.Instance.Init();
            InitMusicState.Instance.Init();
            GameManager.Instance.StartGame();
            CoinManager.Instance.UpdateTexts();
            EveryDayRewardUI.Instance.Init();
        }
    }
}