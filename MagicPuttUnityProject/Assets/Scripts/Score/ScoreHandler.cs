using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreHandler : MonoBehaviour
{
    //Current hole the user is playing on
    public int currentHole = 1;

    //Pars for each hole
    [SerializeField]
    public int[] pars = new int[9];

    //Scores for Each hole
    public int[] scores = new int[9];

    public void IncrementStroke()
    {
        scores[currentHole-1]++;
    }
}
