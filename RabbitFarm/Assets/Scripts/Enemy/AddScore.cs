using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddScore : MonoBehaviour
{
    public static AddScore addInstance;
    public int currentScore;
    public Text[] scoreText;
    public void Awake()
    {
        addInstance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        currentScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(scoreText[1])
        {
            scoreText[1].text = currentScore.ToString();
        }
        if(scoreText[2])
        {
            scoreText[2].text = currentScore.ToString();
        }
    }

    public void SetScoreText()
    {
        currentScore += Enemy.enemyInstance.Score();
        scoreText[0].text = "점수 : " + currentScore.ToString();
    }
}
