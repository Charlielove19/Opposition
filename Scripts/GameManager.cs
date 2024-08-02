using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    /// <summary>
    /// -- C.Love -- this script is the main game controller and controls turn number, win conditions and managing methods that relate to more than one tile
    /// </summary>
    public bool hasWon = false;
    public int turnNumberGM;
    public bool isElection;
    [SerializeField] private GameObject uIManager;
    [SerializeField] private GameObject hex;
    [SerializeField] private GameObject policy;
    private InvestmentManager investmentManager;
    private PolicyProperties policyProperties;
    private HexProperties hexProperties;
    private UI_Controller uIController;
    public List<GameObject> hexList;
    private PlayerProperties playerProps => GameObject.Find("Player").GetComponent<PlayerProperties>();
    public string state;
    [SerializeField] private GameObject nextTurnButton;
    PolicyStacker policyStacker => GameObject.Find("PolicyStack").GetComponent<PolicyStacker>();
    public int width;
    public int height;
    public int hexCount;
    public int govCount;
    public int testX;
    public int testY;
    [SerializeField] private GameObject winScreen;
    // C.Love - this method counts the number of government seats ad player seats
    private void CountSeats()
    {
        hexCount = 0;
        govCount = 0;
        for (int i=0; i<hexList.Count; i++)
        {
            bool currentHexUnderControl = hexList[i].GetComponent<HexProperties>().CheckTileUnderControl();
            
            if (currentHexUnderControl)
            {
                hexCount += 1;
            }
            else
            {
                govCount += 1;
            }
        }
    }
    // C.Love - this method checks if player seats are higher than government seats and declares a public bool for winning
    private void CheckWin()
    {
        CountSeats();
        
        Debug.Log(hexCount + " " + govCount);
        if (hexCount>govCount)
        {
            hasWon = true;
            winScreen.SetActive(true);
        }
        else
        {
            hasWon = false;
        }
    }
    // C.Love - this method returns a hex GameObject from co-ordinate values
    public GameObject ReturnHexFromCoOrds(int x, int y)
    {
        int index = (x*height) + y;
        if (index < hexList.Count)
        {
            return hexList[index];
        }
        else
        {
            return null;
        }
    }
    // C.Love - this method is called once when the GameManager object is initialised and declares all GameObject references alongside starting the first turn
    void Start()
    { 
        state = "menu";
        turnNumberGM = 0;
        height = GameObject.Find("GridManager").GetComponent<GridSpawner>().height;
        width = GameObject.Find("GridManager").GetComponent<GridSpawner>().width;
        uIController = uIManager.GetComponent<UI_Controller>();
        hexProperties = hex.GetComponent<HexProperties>();
        policyProperties = policy.GetComponent<PolicyProperties>();
        investmentManager = GameObject.Find("Investment").GetComponent<InvestmentManager>();
        StartTurn();  
    }
    // C.Love - this method is called at the start of every turn and wipes all necessary values, checks turn conditions and initialises policies.
    void StartTurn()
    {
        turnNumberGM += 1;
        IsElection();
        GeneratePolicies();
        uIController.SetTurnNumber();
        policyStacker.ResetPolicyStacker();
        playerProps.StartTurn();
    }
    // C.Love - this method generates a random number (between 1 and 3) of policies
    void GeneratePolicies()
    {
        int randomtimes = UnityEngine.Random.Range(1,3);
        for (int i=0; i<randomtimes;i++)
        {
            if (!hasWon)
            {
                uIController.NewPolicy();
            }
        }
    }
    // C.Love - this checks if the turn number is a multiple of 5 and thus if it is an election turn
    void IsElection()
    {
        if (turnNumberGM%5 == 0)
        {
            isElection = true;
        }
        else
        {
            isElection = false;
        }
    }
    // C.Love - this resets and updates all relevant values and the end of each turn
    void EndTurn()
    {
        UpdateHexes();
        UpdateInvestment();
        uIController.SetinvestmentFavourUI();
        StartCoroutine(EndTurnChecks());
        nextTurnButton.GetComponent<NextTurnButton>().isPressed = false;
        if (isElection)
        {
            CheckWin();
        }
    }
    // C.Love - this simply delays the gap between the end of a turn and a start by 1 second to feel less confusing
    private IEnumerator EndTurnChecks()
    {
        yield return new WaitForSeconds(1);
        StartTurn();
    }
    // C.Love - this cycles through all the hexes and calls their individal EndTurnProcedures
    void UpdateHexes()
    {
        for(int i= 0; i<hexList.Count; i++)
        {
            hex = hexList[i];
            hexProperties = hex.GetComponent<HexProperties>();
            hexProperties.EndTurnProcedure();
        }
    }
    // C.Love - this updates the investment impact after votes are made
    void UpdateInvestment()
    {
        investmentManager.ImplementInvestmentPolicyImpact();   
    }
    // C.Love - these methods convert XY co-ordinates for hexes to cubic co-ordinates in order to declare a list of hex object within a certain radial range of a centre hex
    // C.Love - the co-ordinate conversion methods are taken from red blob games (link below) and adjusted for my co-ordinate system.
    // https://www.redblobgames.com/grids/hexagons/
    private (int, int) ConvertQRStoXYEven(int q, int r, int s)
    {
        int x = q+(r-(r&1))/2;
        int y = r;
        return (x, y);
    }
    private (int, int) ConvertQRStoXYOdd(int q, int r, int s)
    {
        int x = q+(r+(r&1))/2;
        int y = r;
        return (x, y);
    }
    private (int, int, int) ConvertXYtoQRS(int x, int y)
    {
        int q = x-(y+(y&1))/2;
        int r = y;
        int s = -q-r;

        return (q,r,s);
    }
    private List<(int,int,int)> ListRangeAroundInQRS(int n)
    {
        List<(int,int,int)> list = new List<(int,int,int)>();
        for (int q=-n;q<=n;q++)
        {
            for (int r=-n;r<=n;r++)
            {
                for (int s=-n;s<=n;s++)
                {
                    if ((q+s+r)==0)
                    {
                        list.Add((q,r,s));
                    }
                }
            }
        }
        return list;
    }
    private List<(int,int)> ListRangeAroundInXYGivenCentre(int x, int y, int n)
    {
        List<(int,int,int)> qRSList = ListRangeAroundInQRS(n);
        List<(int, int)> xyList = new List<(int, int)>();
        foreach (var (q,r,s)in qRSList)
        {
            int newX, newY;
            if (y%2 ==0)
            {
                (newX, newY) = ConvertQRStoXYEven(q, r, s);
            }
            else 
            {
                (newX, newY) = ConvertQRStoXYOdd(q, r, s);
            }

            xyList.Add((newX+x,newY+y));
        }
        return xyList;
    }
    public List<GameObject> ListHexesWithinRange(GameObject centreHex, int n)
    {
        List<GameObject> hexesWithinRange = new List<GameObject>();
        int centreX = centreHex.GetComponent<HexProperties>().xCoord;
        int centreY = centreHex.GetComponent<HexProperties>().yCoord;
        List<(int, int)> xyList = ListRangeAroundInXYGivenCentre(centreX,centreY,n);
        foreach (var (xC,yC)in xyList)
        {
            if (xC < width && yC < height && xC>=0 && yC >=0)
            {
                hexesWithinRange.Add(ReturnHexFromCoOrds(xC,yC));
                
            }
        }
        return hexesWithinRange;
    }
    // C.Love - this is called once per frame and counts seats and also checks for the next turn button being pressed
    void Update()
    {
        CountSeats();
        if (nextTurnButton.GetComponent<NextTurnButton>().isPressed)
        {
            EndTurn();
        }
    }
}
