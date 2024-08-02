using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HexBehaviour : MonoBehaviour
{
    /// <summary>
    /// C.Love this script is applied on all hexagons and calls all script's SetCurrentHex() method that require it for moused over hex interaction
    /// </summary>
    private HexProperties hexProp;
    [SerializeField] private float mouseOverRaiseAmount = 2f;
    [SerializeField] private GameObject uIControllerObject;
    private GameObject officeButton;
    private GameObject onlineAd;
    private GameObject paperAd;
    private GameObject canvasing;
    private Vector3 startPos;
    public float currentTileSwing;
    // C.Love - this method is called once at the start of the game and declares all reference objects required
    void Start()
    {
        hexProp = GetComponent<HexProperties>();
        startPos = transform.position;
        officeButton = GameObject.Find("OfficeButton");
        onlineAd = GameObject.Find("OnlineAd");
        paperAd = GameObject.Find("PaperAd");
        canvasing = GameObject.Find("Canvasing");
    }
    // C.Love - this method is called when a tile is moused over and also raises the hex to indicate which hex is moused over to the player
    void OnMouseOver()
    {
        SetCurrentHexes(gameObject);
        gameObject.transform.position = new Vector3(startPos.x, startPos.y + mouseOverRaiseAmount, startPos.z);
    }
    // C.Love - this method calls all relevant scripts to set the correct reference to the currently moused over hex
    void SetCurrentHexes(GameObject hex)
    {
        uIControllerObject = GameObject.Find("UI Manager");
        UI_Controller uIController = uIControllerObject.GetComponent<UI_Controller>();
        uIController.SetCurrentHex(hex);
        officeButton.GetComponent<PurchaseController>().SetCurrentHex(hex);
        onlineAd.GetComponent<PurchaseController>().SetCurrentHex(hex);
        paperAd.GetComponent<PurchaseController>().SetCurrentHex(hex);
        canvasing.GetComponent<PurchaseController>().SetCurrentHex(hex);
    }
    // C.Love - this method resets the SetCurrentHexes() method to null for the nex hex to apply and resets the position of the hexagon.
    void OnMouseExit()
    {
        transform.position = startPos;
        SetCurrentHexes(null);
    }
}
