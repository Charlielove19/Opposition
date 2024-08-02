using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoteCalc : MonoBehaviour
{
    /// <summary>
    /// --C.Love-- this script is the vote calculator script that runs through turnout then favour to generate number of votes for each demographic
    /// </summary>
    
    // C.Love - this method generates voter turnout
    private int TurnoutGen(float turnoutProbability, float demPerc)
    {
        int demCount = Mathf.RoundToInt(demPerc / 2f);
        int turnoutNumber = 0;

        for (int i = 0; i < demCount; i++)
        {
            if (Random.value < turnoutProbability)
            {
                turnoutNumber++;
            }
        }

        return turnoutNumber;
    }
    // C.Love - this method generates votes within a turnout
    private int Votes(float turnoutFinal, float playerVoteProbability)
    {
        int votedPlyrNumber = 0;

        for (int i = 0; i < turnoutFinal; i++)
        {
            if (Random.value < playerVoteProbability)
            {
                votedPlyrNumber++;
            }
        }

        return votedPlyrNumber;
    }
    // C.Love - this method then compounds the other 2 to generate a number of votes for one demographic
    public int CalculatePlayerVotesForOneDemographic(float plyrFavour, float govFavour, float turnoutProbability, float demPerc)
    {
        float favourDiffNormalised = (float)(plyrFavour - govFavour) / 200f;
        float playerVoteProbability = 0.5f + favourDiffNormalised / 2f;
        float turnoutFinal = TurnoutGen(turnoutProbability, demPerc);
        int votedPlyrNumber = Votes(turnoutFinal, playerVoteProbability);
        return votedPlyrNumber;
    }
}
