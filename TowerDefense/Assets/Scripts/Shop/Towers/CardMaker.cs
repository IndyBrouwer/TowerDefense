using System.Collections.Generic;
using UnityEngine;

public class CardMaker : MonoBehaviour
{
    [SerializeField] private GameObject leftCard;
    [SerializeField] private GameObject middleCard;
    [SerializeField] private GameObject rightCard;

    public void SetupCards(Wave waveIndex, int currentWaveIndex)
    {
        //Set the card data for each card based on the shop cards in the enemyWave
        leftCard.GetComponent<ShopCard>().SetCardData(waveIndex.shopCards[0], currentWaveIndex);
        middleCard.GetComponent<ShopCard>().SetCardData(waveIndex.shopCards[1], currentWaveIndex);
        rightCard.GetComponent<ShopCard>().SetCardData(waveIndex.shopCards[2], currentWaveIndex);
    }
}