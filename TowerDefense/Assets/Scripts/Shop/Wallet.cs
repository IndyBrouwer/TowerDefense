using TMPro;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currencyText;
    private int currencyAmount;

    public void AddCurrency(int income)
    {
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

        //Remove costs from total money
        currencyAmount -= costs;

        //Set UI to new amount of money 
        currencyText.text = currencyAmount.ToString();
    }
}