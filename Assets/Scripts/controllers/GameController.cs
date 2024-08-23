using Project.Audio;
using Project.Daily;
using Project.Level;
using Project.Shop;
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
        [SerializeField] private ShopController _shopController;
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
            CountBaseMoney = 10000;
            MoneyByClick = 1;
            Money = CountBaseMoney;
            Init();
        }

        #endregion Unity functions

        #region private functions

        private void Init()
        {
            SetActions();
            _levelController.Init();
        }

        private void SetActions()
        {
            _spinController.OnSpinReward += () => AddMoney(Random.Range(0, 1000));
            
            _shopController.SetAllShips(_uiController.GetAllShopElements());
            _shopController.OnMoneyEnough += CheckMoney;
            _shopController.SetShopElementPlayAction(() => _uiController.SetPlayerRocketSprite(_shopController.CurrentElement.ShipSprite));
            
            _uiController.SetMoneyText(CountBaseMoney.ToString());
            _uiController.SetGameFuelValues(
                _levelController.CurrentRocketInfo.FuelCurrentCount.ToString(),
                _levelController.CurrentRocketInfo.FuelMaxCount.ToString());
            
            _dailyController.OnDailyRewardReady += () => _uiController.SetDailyReward(_dailyController.DailyRewards);
            _dailyController.OnDailyRewardClick += () => AddMoney(_dailyController.ClaimDailyReward());
            _dailyController.OnDailyRewardClick += () => _uiController.SetMoneyText(Money.ToString());
            _dailyController.OnDailyRewardClick += () => _uiController.SetDailyRewardClaimed(_dailyController.DailyRewards);
            
            _upgradeController.OnMoneyEnough += CheckMoney;
            _upgradeController.OnSpendMoney += LoseMoney;
            _upgradeController.OnUpgradeComplete += () => AddMoneyByClick(1);
            _upgradeController.OnUpgradeComplete += () =>_uiController.SetMoneyText(Money.ToString());
            
            _levelController.OnMoneyGet += () => AddMoney(MoneyByClick);
            _levelController.OnMoneyGet += () =>_uiController.SetMoneyText(Money.ToString());
            _levelController.OnClickBoost += () => AddMoney(MoneyByClick * _levelController.CurrentRocketInfo.MultiplierBoost);
            _levelController.OnUpdateBoost += () => _uiController.SetBoostCurrentText(_levelController.CurrentRocketInfo.BoostCountClicks.ToString());
            _levelController.OnUpdateBoost += () => _uiController.SetBoostMaxText(_levelController.CurrentRocketInfo.BoostCountMaxClicks.ToString());
            _levelController.OnUpdateFuel += () => _uiController.SetFuelCurrentText(_levelController.CurrentRocketInfo.FuelCountClicks.ToString());
            _levelController.OnUpdateFuel += () => _uiController.SetFuelMaxText(_levelController.CurrentRocketInfo.FuelCountMaxClicks.ToString());
            _levelController.OnUpdateFuel += () =>_uiController.SetGameFuelValues(
                _levelController.CurrentRocketInfo.FuelCurrentCount.ToString(),
                _levelController.CurrentRocketInfo.FuelMaxCount.ToString());
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
