using System;
using System.Collections;
using System.Collections.Generic;
using Project.Audio;
using Project.Level;
using Project.UI;
using Project.Upgrades;
using UnityEngine;

namespace Project
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private LevelController _levelController;
        [SerializeField] private UIController _uiController;
        [SerializeField] private UpgradeController _upgradeController;
        [SerializeField] private AudioController _audioController;
        [SerializeField] private ObjectPool.ObjectPool _objectPool;

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            SetActions();
        }

        private void SetActions()
        {
            
        }
    }
}
