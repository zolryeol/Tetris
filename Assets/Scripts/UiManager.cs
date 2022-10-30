using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UI의 게임오브젝트와 변수들을 가지고 있으며 싱글톤으로 만들어둔다.
/// 다른곳에서 주로 데이터를 바꿀 것이다.
/// </summary>

public class UiManager : MonoBehaviour
{
    public static UiManager instance = null;

    public Text scoreText;

    public int dropScore;
    public int lineClearScore;
    private int totalScore;
    public Text level;
    public int nowLevel;

    public int TotalScore
    {
        get
        {
            return totalScore;
        }
    }
    public GameObject gameOverPanel; // gameOver판넬

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        dropScore = 0;
        lineClearScore = 0;

        scoreText.text = "Score " + totalScore;

        gameOverPanel.SetActive(false);
    }

    private void Update()
    {
        totalScore = dropScore + lineClearScore;
        scoreText.text = "Score " + totalScore;
        level.text = "Level  " + nowLevel;
    }
}
