
using System;
using System.Collections.Generic;
using UnityEngine;

public class HexPurchased : MonoBehaviour
{
    /// <summary>
    /// -- C.Love -- this script applies purchasable effects to the current tile and is applied on each hex GameObject
    /// </summary>
    public bool officeActive;
    public bool onlineAdActive;
    public bool paperAdActive;
    public bool canvasingActive;
    private HexProperties hexProps => gameObject.GetComponent<HexProperties>();
    private GameManager gameManager => GameObject.Find("GameManager").GetComponent<GameManager>();
    private float officeCost => GameObject.Find("OfficeButton").GetComponent<PurchaseController>().cost;
    private float onlineAdCost => GameObject.Find("OnlineAd").GetComponent<PurchaseController>().cost;
    private float paperAdCost => GameObject.Find("PaperAd").GetComponent<PurchaseController>().cost;
    private float canvasingCost => GameObject.Find("Canvasing").GetComponent<PurchaseController>().cost;
    private PlayerProperties plyrProps => GameObject.Find("Player").GetComponent<PlayerProperties>();
    [SerializeField] private GameObject officeCone;
    [SerializeField] private GameObject onlineAdSymbol;
    [SerializeField] private GameObject paperAdSymbol;
    [SerializeField] private GameObject canvasingSymbol;
    [SerializeField] private GameObject cantAfford;
    private float onlineAdYoungTurnoutIncreaseAmount = 0.2f;
    private float onlineAdYoungFavourIncreaseAmount = 50;
    private int onlineAdHexRange = 3; 
    private float paperAdProfTurnoutIncreaseAmount = 0.2f;
    private float paperAdProfFavourIncreaseAmount = 40;
    private int paperAdHexRange = 2;
    private float canvasingOldTurnoutIncreaseAmount = 0.2f;
    private float canvasingOldFavourIncreaseAmount = 70;
    private int canvasingHexRange = 1;
    // C.Love - this instantiates the cant afford icon at the current mouse position
    private void CantAffordInstance()
    { 
        Vector3 mousePos = Input.mousePosition;
        GameObject cantAffordParent = GameObject.Find("UI");
        Instantiate(cantAfford,mousePos, Quaternion.identity, cantAffordParent.transform);
    }
    // C.Love - this method applies all the Office effects to a hex if it is not already purchased and if the player can afford it
    public void BuyOffice()
    {
        if (plyrProps.money>=officeCost)
        {
            plyrProps.Purchase(officeCost);
            officeActive = true;
            officeCone.SetActive(true);
            hexProps.plyrLoyaltyBottomCap += 2;
            hexProps.plyrLoyaltyTopCap += 1;
            if (hexProps.plyrLoyalty<hexProps.plyrLoyaltyBottomCap)
            {
                hexProps.plyrLoyalty = hexProps.plyrLoyaltyBottomCap;
            }
            hexProps.plyrLoyaltyMultiplier += 1;
        }
        else
        {
            CantAffordInstance();
        }
    }
    // C.Love - this method applies all the OnlineAd effects to a hex if it is not already purchased and if the player can afford it
    public void BuyOnlineAd()
    {
        if (plyrProps.money>=onlineAdCost)
        {
            plyrProps.Purchase(onlineAdCost);
            List<GameObject> tilesinrange = gameManager.ListHexesWithinRange(gameObject, onlineAdHexRange);
            for (int i = 0; i<tilesinrange.Count;i++)
                {
                    if (tilesinrange[i] != null)
                    {
                        HexPurchased currentHexPurch = tilesinrange[i].GetComponent<HexPurchased>();
                        HexProperties currentHexProps = tilesinrange[i].GetComponent<HexProperties>();
                        if (currentHexPurch.onlineAdActive == false)
                        {
                            currentHexProps.turnouts["young"] = (currentHexProps.turnouts["young"] + onlineAdYoungTurnoutIncreaseAmount)/(1+onlineAdYoungTurnoutIncreaseAmount);
                            currentHexProps.favours["young"] += onlineAdYoungFavourIncreaseAmount;
                        }
                        currentHexPurch.onlineAdActive = true;
                        currentHexPurch.onlineAdSymbol.SetActive(true);
                    }
                    
                }   

        }
        else
        {
            CantAffordInstance();
        }
    }
    // C.Love - this method applies all the PaperAd effects to a hex if it is not already purchased and if the player can afford it
    public void BuyPaperAd()
    {
        if (plyrProps.money>=paperAdCost)
        {
            plyrProps.Purchase(paperAdCost);
            List<GameObject> tilesinrange = gameManager.ListHexesWithinRange(gameObject, paperAdHexRange);
            for (int i = 0; i<tilesinrange.Count;i++)
                {
                    if (tilesinrange[i] != null)
                    {
                        HexPurchased currentHexPurch = tilesinrange[i].GetComponent<HexPurchased>();
                        HexProperties currentHexProps = tilesinrange[i].GetComponent<HexProperties>();
                        if (currentHexPurch.paperAdActive == false)
                        {
                            currentHexProps.turnouts["prof"] = (currentHexProps.turnouts["young"] + paperAdProfTurnoutIncreaseAmount)/(1+paperAdProfTurnoutIncreaseAmount);
                            currentHexProps.favours["prof"] += paperAdProfFavourIncreaseAmount;
                        }
                        currentHexPurch.paperAdActive = true;
                        currentHexPurch.paperAdSymbol.SetActive(true);
                    }
                    
                }   

        }
        else
        {
            CantAffordInstance();
        }
    }
    // C.Love - this method applies all the Canvassing effects to a hex if it is not already purchased and if the player can afford it
    public void BuyCanvasing()
    {
        if (plyrProps.money>=canvasingCost)
        {
            plyrProps.Purchase(canvasingCost);
            List<GameObject> tilesinrange = gameManager.ListHexesWithinRange(gameObject, canvasingHexRange);
            List<int> canvasTrail = CreateCanvasTrail();
            foreach (int i in canvasTrail)
                {
                    if (i < tilesinrange.Count)
                    {
                        if (tilesinrange[i] != null)
                        {
                            HexPurchased currentHexPurch = tilesinrange[i].GetComponent<HexPurchased>();
                            HexProperties currentHexProps = tilesinrange[i].GetComponent<HexProperties>();
                            if (currentHexPurch.canvasingActive == false)
                            {
                                currentHexProps.turnouts["old"] = (currentHexProps.turnouts["young"] + canvasingOldTurnoutIncreaseAmount)/(1+canvasingOldTurnoutIncreaseAmount);
                                currentHexProps.favours["old"] += canvasingOldFavourIncreaseAmount;
                            }
                            currentHexPurch.canvasingActive = true;
                            currentHexPurch.canvasingSymbol.SetActive(true);
                        }
                    }
                    
                }   

        }
        else
        {
            CantAffordInstance();
        }
    }
    // C.Love - this method provides a list of 3 random hexes within one tile for canvassing
    private List<int> CreateCanvasTrail()
    {
        List<int> canvasTrailPossibilities = new List<int>{0, 1, 2, 4, 5, 6};
        List<int> canvasTrail = new List<int>();
        int firstIndex = UnityEngine.Random.Range(0, canvasTrailPossibilities.Count);
        int firstNumber = canvasTrailPossibilities[firstIndex];
        canvasTrailPossibilities.RemoveAt(firstIndex);
        int secondIndex = UnityEngine.Random.Range(0, canvasTrailPossibilities.Count);
        int secondNumber = canvasTrailPossibilities[secondIndex];
        canvasTrail.Add(firstNumber);
        canvasTrail.Add(secondNumber);
        canvasTrail.Add(3);
        Debug.Log("canvasTrail indexes: "+ firstNumber + secondNumber + 3);
        return canvasTrail;

    }
    // C.Love - this method was used to test all above functions by colouring relevat ties green
    public void ColorTilesGreenInRange(int n)
    {
        List<GameObject> tilesinrange = gameManager.ListHexesWithinRange(gameObject, n);
        for (int i = 0; i<tilesinrange.Count;i++)
        {
            if (tilesinrange[i] != null)
            {
                tilesinrange[i].GetComponent<HexProperties>().SetTilesGreen();
            }
            
        }
    }
}
