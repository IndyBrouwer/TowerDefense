using TMPro;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currencyText;
    [SerializeField] private int maxCurrencyAmount = 99999;
    private int currencyAmount;

    [SerializeField] private AudioClip earnSound;
    [SerializeField] private AudioClip spendSound;
    public AudioClip noFundsSound;

    public void AddCurrency(int income)
    {
        AudioManager.Instance.sfxManager.PlaySFX(earnSound);

        //Add incoming money to total money
        currencyAmount += income;
        
        //Clamp currency amount to maxCurrencyAmount
        if (currencyAmount > maxCurrencyAmount)
        {
            currencyAmount = maxCurrencyAmount;
        }

        //Set UI to new amount of money 
        currencyText.text = currencyAmount.ToString();
    }

    public void RemoveCurrency(int costs)
    {
        if (currencyAmount < costs)
        {
            Debug.LogWarning("Not enough currency to complete the purchase!");
            AudioManager.Instance.sfxManager.PlaySFX(noFundsSound);
            return;
        }

        //Play currency sound effect
        AudioManager.Instance.sfxManager.PlaySFX(spendSound);

        //Remove costs from total money
        currencyAmount -= costs;

        //Set UI to new amount of money 
        currencyText.text = currencyAmount.ToString();
    }

    public bool CanAfford(int costs)
    {
        return currencyAmount >= costs;
    }

    public int GetCurrencyAmount()
    {
        return currencyAmount;
    }
}