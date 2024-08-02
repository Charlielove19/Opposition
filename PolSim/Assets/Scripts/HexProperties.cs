using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

public class HexProperties : MonoBehaviour
{
    /// <summary>
    /// -- C.Love -- this script is where all hex values are initialised, stored and updated and is applied to each hex
    /// </summary>
    [SerializeField] private GameObject uI;
    private UI_Controller uI_Controller;
    private GameManager gameManager => GameObject.Find("GameManager").GetComponent<GameManager>();
    private PolicyProperties polProps;
    private PolicyStacker policyStacker => GameObject.Find("PolicyStack").GetComponent<PolicyStacker>();
    public int xCoord;
    public int yCoord;
    public float population;
    [Header("Age Demographics")]
    public float percYoung;
    public float percProf;
    public float percOld;
    public float totalAgePop;
    private float youngPercMean = 19.93f;
    private float youngStdDev = 6.30f;
    private float profPercMean = 57.27f;
    private float profStdDev = 3.75f;
    private float oldPercMean = 22.8f;
    private float oldStdDev = 5.97f;
    [Header("Wealth Demographics")]
    public float percHigh;
    public float percIntermediate;
    public float percLow;
    public float totalEarnerPop;
    private float highEarnerMean = 38.91f;
    private float highEarnerStdDev = 8.34f;
    private float intermediateEarnerMean = 26.20f;
    private float intermediateEarnerStdDev = 2.68f;
    private float lowEarnerMean = 34.89f;
    private float lowEarnerStdDev = 8.54f;
    private Dictionary<string, float> demographicPercentages;
    [Header("Voting Swing")]
    public Dictionary<string, float> swings;
    private float youngSwingMean = 0.5f; 
    private float profSwingMean = -0.23f;
    private float oldSwingMean = -0.5f;
    private float lowSwingMean = 0.1f;
    private float intermediateSwingMean = -0.5f;
    private float highSwingMean = -0.3f;
    private float swingStdDev = 0.1f;
    private float playerSwing => GameObject.Find("Player").GetComponent<PlayerProperties>().chosenStartSwing; 
    [Header("Turnout")]
    public Dictionary<string,float> turnouts;
    private float youngTurnoutMean = 0.39f;
    private float profTurnoutMean = 0.56f;
    private float oldTurnoutMean = 0.73f;
    private float lowTurnoutMean = 0.42f;
    private float intermediateTurnoutMean = 0.58f;
    private float highTurnoutMean = 0.69f;
    private float turnoutStdDev = 0.15f;
    [Header("Average Favours")]
    [SerializeField] private float govAverageFavour;
    [SerializeField] private float plyrAverageFavour;
    [Header("Initialise Favours")]
    public Dictionary<string,float> favours;
    public Dictionary<string,float> govFavours;
    private GameObject playerStats;
    private GameObject govStats;
    private float govSwing;
    private float govInitialAdvantage;
    [SerializeField] private float initialFavourDampener = 0.5f; 
    private float impactMultiplier = 0.05f;
    private Renderer hexRenderer;
    private List<string> demographics;
    [Header("Votes")]
    public float playerVotes;
    public float govVotes;
    public float plyrLoyalty;
    public float govLoyalty;
    private float loyaltyChangeProbability = 0.3f;
    public float plyrLoyaltyBottomCap = 0;
    public float plyrLoyaltyTopCap = 5;
    public float plyrLoyaltyMultiplier = 3;
    public float govLoyaltyMultiplier = 3;
    // C.Love - this method is called at the start of the game and lays out the list of demographics which allow later dictionaries to be called from
    // it also initialises hex values and creates references for relevant Game Objects
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
        StartValues(); 
        playerStats = GameObject.Find("Player");
    }
    // C.Love - this method calls all value initialising methods
    void StartValues()
    {
        RandomPop();
        ElectionCalc();
        ColorTiles();
    }
    // C.Love - this method was used to test my range functions in GameManager.cs
    public void SetTilesGreen()
    {
        hexRenderer = gameObject.GetComponent<Renderer>();
        hexRenderer.material.color = Color.green;
    }
    // C.Love - this method checks who controls a tile and colours it appropriately
    private void ColorTiles()
    {
        hexRenderer = gameObject.GetComponent<Renderer>();
       if (CheckTileUnderControl())
       {
            hexRenderer.material.color = Color.red;
       }
       else
       {
            hexRenderer.material.color = Color.blue;
       }
    }   
    // C.Love - this method initialises favour values for a hex, adding the gov favour advantage to govFavours
    private void InitialiseFavours()
    {
        playerStats = GameObject.Find("Player");
        govStats = GameObject.Find("Government");
        govSwing = govStats.GetComponent<GovernmentController>().govStartSwing;
        govInitialAdvantage = govStats.GetComponent<GovernmentController>().govFavourAdvantage;
        
        favours = new Dictionary<string, float>
        {
            { "young", GenerateInitialFavourFromSwings(swings["young"],playerSwing, initialFavourDampener)},
            { "prof", GenerateInitialFavourFromSwings(swings["prof"],playerSwing, initialFavourDampener)},
            { "old", GenerateInitialFavourFromSwings(swings["old"],playerSwing, initialFavourDampener)},
            { "low", GenerateInitialFavourFromSwings(swings["low"],playerSwing,initialFavourDampener)},
            { "intermediate", GenerateInitialFavourFromSwings(swings["intermediate"],playerSwing,initialFavourDampener)},
            { "high",  GenerateInitialFavourFromSwings(swings["high"],playerSwing,initialFavourDampener)}
        };
        govFavours = new Dictionary<string, float>
        {
            { "young", GenerateInitialFavourFromSwings(swings["young"],govSwing, initialFavourDampener)+ govInitialAdvantage},
            { "prof", GenerateInitialFavourFromSwings(swings["prof"],govSwing, initialFavourDampener)+ govInitialAdvantage},
            { "old", GenerateInitialFavourFromSwings(swings["old"],govSwing, initialFavourDampener)+ govInitialAdvantage},
            { "low", GenerateInitialFavourFromSwings(swings["low"],govSwing,initialFavourDampener)+ govInitialAdvantage},
            { "intermediate", GenerateInitialFavourFromSwings(swings["intermediate"],govSwing,initialFavourDampener)+ govInitialAdvantage},
            { "high",  GenerateInitialFavourFromSwings(swings["high"],govSwing,initialFavourDampener)+ govInitialAdvantage}
        };
    }      
    // C.Love - this method compares the swing of the player/government to the swing of the demographic and generates a favour value from that
    private float GenerateInitialFavourFromSwings(float demSwing, float plyrSwing, float dampingValue = 10)
    {
        return 100*(1-Math.Abs(demSwing-plyrSwing))*dampingValue;
    } 
    // C.Love - this method generates the swing value dictionary from means and stdDevs
    void DictSwing()
    {
        swings = new Dictionary<string, float>
        {
            { "young", GenerateRandomWithMeanAndStdDev(youngSwingMean, swingStdDev) },
            { "prof", GenerateRandomWithMeanAndStdDev(profSwingMean, swingStdDev) },
            { "old", GenerateRandomWithMeanAndStdDev(oldSwingMean, swingStdDev) },
            { "low", GenerateRandomWithMeanAndStdDev(lowSwingMean, swingStdDev) },
            { "intermediate", GenerateRandomWithMeanAndStdDev(intermediateSwingMean, swingStdDev) },
            { "high", GenerateRandomWithMeanAndStdDev(highSwingMean, swingStdDev) }
        };
    }  
    // C.Love - this method uses a Box-muller transform to generate a random value according to a given mean and standard deviation
    private float GenerateRandomWithMeanAndStdDev(float mean, float stdDev)
    {
        float u1 = 1.0f - UnityEngine.Random.value; 
        float u2 = 1.0f - UnityEngine.Random.value;
        float randStdNormal = Mathf.Sqrt(-2.0f * Mathf.Log(u1)) * Mathf.Sin(2.0f * Mathf.PI * u2); 
        float randNormal = mean + stdDev * randStdNormal; 
        return randNormal;
    }  
    // C.Love - these method generates all the population percentages from their means and stdDevs followed by normalising them to a total of 100
    void DistributeAges()
    {
        percYoung = GenerateRandomWithMeanAndStdDev(youngPercMean, youngStdDev);
        percProf = GenerateRandomWithMeanAndStdDev(profPercMean, profStdDev);
        percOld = GenerateRandomWithMeanAndStdDev(oldPercMean, oldStdDev);
        
        totalAgePop = percYoung + percProf + percOld;
        
        percYoung = percYoung / totalAgePop * 100f;
        percProf = percProf / totalAgePop * 100f;
        percOld = percOld / totalAgePop * 100f;
        totalAgePop = percYoung + percProf + percOld;
    }
    void DistributeEarnings()
    {
        percHigh = GenerateRandomWithMeanAndStdDev(highEarnerMean, highEarnerStdDev);
        percIntermediate = GenerateRandomWithMeanAndStdDev(intermediateEarnerMean, intermediateEarnerStdDev);
        percLow = GenerateRandomWithMeanAndStdDev(lowEarnerMean, lowEarnerStdDev);
        
        totalEarnerPop = percHigh + percIntermediate + percLow;

        percHigh = percHigh / totalEarnerPop * 100f;
        percIntermediate = percIntermediate / totalEarnerPop * 100f;
        percLow = percLow / totalEarnerPop * 100f;
        totalEarnerPop = percHigh + percIntermediate +percLow;
    }
    // C.Love - this method then generates a dictionary containing all the demographic population percentages
    void CreateDemoDict()
    {
        demographicPercentages =  new Dictionary<string,float>
        {
            { "young", percYoung},
            { "prof", percProf},
            { "old", percOld},
            { "low", percLow},
            { "intermediate", percIntermediate},
            { "high", percHigh} 
        };
    }   
    // C.Love - these methods were used to generate preliminary values of overall favour within a tile to test election stats
    float CalculateOverallFavour(Dictionary<string,float> favours, Dictionary<string,float> percentages)
    {
        List<float> values = new List<float>();
        for (int i=0; i<demographics.Count;i++)
        {
            values.Add(favours[demographics[i]] * (percentages[demographics[i]]/100));
        }
        return values.Sum()/values.Count; 
    }
    private void UpdateOverallFavours()
    {
        govAverageFavour = CalculateOverallFavour(govFavours, demographicPercentages);
        plyrAverageFavour = CalculateOverallFavour(favours, demographicPercentages);
    }    
    // C.Love - this method generates semi random turnout values within a dictionary from a given mean and stdDev on each tile
    private void CreateTurnoutDict()
    {
        turnouts =  new Dictionary<string,float>
        { 
            { "young", GenerateRandomWithMeanAndStdDev(youngTurnoutMean,turnoutStdDev)},
            { "prof", GenerateRandomWithMeanAndStdDev(profTurnoutMean,turnoutStdDev)},
            { "old", GenerateRandomWithMeanAndStdDev(oldTurnoutMean,turnoutStdDev)},
            { "low", GenerateRandomWithMeanAndStdDev(lowTurnoutMean,turnoutStdDev)},
            { "intermediate", GenerateRandomWithMeanAndStdDev(intermediateTurnoutMean,turnoutStdDev)},
            { "high", GenerateRandomWithMeanAndStdDev(highTurnoutMean,turnoutStdDev)} 
        };
    }   
    // C.Love - this method then calls all the above methods for each demographic value
    void RandomPop()
    {
        population = 100f;
        DistributeAges();
        DistributeEarnings();
        DictSwing();
        InitialiseFavours();
        CreateDemoDict();
        UpdateOverallFavours();
        CreateTurnoutDict();
    }
    // C.Love - this method can be called by the policy stacker to implement all the impacts on the favour values
    private void ImplementTilePolicyImpact()
    {
        for (int i=0; i<demographics.Count;i++)
        {
            favours[demographics[i]] += policyStacker.impacts[i] * impactMultiplier;
        }
        
        for (int i=0; i<demographics.Count;i++)
        {
            govFavours[demographics[i]] += policyStacker.impacts[i] * impactMultiplier * policyStacker.RandomiseOptimalVote();
        }
        if (gameManager.isElection)
        {
            ElectionCalc();
        }
    }   
    // C.Love - this method clamps the favour values for tile to be between +- 100
    private void ClampFavours()
    {
        for (int i=0; i<demographics.Count; i++)
        {
            favours[demographics[i]] = Mathf.Clamp(favours[demographics[i]], -100, 100);
        }
        for (int i=0; i<demographics.Count; i++)
        {
            govFavours[demographics[i]] = Mathf.Clamp(govFavours[demographics[i]], -100, 100);
        }
    }
    // C.Love - this method calculates the votes for each tile given the relevant favours and govFavours for each demographic then totals votes for the tile
    private void ElectionCalc()
        {
            playerVotes = 0;
            govVotes = 0;
            VoteCalc voteCalc = gameObject.GetComponent<VoteCalc>();
            for (int i=0; i<demographics.Count; i++)
            {
                playerVotes += voteCalc.CalculatePlayerVotesForOneDemographic(favours[demographics[i]],govFavours[demographics[i]],turnouts[demographics[i]],demographicPercentages[demographics[i]]);
            }
            for (int i=0; i<demographics.Count; i++)
            {
                govVotes += voteCalc.CalculatePlayerVotesForOneDemographic(govFavours[demographics[i]],favours[demographics[i]],turnouts[demographics[i]],demographicPercentages[demographics[i]]);
            }
            playerVotes += plyrLoyalty*plyrLoyaltyMultiplier;
            govVotes += govLoyalty*govLoyaltyMultiplier;

        }
    // C.Love - this checks which entity has more votes and returns a bool for if the player controls said tile
    public bool CheckTileUnderControl()
    {
        if (playerVotes<govVotes)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    // C.Love - this method controls loyalty values and will run once per turn and update the loyalty accordingly
    public void UpdateLoyalty()
    {
        if (CheckTileUnderControl())
        {
            if (UnityEngine.Random.value<loyaltyChangeProbability && plyrLoyalty<plyrLoyaltyTopCap)
            {
                plyrLoyalty += 1;
            }
            if (UnityEngine.Random.value<loyaltyChangeProbability && govLoyalty > 0)
            {
                govLoyalty -= 1;
            }
        }
        else
        {
            if (UnityEngine.Random.value<loyaltyChangeProbability && plyrLoyalty>plyrLoyaltyBottomCap)
            {
                plyrLoyalty -= 1;
            }
            if (UnityEngine.Random.value<loyaltyChangeProbability && govLoyalty < 5)
            {
                govLoyalty += 1;
            }
        }
    }
    // C.Love - this method calls all the relevant methods at the end of a turn for each hex
    public void EndTurnProcedure()
    {
        UpdateLoyalty();
        ImplementTilePolicyImpact();
        ClampFavours();
        UpdateOverallFavours();
        ColorTiles();
    }
}
