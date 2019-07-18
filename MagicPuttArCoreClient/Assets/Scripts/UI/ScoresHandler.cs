using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoresHandler : MonoBehaviour
{
    [SerializeField]
    Text hole1Par = default;
    [SerializeField]
    Text hole2Par = default;
    [SerializeField]
    Text hole3Par = default;
    [SerializeField]
    Text hole4Par = default;
    [SerializeField]
    Text hole5Par = default;
    [SerializeField]
    Text hole6Par = default;
    [SerializeField]
    Text hole7Par = default;
    [SerializeField]
    Text hole8Par = default;
    [SerializeField]
    Text hole9Par = default;

    [SerializeField]
    Text hole1Score = default;
    [SerializeField]
    Text hole2Score = default;
    [SerializeField]
    Text hole3Score = default;
    [SerializeField]
    Text hole4Score = default;
    [SerializeField]
    Text hole5Score = default;
    [SerializeField]
    Text hole6Score = default;
    [SerializeField]
    Text hole7Score = default;
    [SerializeField]
    Text hole8Score = default;
    [SerializeField]
    Text hole9Score = default;

    [SerializeField]
    Text totalScore = default;

    ScoresMessage scoresMessage;

    // Start is called before the first frame update
    void Start()
    {
        scoresMessage = new ScoresMessage();
        MqttClientHandler.OnScoresRecieved += ScoreMessageReceived;
    }


    void ScoreMessageReceived(string msg)
    {
        scoresMessage = JsonUtility.FromJson<ScoresMessage>(msg);
    }

    // Update is called once per frame
    void Update()
    {
        //Pars
        hole1Par.text = scoresMessage.pars[0].ToString();
        hole2Par.text = scoresMessage.pars[1].ToString();
        hole3Par.text = scoresMessage.pars[2].ToString();
        hole4Par.text = scoresMessage.pars[3].ToString();
        hole5Par.text = scoresMessage.pars[4].ToString();
        hole6Par.text = scoresMessage.pars[5].ToString();
        hole7Par.text = scoresMessage.pars[6].ToString();
        hole8Par.text = scoresMessage.pars[7].ToString();
        hole9Par.text = scoresMessage.pars[8].ToString();

        //Scores
        hole1Score.text = scoresMessage.scores[0].ToString();
        hole2Score.text = scoresMessage.scores[1].ToString();
        hole3Score.text = scoresMessage.scores[2].ToString();
        hole4Score.text = scoresMessage.scores[3].ToString();
        hole5Score.text = scoresMessage.scores[4].ToString();
        hole6Score.text = scoresMessage.scores[5].ToString();
        hole7Score.text = scoresMessage.scores[6].ToString();
        hole8Score.text = scoresMessage.scores[7].ToString();
        hole9Score.text = scoresMessage.scores[8].ToString();

        if(scoresMessage.scores[0] == 0)
        {
            hole1Score.text = "";
        }
        if (scoresMessage.scores[1] == 0)
        {
            hole2Score.text = "";
        }
        if (scoresMessage.scores[2] == 0)
        {
            hole3Score.text = "";
        }
        if (scoresMessage.scores[3] == 0)
        {
            hole4Score.text = "";
        }
        if (scoresMessage.scores[4] == 0)
        {
            hole5Score.text = "";
        }
        if (scoresMessage.scores[5] == 0)
        {
            hole6Score.text = "";
        }
        if (scoresMessage.scores[6] == 0)
        {
            hole7Score.text = "";
        }
        if (scoresMessage.scores[7] == 0)
        {
            hole8Score.text = "";
        }
        if (scoresMessage.scores[8] == 0)
        {
            hole9Score.text = "";
        }

        int total = 0;
        for(int i = 0; i< 9; i++)
        {
            total += scoresMessage.scores[i];
        }
        totalScore.text = total.ToString();



    }
}
