using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolicyStacker : MonoBehaviour
{
    /// <summary>
    /// -- C.Love -- this script stacks all policy impacts and choices to allow for multiple policies in a turn
    /// </summary>
    public float[] impacts = new float[6];
    public float stackedInvestmentImpact;
    private float chanceToVoteOptimally => GameObject.Find("Government").GetComponent<GovernmentController>().chanceToVoteOptimally;
    // C.Love - this method resets the policy stacker and is called each turn
    public void ResetPolicyStacker()
    {
        for (int i = 0; i<6;i++)
        {
            impacts[i] = 0;
        }
        stackedInvestmentImpact = 0;
    }
    // C.Love - this adds all policy impacts after they have been voted on
    public void AddValues(float[] polimpacts, float forAgainstMultiplier, float investmentImpact)
    {
        for (int i = 0;i< impacts.Length; i++)
        {
            impacts[i] += polimpacts[i] * forAgainstMultiplier;
            
        }
        stackedInvestmentImpact += investmentImpact * forAgainstMultiplier;
    }

    // C.Love - this adds a random chance that the government will vote optimally
    public int RandomiseOptimalVote()
    {
        if (Random.value<chanceToVoteOptimally)
        {
            int optimalVote = FindOptimalVote();
            Debug.Log("optimal vote:" + optimalVote);
            return optimalVote;
        }
        else
        {
            return FindOptimalVote()*-1;
        }
    }
    // C.Love - this method finds the optimal vote for each turn
    public int FindOptimalVote()
    {
        float total = new float();
        for (int i =0; i<impacts.Length; i++)
        {
            total += impacts[i];
        }
        total += stackedInvestmentImpact*6;
        if (total> 0)
        {
            return 1;
        }
        else
        {
            return -1;
        }
    }
}
