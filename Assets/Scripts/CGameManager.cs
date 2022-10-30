using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  ��� ������� �������� ���⼭ �����ϰ� 
/// Ÿ�̸� ���ӿ����� ������ �������� ������ �ٷ��.
///  
/// Ű��ǲ�� ������ �ִ�.
/// 
///  ���ھ���� UI�� �̱������� �޾ƿͼ� �����͸� �ٲپ��ش�. 
/// </summary>

public class CGameManager : MonoBehaviour
{
    /// ������ �������� ��ǻ� ��Ʈ�ι̳�� �����ͺ��带 �Ѵ� �˰��ִ� ����
    RenderBoard sRenderBoard;   // ������ ���� 

    // Ÿ�̸�
    float timer;
    [SerializeField]
    float waitingTime; // �� ������ ������ �ڵ����� �������� �ӵ��� ������ �� �ִ�.

    // ���ӿ���
    bool isGameOver;
    public const int GameOverLine = 22;
    int level;

    private void Awake()
    {
        sRenderBoard = GameObject.FindObjectOfType<RenderBoard>();
        level = 1;

        timer = 0.0f;
        waitingTime = 1;

        isGameOver = false;
    }

    // �ܼ��� Ÿ�̸�
    void Timer()
    {
        timer += Time.deltaTime;
        if (UiManager.instance.TotalScore > 500 * level)
        {
            waitingTime = waitingTime * 0.9f;

            if (level * 500 < 15000) level++;
        }

        if (timer > waitingTime)
        {
            timer = 0;
            sRenderBoard.AutoDown(ref timer);
        }
    }

    /// ȸ���� ���Ѱ� �˴� ����� ���� ��κ��� sRender���� �����°�
    /// �Ϻΰ� �Ŵ����� �ֱ⋚���� �����ִ�.
    void RotationCheckAndRotate()
    {
        if (sRenderBoard.changeRota == true)
        {
            sRenderBoard.RotateRightFromTetromino();        // �ϴ� ������/

            sRenderBoard.ItetrominoWallCheck();

            if (sRenderBoard.overTop == true)   // �����ڵ�
            {
                sRenderBoard.movePos.y -= 1;
                sRenderBoard.overTop = false;
                return;
            }

            if (sRenderBoard.PreCheckForRotation() == false) sRenderBoard.RotateLeftFromTetromino();    // ������ ��������̳� �ٴ��̶�� �ݴ�� ������.

            if (sRenderBoard.PreWallCheck() == false)       // ���� ������ �ݴ������� �ű��.
            {
                if (sRenderBoard.testOut == 1) sRenderBoard.movePos.x -= 1;    // �����ʿ� ������
                else if (sRenderBoard.testOut == -1) sRenderBoard.movePos.x += 1; // ���ʿ� ������
            }
        }

        else // ȸ������ �ݴ�� �ٲٱ� Ű�� ���� ���
        {
            sRenderBoard.RotateLeftFromTetromino();

            sRenderBoard.ItetrominoWallCheck();

            if (sRenderBoard.overTop == true)   // �����ڵ�
            {
                sRenderBoard.movePos.y -= 1;
                sRenderBoard.overTop = false;
                return;
            }

            if (sRenderBoard.PreCheckForRotation() == false) sRenderBoard.RotateRightFromTetromino();    // ������ ��������̳� �ٴ��̶�� �ݴ�� ������.

            if (sRenderBoard.PreWallCheck() == false)       // ���� ������ �ݴ������� �ű��.
            {
                if (sRenderBoard.testOut == 1) sRenderBoard.movePos.x -= 1;    // �����ʿ� ������
                else if (sRenderBoard.testOut == -1) sRenderBoard.movePos.x += 1; // ���ʿ� ������
            }

        }
    }

    void KeyInput()
    {
        // �ϴ� ���������� �������� ���̶�� �ݴ�� �������ش�.
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            sRenderBoard.movePos.x -= 1;
            if (sRenderBoard.PreCheckMove() == false) sRenderBoard.movePos.x += 1;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            sRenderBoard.movePos.x += 1;
            if (sRenderBoard.PreCheckMove() == false) sRenderBoard.movePos.x -= 1;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            RotationCheckAndRotate();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            // ������ �ڵ尡 �����Ѵ�.
            sRenderBoard.movePos.y -= 1;
            if (sRenderBoard.PreCheckMove() == false)
            {
                sRenderBoard.movePos.y += 1;
                SetBottomFromRender();
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            while (sRenderBoard.PreCheckMove() == true)
            {
                sRenderBoard.movePos.y -= 1;
            }
            sRenderBoard.movePos.y += 1;

            SetBottomFromRender();
        }

        if (Input.GetKeyDown(KeyCode.Z)) // ���� �ٲٱ� ���
        {
            // changeRota = changeRota ? false : true;
            sRenderBoard.changeRota = !sRenderBoard.changeRota; // �ݴ�ȭ ��Ű�� ������ ����
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && sRenderBoard.isUsedHold == false)
        {
            // Ȧ������ ����
            sRenderBoard.sTetromino.HoldTetromino();

            if (sRenderBoard.sTetromino.rotationIndex != 0) sRenderBoard.sTetromino.rotationIndex = 0;
            //             if (sRenderBoard.sTetromino.holdingTetromino == 5)  // ITetromino�� �ڲ� �������� ������ ����ó��
            //             {
            //                 sRenderBoard.RotateRightFromTetromino();
            //                 sRenderBoard.movePos.y -= 1;
            //             }

            sRenderBoard.isUsedHold = true;

            sRenderBoard.InitPos();

            sRenderBoard.sTetromino.WhatIsHoldingTetoromino();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ReStart();

            timer = 0;

            SoundManager.instance.bgm.Play();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneMG.instance.GoTitleScene();

        }

    }

    void SetBottomFromRender()
    {
        sRenderBoard.SetBottom();
        timer = 0.0f;
    }

    void ReStart()
    {
        isGameOver = false;

        sRenderBoard.InitializeForRestart();

        UiManager.instance.dropScore = 0;

        UiManager.instance.lineClearScore = 0;

        SoundManager.instance.bgm.Play();

        UiManager.instance.gameOverPanel.SetActive(false);
    }

    // ���ӿ���üũ �ϴ� �Լ��̴�.
    void CheckGameOver()
    {
        //GameOverLine = 22;

        for (int x = 1; x < TetrisDataBoard.WIDTHMAX - 1; ++x)
        {
            if (sRenderBoard.GameOverLineFromTetromino(GameOverLine, x) == 99)
            {
                isGameOver = true;
            }
        }

        if (isGameOver == true)
        {
            UiManager.instance.gameOverPanel.SetActive(true);
            SoundManager.instance.bgm.Pause();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ReStart();

            SoundManager.instance.bgm.Play();

            UiManager.instance.gameOverPanel.SetActive(false);
        }
    }

    void Update()
    {
        sRenderBoard.UpdateNowTetromino();

        CheckGameOver();

        if (isGameOver != true)
        {
            Timer();

            sRenderBoard.ConsolidateBoardToBoard();

            KeyInput();

            sRenderBoard.DeleteLine();

            sRenderBoard.ConsolidateTetrominoToBoard();

            sRenderBoard.ChangeMaterial();

            UiManager.instance.nowLevel = level;
        }
    }
}
