using System;
using System.Collections;
using System.Collections.Generic;
using Project.Audio;
using Project.Daily;
using Project.Level;
using Project.Spin;
using Project.UI;
using Project.Upgrades;
using UnityEngine;

namespace Project
{
    public class GameController : MonoBehaviour
    {
        #region Inspector variables

        [SerializeField] private LevelController _levelController;
        [SerializeField] private UIController _uiController;
        [SerializeField] private UpgradeController _upgradeController;
        [SerializeField] private AudioController _audioController;
        [SerializeField] private DailyController _dailyController;
        [SerializeField] private SpinController _spinController;
        [SerializeField] private ObjectPool.ObjectPool _objectPool;

        #endregion Inspector variables

        #region properties

        public int CountBaseMoney { get; private set; }
        public int MoneyByClick { get; private set; }
        public int Money { get; private set; }

        #endregion properties
        
        #region Unity functions

        private void Start()
        {
            Init();
        }

        #endregion Unity functions

        #region private functions

        private void Init()
        {
            SetActions();
        }

        private void SetActions()
        {
            _uiController.SetMoneyText(CountBaseMoney.ToString());
            _dailyController.OnDailyRewardReady += () => _uiController.SetDailyReward(_dailyController.DailyRewards);
            _dailyController.OnDailyRewardClick += () => AddMoney(_dailyController.ClaimDailyReward());
            _dailyController.OnDailyRewardClick += () => _uiController.SetMoneyText(Money.ToString());
            _upgradeController.OnMoneyEnough += CheckMoney;
            _upgradeController.OnUpgradeComplete += () => AddMoneyByClick(1);
            _upgradeController.OnMoneyClick += () => AddMoney(MoneyByClick);
        }
        
        private void AddMoneyByClick(int value) => MoneyByClick += value;
        private void AddMoney(int value) => Money += value;

        private void LoseMoney(int value)
        {
            if (value > Money)
            {
                Money -= value;
            }
        }

        private bool CheckMoney(int value) => Money >= value;

        #endregion private functions
    }
}
