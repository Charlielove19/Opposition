
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
public class PurchaseController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    /// <summary>
    /// -- C.Love -- This script is applied to all purchasables to control their mechanics
    /// </summary>
    private Vector2 offset; 
    private float clickScaleUpAmount= 1.05f;
    private Vector3 defaultScale => transform.localScale;
    private Vector3 startPos;
    private GameObject hex;
    private UI_Controller uIController => GameObject.Find("UI Manager").GetComponent<UI_Controller>();
    public float cost;
    private PlayerProperties playerProps => GameObject.Find("Player").GetComponent<PlayerProperties>();
    public bool purchaseActive;
    private Vector3 dragStartOffset;

   
    /// C.Love - these methods check whether a tile already has the purchasable on them.
    bool CheckPurchaseActive()
    {
        
        if (gameObject.name == "OfficeButton" && hex.GetComponent<HexPurchased>().officeActive == true)
        {
            purchaseActive = true;
            return true;
        }
        else if (gameObject.name == "OnlineAd" && hex.GetComponent<HexPurchased>().onlineAdActive == true )
        {
            purchaseActive = true;
            return true;
        }
        else if (gameObject.name == "PaperAd" && hex.GetComponent<HexPurchased>().paperAdActive == true )
        {
            purchaseActive = true;
            return  true;
        }
        else if (gameObject.name == "Canvasing" && hex.GetComponent<HexPurchased>().canvasingActive == true)
        {
            purchaseActive = true;
            return true;
        }
        else
        {
            purchaseActive = false;
            return false;
        }
        
    }
    void SetPurchasedTrue()
    {
        if (hex != null)
        {
            if (gameObject.name == "OfficeButton" && purchaseActive != true)
                {
                    hex.GetComponent<HexPurchased>().BuyOffice();
                }
            else if (gameObject.name == "OnlineAd" && purchaseActive != true )
                {
                    hex.GetComponent<HexPurchased>().BuyOnlineAd();
                }
            else if (gameObject.name == "PaperAd" && purchaseActive != true)
                {
                    hex.GetComponent<HexPurchased>().BuyPaperAd();
                }
            else if (gameObject.name == "Canvasing" && purchaseActive != true )
                {
                    hex.GetComponent<HexPurchased>().BuyCanvasing();
                }
        }
        
        

    }

    /// C.Love - this allows the purchasables to access the hex tile which is moused over. Is called on HexBehaviour.cs
    public void SetCurrentHex(GameObject newHex)
    {
        
        hex = newHex;
        if (hex!= null)
        {
            HexBehaviour hexBehaviour = hex.GetComponent<HexBehaviour>();
            HexProperties hexProperties = hex.GetComponent<HexProperties>();
        }
        
        
    }

    

    /// C.Love - these methods control the drag behaviour of the purchasables
    public void OnBeginDrag(PointerEventData eventData)

    {   
        startPos = transform.position;

        dragStartOffset = GameObject.Find("BuyOptionsPanel").transform.position - startPos;

        transform.localScale = defaultScale*clickScaleUpAmount;
        offset = eventData.position - new Vector2(transform.position.x, transform.position.y);

        GameObject.Find("Buy Options").GetComponent<buyOptionsHover>().KeepOpen(true);
        
    }
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position - offset;   
    }  
    public void OnEndDrag(PointerEventData eventData)
    {
        
        transform.localScale = defaultScale*(1/clickScaleUpAmount);
        offset = Vector2.zero;
        
        
        SetPurchasedTrue();
        transform.position = startPos;
        
        GameObject.Find("Buy Options").GetComponent<buyOptionsHover>().KeepOpen(false);
        GameObject.Find("Buy Options").GetComponent<buyOptionsHover>().OnMouseExit();
        
        
        
    }

    /// C.Love - This updates once per frame to check if a tile already has a purchasable on it
    void Update()
    {
        if (hex != null)
        {
            CheckPurchaseActive();
        }
    }
}
