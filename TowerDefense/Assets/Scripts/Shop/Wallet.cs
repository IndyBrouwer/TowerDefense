using TMPro;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currencyText;
    private int currencyAmount;

    [SerializeField] private AudioClip earnSound;
    [SerializeField] private AudioClip spendSound;

    public void AddCurrency(int income)
    {
        AudioManager.Instance.sfxManager.PlaySFX(earnSound);

        //Add incoming money to total money
        currencyAmount += income;

        //Set UI to new amount of money 
        currencyText.text = currencyAmount.ToString();
    }

    public void RemoveCurrency(int costs)
    {
        if (currencyAmount < costs)
        {
            Debug.LogWarning("Not enough currency to complete the transaction!");
            return;
        }

        //Play currency sound effect
        AudioManager.Instance.sfxManager.PlaySFX(spendSound);

        //Remove costs from total money
        currencyAmount -= costs;

        //Set UI to new amount of money 
        currencyText.text = currencyAmount.ToString();
    }

    public int GetCurrencyAmount()
    {
        return currencyAmount;
    }
}