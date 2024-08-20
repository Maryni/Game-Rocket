using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Project.Upgrades
{
    public class UpgradeController : MonoBehaviour
    {
        #region Inspector variables

        [SerializeField] private Sprite _emptyCell;
        [SerializeField] private Sprite _fullCell;
        [SerializeField] private int _countUpgrades;
        [SerializeField] private List<UpgradeElement> _upgradeElements;

        #endregion Inspector variables

        #region public variables

        public Func<int, bool> OnMoneyEnough;
        public UnityAction<int> OnSpendMoney;
        public UnityAction OnUpgradeComplete;

        #endregion public variables

        #region private variables

        private UpgradeElement currentElement;

        #endregion private variables

        #region public functions

        public void ClickUpgrade(int id) //suppose to be 0..n-1
        {
            currentElement = _upgradeElements[id];
            var costUpgrade = currentElement.CostBaseUpgrade * (1 + currentElement.CountUpgrades);
            
            if (OnMoneyEnough.Invoke(costUpgrade))
            {
                if (currentElement.CountUpgrades < 10)
                {
                    OnSpendMoney?.Invoke(costUpgrade);
                    currentElement.CellImages[currentElement.CountUpgrades].sprite = _fullCell;
                    currentElement.CountUpgrades++;
                    OnUpgradeComplete?.Invoke();
                }
                else
                {
                    currentElement.TextCostUpgrades.transform.parent.parent.gameObject.SetActive(false);
                }
            }
        }

        #endregion public functions
    }
}