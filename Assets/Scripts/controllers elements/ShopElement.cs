using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ShopElement : MonoBehaviour
{
    public Sprite ShipSprite;
    public TMP_Text CostBuyText;
    public TMP_Text UpgradeOneText;
    public TMP_Text UpgradeTwoText;
    public TMP_Text UpgradeThreeText;
    public TMP_Text UpgradeFourText;
    public GameObject ButtonBuy;
    public GameObject ButtonPlay;
    public GameObject ButtonChoose;
    public int CostBuy;

    public UnityAction OnShopElementPlay;

    public void BuyComplete()
    {
        ButtonBuy.SetActive(false);
        ButtonChoose.SetActive(true);
    }

    public void Choose()
    {
        ButtonChoose.SetActive(false);
        ButtonPlay.SetActive(true);
    }

    public void Play()
    {
        OnShopElementPlay?.Invoke();
    }
}
