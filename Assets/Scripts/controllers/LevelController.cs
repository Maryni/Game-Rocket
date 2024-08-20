using UnityEngine;
using UnityEngine.Events;

namespace Project.Level
{
    public class LevelController : MonoBehaviour
    {
        #region Inspector variables

        [SerializeField] private RocketInfo _rocketInfo;
        //[SerializeField] private int _countBoost;
        //[SerializeField] private int _maxBoost;

        #endregion Inspector variables

        #region private variables

        private bool isPlaying;
        private bool isReloading;
        private int countPerSecond;

        #endregion private variables

        #region public variables

        public UnityAction OnMoneyGet;
        public UnityAction OnClickBoost;
        public UnityAction OnUpdateBoost;
        public UnityAction OnUpdateFuel;
        public RocketInfo CurrentRocketInfo => _rocketInfo;

        #endregion private variables

        #region Unity functions

        private void FixedUpdate()
        {
            SpendFuel();
            ReloadFuel();
        }

        #endregion Unity functions

        #region public functions

        public void Init()
        {
            _rocketInfo.FuelCurrentCount = _rocketInfo.FuelMaxCount;
            _rocketInfo.FuelCountClicks = _rocketInfo.FuelCountMaxClicks;
            _rocketInfo.BoostCountClicks = _rocketInfo.BoostCountMaxClicks;
            OnUpdateBoost?.Invoke();
            OnUpdateFuel?.Invoke();
            EnableGame();
        }
        
        public void Click()
        {
            if (_rocketInfo.FuelCurrentCount > 0)
            {
                _rocketInfo.FuelCurrentCount--;
                OnMoneyGet?.Invoke();
                OnUpdateFuel?.Invoke();
            }
            else
            {
                isReloading = true;
            }
        }
        
        public void ClickBoost()
        {
            if (_rocketInfo.BoostCurrentCount > 0)
            {
                OnClickBoost?.Invoke();
                OnUpdateBoost?.Invoke();
                _rocketInfo.BoostCurrentCount--;
            }
        }

        public void ClickFuel()
        {
            if (_rocketInfo.FuelCountClicks > 0)
            {
                _rocketInfo.FuelCurrentCount = _rocketInfo.FuelMaxCount;
                _rocketInfo.FuelCountClicks--;
                OnUpdateFuel?.Invoke();
            }
        }
        
        public void EnableGame() => isPlaying = true;
        public void DisableGame() => isPlaying = false;

        #endregion public functions

        #region private functions

        private void SpendFuel()
        {
            if (isPlaying && !isReloading)
            {
                if (_rocketInfo.FuelCurrentCount > 0)
                {
                    _rocketInfo.CountToUse++;
                    if (_rocketInfo.CountToUse > 60)
                    {
                        _rocketInfo.FuelCurrentCount--;
                        OnMoneyGet?.Invoke();
                        OnUpdateFuel?.Invoke();
                        _rocketInfo.CountToUse = 0;
                    }
                }
                else
                {
                    isReloading = true;
                }
            }
        }

        public void ClickMeteor()
        {
            OnMoneyGet?.Invoke();
            OnMoneyGet?.Invoke();
            OnMoneyGet?.Invoke();
        }

        private void ReloadFuel()
        {
            if (isReloading)
            {
                _rocketInfo.CountToReload++;
                if (_rocketInfo.CountToReload > 60)
                {
                    _rocketInfo.FuelCurrentCount++;
                    _rocketInfo.CountToReload = 0;
                    OnUpdateFuel?.Invoke();
                }

                if (_rocketInfo.FuelCurrentCount == _rocketInfo.FuelMaxCount)
                {
                    isReloading = false; //yea, cuz clicking all day long it's terrible idea (imho)
                }
            }
        }

        #endregion private functions
    }

    [System.Serializable]
    public class RocketInfo
    {
        public int BoostMaxCount;
        public int BoostCurrentCount;
        public int BoostCountClicks;
        public int BoostCountMaxClicks;
        public int FuelMaxCount;
        public int FuelCurrentCount;
        public int FuelCountClicks;
        public int FuelCountMaxClicks;
        public int CountToUse; //base 60 = 1s
        public int CountToReload;
        public int MultiplierBoost; //base 60 = 1s
    }
    
}