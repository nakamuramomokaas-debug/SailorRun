using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResultScore : MonoBehaviour
{
    public int resultScore = 0;
    private TMP_Text scoreText;

    void Start()
    {
        resultScore = 0;
        scoreText = GetComponent<TMP_Text>();
        scoreText.text = "0";
    }

    void Update()
    {
        scoreText.SetText("Score:  {0}",resultScore);
    }
}
