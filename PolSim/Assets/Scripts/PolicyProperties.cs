
using System.Linq;
using TMPro;
using UnityEngine;

public class PolicyProperties : MonoBehaviour
{
    /// <summary>
    /// -- C.Love -- this script stores all policy values and draws relevant info from PolicyList.cs
    /// </summary>
    [SerializeField] GameObject[] impactBars;
    [SerializeField] private GameObject investmentImpactBar;
    [Header("Age Impacts")] 
    public float[] impacts;
    private PolicyStacker policyStacker => GameObject.Find("PolicyStack").GetComponent<PolicyStacker>();
    private float investmentImpact;
    [Header("Other")]
    public string voteChoice;
    public float forAgainstMultiplier;
     [Header("UI objects")]
    [SerializeField] private TextMeshProUGUI currentPolicyTitle;
    [SerializeField] private TextMeshProUGUI currentPolicyDescription;
    [SerializeField] private TextMeshProUGUI currentPolicyMinistry;
    public string randPolTitle;
    // C.Love - this is called upon initialisation of each policy and applies all relevant methods
    void Start()
    {
        SortPolicyInfo();
        ListImpacts();
        AdjustImpactBar();
        AdjustInvestmentImpactBar();
        investmentImpact = GetValueFromPolDict("investmentImpact");
    }
    // C.Love - this method applies all the relevant title info from PolicyList.cs
    void SortPolicyInfo()
    {
        randPolTitle = gameObject.GetComponent<PolicyList>().GetRandomPolicy();
        currentPolicyTitle.text = SplitPolTitle(randPolTitle)[0];
        currentPolicyDescription.text = SplitPolTitle(randPolTitle)[1];
        currentPolicyMinistry.text = SplitPolTitle(randPolTitle)[2];
    }
    // C.Love - this method splits the title string from PolicyList.cs into policy title, info and ministry
    string[] SplitPolTitle(string x)
    {
        string[] parts = x.Split( " _ " );
        return parts;
    }
    // C.Love - this method sets the UI of all policy impact bars to the relevant values
    void AdjustImpactBar()
    {
        for(int i = 0; i<impactBars.Length; i++)
            {
                GameObject currentBar = impactBars[i];
                ImpactBarController accessScript = currentBar.GetComponent<ImpactBarController>();
                accessScript.SetWidth(impacts[i]); // WW - Changed to setWidth, due to new policy design
            }
    }
    // C.Love - this does the same but  for the investment impact bar
    void AdjustInvestmentImpactBar()
    {
        ImpactBarController accessScript = investmentImpactBar.GetComponent<ImpactBarController>(); //WW
        accessScript.SetWidth(GetValueFromPolDict("investmentImpact")); // WW
    }
    // C.Love - this method retrieves the impacts from PolicyList.cs
    public float GetValueFromPolDict(string x)
    {
        return gameObject.GetComponent<PolicyList>().GetPolicyValues(randPolTitle)[x];
    }
    // C.Love - this makes a list of current policy impacts
    void ListImpacts()
    {
        impacts = new float[6];
        impacts[0] = GetValueFromPolDict("youngImpact");
        impacts[1] = GetValueFromPolDict("profImpact");
        impacts[2] = GetValueFromPolDict("oldImpact");
        impacts[3] = GetValueFromPolDict("lowImpact");
        impacts[4] = GetValueFromPolDict("intermediateImpact");
        impacts[5] = GetValueFromPolDict("highImpact");
    }
    // C.Love - these methods apply the relevant vote choices
    public void For()
    {
        voteChoice = "for";
        forAgainstMultiplier = 1;
        policyStacker.AddValues(impacts, forAgainstMultiplier,investmentImpact);
        Destroy(gameObject);
    }
    public void Against()
    {
        voteChoice = "against";
        forAgainstMultiplier = -1;
        policyStacker.AddValues(impacts, forAgainstMultiplier,investmentImpact);
        Destroy(gameObject);
    }
    public void Abstain()
    { 
        voteChoice = "abstain";       
        forAgainstMultiplier = 0;
        policyStacker.AddValues(impacts, forAgainstMultiplier,investmentImpact);
        Destroy(gameObject);
    }
        
        
}
