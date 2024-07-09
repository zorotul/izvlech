﻿using System;
using blocks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    public class LevelsControl : MonoBehaviour
    {
        [SerializeField] private GameObject[] _levels;

        private GameObject _activeLevel;
        
        public static LevelsControl Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        public void LoadFirstLevel()
        {
            LoadLevel(0);
        }

        public void RestartLevel()
        {
            Destroy(_activeLevel);
            LoadLevel(0);
        }

        public void LoadNextLevel()
        {
            Destroy(_activeLevel);
            LoadLevel(1);
        }

        private void LoadLevel(int index)
        {
            GameDataManager.AddLevel(index);
            if (GameDataManager.GetLevel() >= _levels.Length)
            {
                _activeLevel = Instantiate(_levels[Random.Range(0, _levels.Length)], Vector3.zero, Quaternion.identity);
                return;
            }
            _activeLevel = Instantiate(_levels[GameDataManager.GetLevel()], Vector3.zero, Quaternion.identity);
        }
    }
}