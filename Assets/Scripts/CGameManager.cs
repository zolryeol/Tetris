using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  모든 보드들의 조각들을 여기서 조합하고 
/// 타이머 게임오버등 게임의 전반적인 내용을 다룬다.
///  
/// 키인풋도 가지고 있다.
/// 
///  스코어등의 UI도 싱글턴으로 받아와서 데이터를 바꾸어준다. 
/// </summary>

public class CGameManager : MonoBehaviour
{
    /// 랜더용 보드지만 사실상 테트로미노와 데이터보드를 둘다 알고있는 변수
    RenderBoard sRenderBoard;   // 랜더용 보드 

    // 타이머
    float timer;
    [SerializeField]
    float waitingTime; // 이 변수를 만지면 자동으로 떨어지는 속도를 조절할 수 있다.

    // 게임오버
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

    // 단순한 타이머
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

    /// 회전엔 관한것 죄다 묶어둠 물론 대부분은 sRender에서 가져온것
    /// 일부가 매니저에 있기떄문에 꼬여있다.
    void RotationCheckAndRotate()
    {
        if (sRenderBoard.changeRota == true)
        {
            sRenderBoard.RotateRightFromTetromino();        // 일단 돌린다/

            sRenderBoard.ItetrominoWallCheck();

            if (sRenderBoard.overTop == true)   // 안전코드
            {
                sRenderBoard.movePos.y -= 1;
                sRenderBoard.overTop = false;
                return;
            }

            if (sRenderBoard.PreCheckForRotation() == false) sRenderBoard.RotateLeftFromTetromino();    // 돌려서 굳은블록이나 바닥이라면 반대로 돌린다.

            if (sRenderBoard.PreWallCheck() == false)       // 벽이 있으면 반대편으로 옮긴다.
            {
                if (sRenderBoard.testOut == 1) sRenderBoard.movePos.x -= 1;    // 오른쪽에 박으며
                else if (sRenderBoard.testOut == -1) sRenderBoard.movePos.x += 1; // 왼쪽에 박으면
            }
        }

        else // 회전방향 반대로 바꾸기 키를 누를 경우
        {
            sRenderBoard.RotateLeftFromTetromino();

            sRenderBoard.ItetrominoWallCheck();

            if (sRenderBoard.overTop == true)   // 안전코드
            {
                sRenderBoard.movePos.y -= 1;
                sRenderBoard.overTop = false;
                return;
            }

            if (sRenderBoard.PreCheckForRotation() == false) sRenderBoard.RotateRightFromTetromino();    // 돌려서 굳은블록이나 바닥이라면 반대로 돌린다.

            if (sRenderBoard.PreWallCheck() == false)       // 벽이 있으면 반대편으로 옮긴다.
            {
                if (sRenderBoard.testOut == 1) sRenderBoard.movePos.x -= 1;    // 오른쪽에 박으며
                else if (sRenderBoard.testOut == -1) sRenderBoard.movePos.x += 1; // 왼쪽에 박으면
            }

        }
    }

    void KeyInput()
    {
        // 일단 움직여보고 갈수없는 곳이라면 반대로 움직여준다.
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
            // 굳히는 코드가 들어가야한다.
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

        if (Input.GetKeyDown(KeyCode.Z)) // 방향 바꾸기 모드
        {
            // changeRota = changeRota ? false : true;
            sRenderBoard.changeRota = !sRenderBoard.changeRota; // 반대화 시키기 위에도 같음
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && sRenderBoard.isUsedHold == false)
        {
            // 홀드기능을 넣자
            sRenderBoard.sTetromino.HoldTetromino();

            if (sRenderBoard.sTetromino.rotationIndex != 0) sRenderBoard.sTetromino.rotationIndex = 0;
            //             if (sRenderBoard.sTetromino.holdingTetromino == 5)  // ITetromino가 자꾸 세워져서 억지로 예외처리
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

    // 게임오버체크 하는 함수이다.
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
