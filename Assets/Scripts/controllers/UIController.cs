using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Project.UI
{
    public class UIController : MonoBehaviour
    {
        #region Inspector variables

        [Header("Base UI"),SerializeField] private GameObject _game;
        [SerializeField] private GameObject _menu;
        [SerializeField] private GameObject _shop;
        [SerializeField] private GameObject _boost;
        [SerializeField] private GameObject _settings;
        [SerializeField] private GameObject _dailyReward;
        [SerializeField] private GameObject _spin;
        [SerializeField] private List<TMP_Text> _money;
        [Header("Daily Reward"), SerializeField] private List<DailyRewardUI> _allRewardUI;
        [SerializeField] private Sprite _rewardClaimed;
        [SerializeField] private Sprite _rewardUnclaimed;
        [Header("Shop"), SerializeField] private List<ShopElement> _shopElements;
        [Header("Boost"), SerializeField] private TMP_Text _boostCountCurrentText;
        [SerializeField] private TMP_Text _boostCountMaxText;
        [SerializeField] private TMP_Text _fuelCountCurrentText;
        [SerializeField] private TMP_Text _fuelCountMaxText;
        [Header("Game"), SerializeField] private TMP_Text _fuelGameCurrent;
        [SerializeField] private TMP_Text _fuelGameMax;
        [SerializeField] private Slider _sliderFuel;
        [SerializeField] private Image _mainShipImage;
        
        #endregion Inspector variables

        #region public functions

        public List<ShopElement> GetAllShopElements() => _shopElements;
        
        public void ChangeViewGame() => ChangeView(_game);
        public void ChangeViewMenu() => ChangeView(_menu);
        public void ChangeViewShop() => ChangeView(_shop);
        public void ChangeViewSettings() => ChangeView(_settings);
        public void ChangeViewBoost() => ChangeView(_boost);
        public void ChangeViewDailyReward() => ChangeView(_dailyReward);
        public void ChangeViewSpin() => ChangeView(_spin);

        public void SetFuelMaxText(string value) => _fuelCountMaxText.text = value;
        public void SetFuelCurrentText(string value) => _fuelCountCurrentText.text = value;
        public void SetBoostMaxText(string value) => _boostCountMaxText.text = value;
        public void SetBoostCurrentText(string value) => _boostCountCurrentText.text = value;
        public void SetPlayerRocketSprite(Sprite sprite) => _mainShipImage.sprite = sprite;
        
        public void SetGameFuelValues(string fuelCurrent, string fuelMax)
        {
            _fuelGameMax.text = fuelMax;
            _fuelGameCurrent.text = fuelCurrent;
            _sliderFuel.maxValue = float.Parse(fuelMax);
            _sliderFuel.value = float.Parse(fuelCurrent);
        }
        
        public void SetDailyRewardAvailable(List<DailyReward> rewards)
        {
            for (int i=0; i< rewards.Count; i++)
            {
                if (rewards[i].IsAvailable)
                {
                    _allRewardUI[i].BackgroundImage.sprite = _rewardClaimed;
                }
            }
        }

        public void SetDailyReward(List<DailyReward> rewards)
        {
            for (int i=0; i< rewards.Count; i++)
            {
                _allRewardUI[i].RewardText.text = rewards[i].Reward.ToString();
            }
        }

        public void SetMoneyText(string value)
        {
            var newValue = ConvertToKFormat(value);
            foreach (var item in _money)
            {
                SetText(item, newValue);
            }
        }

        #endregion public functions

        #region private functions

        private void ChangeView(GameObject item) => item.SetActive(!item.activeSelf);
        private void SetText(TMP_Text text, string value) => text.text = value;
        
        private string ConvertToKFormat(string input)
        {
            if (long.TryParse(input, out long number))
            {
                if (number >= 1000000)
                {
                    return $"{number / 1000000}KK";
                }
                else if (number >= 100000)
                {
                    return $"{number / 1000}K";
                }
                else
                {
                    return number.ToString("N0");
                }
            }

            return input;
        }

        #endregion private functions
    }
}

