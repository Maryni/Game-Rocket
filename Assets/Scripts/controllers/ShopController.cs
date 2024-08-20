using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Project.Shop
{
    public class ShopController : MonoBehaviour
    {
        [SerializeField] private List<ShopElement> _ships;

        public Func<int, bool> OnMoneyEnough;
        public ShopElement CurrentElement => currentElement;
        
        private ShopElement currentElement;
        
        public void SetAllShips(List<ShopElement> ships) => _ships = ships;
        
        public void BuyShopElement(int id)
        {
            if (id < _ships.Count)
            {
                currentElement = _ships[id];
                if (OnMoneyEnough(currentElement.CostBuy))
                {
                    
                }
            }
        }

        public void SetCurrentShop(int id) => currentElement = _ships[id];

        public void SetShopElementPlayAction(UnityAction action)
        {
            _ships.ForEach(x=>x.OnShopElementPlay += action);
        }
    }
}