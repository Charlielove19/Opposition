using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GovernmentController : MonoBehaviour
{
    /// <summary>
    /// -- C.Love -- this script declares variables relevant to the government and gives them a swing opposite to that of the players
    /// </summary>
    public float govStartSwing;
    public float govFavourAdvantage;
    public float chanceToVoteOptimally;
    [SerializeField] GameObject player;
    void Start()
    {
        govStartSwing = player.GetComponent<PlayerProperties>().chosenStartSwing * -1;
    }
}
