using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Project.Upgrades
{
    public class UpgradeController : MonoBehaviour
    {
        [SerializeField] private Sprite _emptyCell;
        [SerializeField] private Sprite _fullCell;
        [SerializeField] private int _countUpgrades;
        [SerializeField] private List<UpgradeElement> _upgradeElements;
        
        public Func<int, bool> OnMoneyEnough;
        public UnityAction OnMoneyClick;
        public UnityAction OnUpgradeComplete;
        
        private UpgradeElement currentElement;

        public void ClickIdle()
        {
            OnMoneyClick?.Invoke();
        }

        public void ClickUpgrade(int id) //suppose to be 0..n-1
        {
            currentElement = _upgradeElements[id];
            var costUpgrade = currentElement.CostBaseUpgrade * (1 + currentElement.CountUpgrades);
            
            if (OnMoneyEnough.Invoke(costUpgrade))
            {
                if (currentElement.CountUpgrades < 10)
                {
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
    }
}