using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UI�� ���ӿ�����Ʈ�� �������� ������ ������ �̱������� �����д�.
/// �ٸ������� �ַ� �����͸� �ٲ� ���̴�.
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
    public GameObject gameOverPanel; // gameOver�ǳ�

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
