using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreHandler : MonoBehaviour
{
    const int numHoles = 9;

    //Current hole the user is playing on
    public int currentHole = 1;

    //Pars for each hole
    [SerializeField]
    public int[] pars = new int[numHoles];
    private int[] lastPars = new int[numHoles];

    [SerializeField]
    TextMesh parText = default;

    [SerializeField]
    TextMesh scoreText = default;

    //Scores for Each hole
    public int[] scores = new int[numHoles];
    private int[] lastScores = new int[numHoles];

    float currTime = 0;


    private void Update()
    {
        //Update scorecard every 1/2 second
        currTime += Time.deltaTime;

        if (currTime > 0.5)
        {
            UpdateParText();
            UpdateScoreText();
            currTime = 0;
        }
    }

    public void IncrementStroke()
    {
        scores[currentHole-1]++;
    }

    void UpdateParText()
    {
        string _text = "";

        for(int i =0; i < pars.Length; i++)
        {
            _text += pars[i].ToString() + "\n\n";
        }
        parText.text = _text;
    }

    void UpdateScoreText()
    {
        string _text = "";

        for (int i = 0; i < scores.Length; i++)
        {
            if (scores[i] == 0)
            {
                _text += "\n\n";
            }
            else
            {
                _text += scores[i].ToString() + "\n\n";
            }
        }
        scoreText.text = _text;
    }
}
