

using System.Collections.Generic;

using UnityEngine.SceneManagement;
using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class UI_Controller : MonoBehaviour

{
    /// <summary>
    /// -- C.Love -- this is the script that controls all the UI features within the game from policies to purchasables
    /// </summary>
    private PlayerProperties playerProps => GameObject.Find("Player").GetComponent<PlayerProperties>();
    [Header("Stats UI")]
    [SerializeField] private TextMeshProUGUI tileSwingText;
    [SerializeField] private TextMeshProUGUI turnNumber;
    [SerializeField] private TextMeshProUGUI investmentFavourUI;
    [SerializeField] private TextMeshProUGUI money;
    [SerializeField] private TextMeshProUGUI seatNumberText;
    [SerializeField] private TextMeshProUGUI govSeatsText;
    [Header("Other")]
    [SerializeField] private GameObject hex;
    [SerializeField] private GameObject policy;
    [SerializeField] private GameObject uICanvas;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private InvestmentManager investmentManager;
    private PolicyProperties policyProperties;
    private HexBehaviour hexBehaviour;
    private HexProperties hexProperties;
    private float currentTileSwingOutput;
    public GameObject currentPolicy;
    private List<string> demographics;
    [SerializeField] private GameObject pauseMenuCanvas;
    [SerializeField] private GameObject winScreen;
    public Image[] ageBarSections;
    public Image[] wealthBarSections;
    [SerializeField] private GameObject investmentFavourBar;
    [SerializeField] private GameObject[] favourBars;
    
    // WW - sets age bar UI
    private void AgePieChart()
    {
        ageBarSections[0].fillAmount = hex.GetComponent<HexProperties>().percYoung/100;
        ageBarSections[2].fillAmount = hex.GetComponent<HexProperties>().percOld/100; 
    }
    // WW - sets wealth bar UI
    private void WealthPieChart()
    {
        wealthBarSections[0].fillAmount = hex.GetComponent<HexProperties>().percLow/100;
        wealthBarSections[2].fillAmount = hex.GetComponent<HexProperties>().percHigh/100;
        Debug.Log(hex.GetComponent<HexProperties>().percLow/100 + " " + hex.GetComponent<HexProperties>().percIntermediate/100 + " " + hex.GetComponent<HexProperties>().percHigh/100);
    }
    // C.Love - this sets the money UI
    private void Money()
    {
        money.text = "Money: " + playerProps.money;
    }
    // C.Love - this is called at the start of the game and creates references to all relevant objects
    void Start()
    {
        demographics = new List<string>()
        {
            "young",
            "prof",
            "old",
            "low",
            "intermediate",
            "high"
        };
        hexBehaviour = hex.GetComponent<HexBehaviour>();
        hexProperties = hex.GetComponent<HexProperties>();
        policyProperties = policy.GetComponent<PolicyProperties>();
        investmentManager = GameObject.Find("Investment").GetComponent<InvestmentManager>();
        SetinvestmentFavourUI();
    }
    // C.Love this is called once per frame and sets the current tile UI and money
    void Update()
    {
        MouseOverTileSetUI();
        Money();
        
        // WW
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (!winScreen.activeSelf) {
                TogglePauseMenu();
            }
        }
        

    }
    // WW
    public void TogglePauseMenu()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            pauseMenuCanvas.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            pauseMenuCanvas.SetActive(false);
        }
    }
    // WW
    public void ExitToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
    // C.Love - this instantiates a new policy in a slightly random position
    public void NewPolicy()
    {
        float offsetRange = 200f;
        Vector3 randomPos = new Vector3(Random.Range(-offsetRange,offsetRange),Random.Range(-offsetRange,offsetRange),Random.Range(-offsetRange,offsetRange));
        currentPolicy = Instantiate(policy, uICanvas.transform.position+randomPos, Quaternion.identity, uICanvas.transform);
    }
    // C.Love - this updates the seat numbers in the top left
     private void UpdateSeatUI()
    {
        seatNumberText.text = "NUMBER OF SEATS: " + gameManager.hexCount;
        govSeatsText.text = "GOVERNMENT SEATS " + gameManager.govCount; 
    }
    // C.Love - this updates the relevant UI if a hex is moused over
    private void MouseOverTileSetUI()
    {
        if (hex != null)
        {
            UpdateSeatUI();
            AgePieChart();
            WealthPieChart();
            SetFavourBars();
        }
        
    }
    // C.Love - this sets the turn number UI
    public void SetTurnNumber()
    {
        if (gameManager.isElection)
        {
            turnNumber.text = "Turn: " + gameManager.turnNumberGM + "~ELECTION~";
        }
        else
        {
        turnNumber.text = "Turn: " + gameManager.turnNumberGM;
        }
    }
    // C.Love - this adjusts the investment favour bar
    public void SetinvestmentFavourUI()
    {
        investmentFavourBar.GetComponent<ImpactBarController>().SetWidth(investmentManager.investmentFavour);
    }
    // C.Love - this adjusts the favour bars
    private void SetFavourBars()
    {
        if(hex!=null)
        {
            for (int i = 0; i<demographics.Count; i++)
            {
                favourBars[i].GetComponent<ImpactBarController>().SetWidth(hex.GetComponent<HexProperties>().favours[demographics[i]]);
            }
            
        }
        
    }
    // C.Love - this allows the UI script to access the current hex that has been moused over
    public void SetCurrentHex(GameObject newHex)
    {
        hex = newHex;
        if (hex != null)
        {
            hexBehaviour = hex.GetComponent<HexBehaviour>();
            hexProperties = hex.GetComponent<HexProperties>();
        }   
    }

    
}
