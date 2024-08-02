using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvestmentManager : MonoBehaviour
{
    /// <summary>
    /// -- C.Love -- this script is where investment favour is stored and updated
    /// </summary>
    public float investmentFavour = 0f;
    public float lobbyThreshold;
    private PolicyStacker policyStacker => GameObject.Find("PolicyStack").GetComponent<PolicyStacker>();
    private GameObject uI;
    private UI_Controller uI_Controller;
    public void ImplementInvestmentPolicyImpact()
    {
        investmentFavour += policyStacker.stackedInvestmentImpact;
        investmentFavour = Mathf.Clamp(investmentFavour, -300f, 300f);
    }
}
