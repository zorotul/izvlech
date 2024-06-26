using System;
using System.Collections;
using blocks;
using ShopMechanics;
// using Platformer.Mechanics;
// using ShopMechanics;
using UnityEngine;
using YG;

namespace Flatformer.GameData
{
    public class YandexControl : MonoBehaviour
    {
        private void Awake()
        {
            StartCoroutine(YandexSDKEnabledCoroutine());
        }

        public IEnumerator YandexSDKEnabledCoroutine()
        {
            yield return new WaitUntil(() => YandexGame.SDKEnabled);
            YandexGame.InitEnvirData();
            GameDataManager.InitData();
            YandexGame.NewLeaderboardScores("Score", YandexGame.savesData.allMoney);
            YandexGame.GetLeaderboard("Score",
                Int32.MaxValue, Int32.MaxValue, 
                Int32.MaxValue, "nonePhoto");
            ShopManager.Instance.Init();
            InitMusicState.Instance.Init();
            GameManager.Instance.StartGame();
            // GameStartUI.Instance.Init();
        }
    }
}