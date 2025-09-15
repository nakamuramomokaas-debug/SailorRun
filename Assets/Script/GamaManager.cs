using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GamaManager : MonoBehaviour
{
    [SerializeField] PlayerController  playerScr;
    [SerializeField] GameObject titleObj;
    [SerializeField] GameObject resultObj;
    [SerializeField] GameObject scoreObj;
    [SerializeField] AudioManager audioMgr;

    public void StartGame()
    {
        audioMgr.DisisionSound();
        resultObj.SetActive(false);
        titleObj.SetActive(false);
        scoreObj.SetActive(true);

        playerScr.IsRun = true;
        playerScr.SetRunAnimator();
    }

    public void RetryGame()
    {
        audioMgr.DisisionSound();
        SceneManager.LoadScene("main");
        StartGame();
    }

    public void EndGame()
    {
        audioMgr.DisisionSound();
        SceneManager.LoadScene("main");
    }
}
