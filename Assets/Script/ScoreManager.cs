using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public int score = 0;
    private TMP_Text scoreText;

    void Start()
    {
        score = 0;
        scoreText = GetComponent<TMP_Text>();
        scoreText.text = "0";
    }

    void Update()
    {
        //scoreText.text = score.ToString();
        scoreText.SetText("Score:  {0}",score);
    }

    public void ScoreAdd(int add)
    {
        score += add;
    }
}
