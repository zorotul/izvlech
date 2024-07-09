﻿using System;
using System.Collections.Generic;
using ShopMechanics;
using UnityEngine;
using YG;

namespace blocks
{
    public enum EveryDayRewardState
    {
        Blocked,
        CanGet,
        WasGotten
    }
    
    #region Class: ShopData
    [System.Serializable]
    public class ShopData
    {
        public List<int> purchaseAsCharacters = new();

        public bool GetPurchaseAsCharacter(int index) 
            => purchaseAsCharacters.Contains(index);

        public void AddPurchaseCharacter(int index) 
            => purchaseAsCharacters.Add(index);
    }
    #endregion
    
    [System.Serializable]
    public class PlayerData
    {
        public int coins;
        public int level;
        public int heightPrice;
        public int widthPrice;
        public int activeWidth;
        public int activeHeight;
        public EveryDayRewardState[] everyDayRewardsInfo;
        public int lastCallDate;

        public int selectCharacterIndex;
        public int secondSelectCharacterIndex;

        public int GetCoin
        {
            get { return coins; }
        }
        public void AddCoins(int coin) 
            => coins += coin;

        public void SpendCoins(int coin) 
            => coins -= coin;

        public bool CanSpenCoin(int coin) 
            => this.coins >= coin;

        // Level
        public int GetLevel()
        {
            return level;
        }

        public void AddLevel(int value) 
            => level += value;

        public void SetSelectChracter(int index) 
            => selectCharacterIndex = index;

        public int GetSelectCharacter() 
            => selectCharacterIndex;
    }
    
    public static class GameDataManager
    {
        private static PlayerData _playerData;
        private static ShopData _shopData;

        public static void InitData()
        {
            _playerData = JsonUtility.FromJson<PlayerData>(YandexGame.savesData.playerData);

            _shopData = JsonUtility.FromJson<ShopData>(YandexGame.savesData.shopData);
            if (_playerData == null)
            {
                int currentCoin = 0;
                int currentLevel = 0;
                int currentSkin = 0;
                _playerData = new PlayerData
                {
                    coins = currentCoin,
                    level = currentLevel,
                    selectCharacterIndex = currentSkin,
                    heightPrice = 20,
                    widthPrice = 10
                };
                SavePlayerData();
            }
            if(_shopData == null)
            {
                int curreentIndex = 0;
                _shopData = new ShopData
                {
                    purchaseAsCharacters = new List<int> { curreentIndex }
                };
                SaveShopData();
            }
        }

        public static void SavePlayerData()
        {
            var data = JsonUtility.ToJson(_playerData);
            YandexGame.savesData.playerData = data;
            YandexGame.SaveProgress();
        }

        public static PlayerData GetPlayerData()
        {
            return _playerData; 
        }

        private static void SaveShopData()
        {
            var data = JsonUtility.ToJson(_shopData);
            YandexGame.savesData.shopData = data;
            YandexGame.SaveProgress();
        }

        public static void AddCoin(int coin)
        {
            YandexGame.savesData.allMoney += coin;
            YandexGame.NewLeaderboardScores("Score", YandexGame.savesData.allMoney);
            YandexGame.GetLeaderboard("Score",
                Int32.MaxValue, Int32.MaxValue, Int32.MaxValue, "nonePhoto");
            _playerData.AddCoins(coin);
            SavePlayerData();
        }

        public static void SpendCoin(int coin)
        {
            _playerData.SpendCoins(coin) ;
            SavePlayerData() ;
        }
        
        public static int GetCoin() 
            => _playerData.GetCoin;
        public static bool CanSpenCoin(int value) 
            => _playerData.CanSpenCoin(value);



        public static int GetLevel() 
            => _playerData.GetLevel();

        public static void AddLevel(int value)
        {
            YandexMetrica.Send("Level" + _playerData.GetLevel());
            _playerData.AddLevel(value);
            SavePlayerData();
        }

        public static void SetCharacterIndex(int index)
        {
            _playerData.SetSelectChracter(index);
            SavePlayerData() ;
        }

        public static int GetCharacterIndex()
        { 
            return _playerData.GetSelectCharacter();
        }

        public static bool GetPurchaseAsCharacter(int index)
        {
            return _shopData.GetPurchaseAsCharacter(index);
        }

        public static void AddPurchaseCharacter(int index)
        {
            _shopData.AddPurchaseCharacter(index);
            SaveShopData();
        }
    }
}