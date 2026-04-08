using System.Collections.Generic;
using UnityEngine;

public class CardMaker : MonoBehaviour
{
    [SerializeField] private GameObject leftCard;
    [SerializeField] private GameObject middleCard;
    [SerializeField] private GameObject rightCard;

    public void SetupCards(Wave wave)
    {
        //Set the card data for each card based on the shop cards in the enemyWave
        leftCard.GetComponent<ShopCard>().SetCardData(wave.shopCards[0]);
        middleCard.GetComponent<ShopCard>().SetCardData(wave.shopCards[1]);
        rightCard.GetComponent<ShopCard>().SetCardData(wave.shopCards[2]);
    }
}