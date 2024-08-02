using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProperties : MonoBehaviour
{
    /// <summary>
    /// -- C.Love -- this scriptis where all player values are stored such as money and swing and also all money donation methods from investment favour
    /// </summary>
    [SerializeField] public float money;
    [SerializeField] private float startMoney = 20f;
    [SerializeField] private GameObject cantAfford;
    public float chosenStartSwing;
    private InvestmentManager investmentManager => GameObject.Find("Investment").GetComponent<InvestmentManager>();
    private float investmentFavour => investmentManager.investmentFavour;
    private float[] investmentBrackets = {100, 200, 300};
    public float giftAmount;
    public float giftProbability; 
    // C.Love - this method is called at the start of the game and creates references to all key objects
    void Start()
    {
        money = startMoney;
        chosenStartSwing = variableStore.swing; // WW
        Debug.Log(chosenStartSwing);
    }
    // C.Love - this function is there to be called when purchasing purchasables
    public void Purchase(float amount)
    {
        money -= amount;    
    }
    // C.Love - this sets the gift value and probability based off of investment favour brackets
    public void LobbyingProperties()
    {
        if (investmentFavour >= investmentBrackets[0] && investmentFavour < investmentBrackets[1])
        {
            giftAmount = 100;
            giftProbability = 0.1f;
        }
        else if (investmentFavour >= investmentBrackets[1] && investmentFavour < investmentBrackets[2])
        {
            giftAmount = 150;
            giftProbability = 0.4f;
        }
        else if (investmentFavour >= investmentBrackets[2])
        {
            giftAmount = 250;
            giftProbability = 0.6f;
        }
        else
        {
            giftAmount = 0;
            giftProbability = 0f;
        }
    }
    // C.Love - this outputs the gift amount based off of the gift probability
    private float LobbyingOutputPT()
    {
        if (Random.value < giftProbability)
        {
            return giftAmount;
        }
        else
        {
            return 0f;
        }
    }
    // C.Love - this calls the lobbying methods at the start of each turn
    public void StartTurn()
    {
        LobbyingProperties();
        money += LobbyingOutputPT();
    }



}
