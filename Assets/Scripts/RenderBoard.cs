using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
///  보드들을 취합하고 랜더용으로 내보낸다.
///  Tetromino 클래스와  TetrisDataBoard 클래스를 가지고있다.
/// </summary>

public class RenderBoard : MonoBehaviour
{
    // 테트로미노가 가장 처음 등장하는 위치
    public int startPosX;
    public int startPosY;

    // 현재 선택된 테트로미노의 종류를 판단하는 변수
    public int nowTetrominonum;

    // 이동할 때 필요한 좌표
    public Vector2 movePos;

    // 랜더되는 실질적인 게임판
    public int[,] renderBoard;

    // 리스트안에 들어있는 값을들 다루기위함
    static int tempArr = 0;

    public TetrisDataBoard sTetrisDataBoard;   // 기본 보드
    public Tetromino sTetromino;               // 테트로미노

    // 방향바꾸기 키를 위한 불린
    public bool changeRota = true;
    public bool isUsedHold = false;

    public bool overTop = false;

    public int testOut;
    private void Awake()
    {
        sTetrisDataBoard = GameObject.FindObjectOfType<TetrisDataBoard>();

        sTetromino = GameObject.FindObjectOfType<Tetromino>();

        startPosX = TetrisDataBoard.STARTPOSX;
        startPosY = TetrisDataBoard.STARTPOSY;

        movePos = new Vector2(0, 0);

        renderBoard = new int[TetrisDataBoard.HEIGHTMAX, TetrisDataBoard.WIDTHMAX];
    }

    //무엇인가 처리한 후 설정된 위치값들을 초기화해준다.
    public void InitPos()
    {
        startPosX = TetrisDataBoard.STARTPOSX;
        startPosY = TetrisDataBoard.STARTPOSY;

        movePos.x = 0;
        movePos.y = 0;
    }

    public void InitPosForRestart()
    {
        startPosX = TetrisDataBoard.STARTPOSX;
        startPosY = TetrisDataBoard.STARTPOSY;

        if (nowTetrominonum == 5) startPosY = 20;
        else startPosY = TetrisDataBoard.STARTPOSY;

        movePos.x = 0;
        movePos.y = 0;
    }

    // 랜더보드 = 기본보드
    public void ConsolidateBoardToBoard()
    {
        for (int i = 0; i < TetrisDataBoard.HEIGHTMAX; ++i)
        {
            for (int j = 0; j < TetrisDataBoard.WIDTHMAX; ++j)
            {
                renderBoard[i, j] = sTetrisDataBoard.TetrisBoard[i, j];
            }
        }
    }

    void PrintGhost()
    {
        int ghostY = 0;

        while (PreCheckMoveGhost(ghostY) == true) // 고스트용 바닥체크
        {
            ghostY -= 1;
        }
        ghostY += 1;

        for (int i = 0; i < 4; ++i)
        {
            for (int j = 0; j < 4; ++j)
            {
                if (TetrominosCheck(i, j) == true)
                {
                    int _X = startPosX + j + (int)movePos.x;
                    int _Y = startPosY + i + (int)movePos.y;

                    renderBoard[_Y + ghostY, _X] = sTetromino.tetroimino[nowTetrominonum, sTetromino.rotationIndex, i, j] + 10;
                }
            }
        }
    }

    // 랜더보드 + 테트로미노
    public void ConsolidateTetrominoToBoard()
    {
        /// 고스트 관련
        PrintGhost();

        for (int i = 0; i < TetrisDataBoard.HEIGHTMAX; ++i)
        {
            for (int j = 0; j < TetrisDataBoard.WIDTHMAX; ++j)
            {
                int nowPosY = i + startPosY + (int)movePos.y;
                int nowPosX = j + startPosX + (int)movePos.x;

                if (TetrominosCheck(i, j) == true)
                {

                    // 비주얼적으로 테트로미노 판에 찍는 부분
                    renderBoard[nowPosY, nowPosX] = sTetromino.tetroimino[nowTetrominonum, sTetromino.rotationIndex, i, j];
                }
            }
        }
    }

    // 테트로미노의 빈칸을 제외한 블록칸을 조사하기위한 함수
    public bool TetrominosCheck(int _i, int _j)
    {
        if (_i < 4 && _j < 4 &&
            sTetromino.tetroimino[nowTetrominonum, sTetromino.rotationIndex, _i, _j] != (int)eDefine.EMPTY) return true;
        else return false;
    }

    //라인클리어를 하는 부분
    public void DeleteLine()
    {
        for (int i = 0; i < TetrisDataBoard.HEIGHTMAX; ++i)
        {
            int _hardBoxCount = 0;

            for (int j = 0; j < TetrisDataBoard.WIDTHMAX; ++j)
            {
                if (renderBoard[i, j] == (int)eDefine.HARDBOX) _hardBoxCount++; // 모든칸을 조사하면서 가로축에 굳어 있는 블럭을 조사한다.

                if (_hardBoxCount == 10)    // 블럭이 10개라는 말은 한줄 다채워졌다는 뜻이다.
                {
                    for (int k = 1; k < TetrisDataBoard.WIDTHMAX - 1; ++k) // 가로열 10개 빈공간으로 지워준다.
                    {
                        sTetrisDataBoard.TetrisBoard[i, k] = (int)eDefine.EMPTY;  // 
                    }
                    FillEmptyBlock(i); return;  // 한줄씩 떙기고 함수를 종류시킨다.                                                               
                }
            }
        }
    }

    public void FillEmptyBlock(int deletedLine)
    {
        SoundManager.instance.lineClear.Play();

        UiManager.instance.lineClearScore += 100;

        for (int i = 0; i < TetrisDataBoard.HEIGHTMAX - deletedLine - 1; ++i)
        {
            for (int j = 1; j < TetrisDataBoard.WIDTHMAX - 1; ++j)
            {
                /// 라인 지울때마다 데드라인이 내려와서 막아주는 예외설정
                if (sTetrisDataBoard.TetrisBoard[deletedLine + 1 + i, j] == (int)eDefine.DEADLINE) return;

                sTetrisDataBoard.TetrisBoard[deletedLine + i, j] = sTetrisDataBoard.TetrisBoard[deletedLine + 1 + i, j];
            }
        }
    }

    public void NewTetromino()
    {
        InitPos();

        // 링크드리스트로 구성
        sTetromino.TetrominoGenerator(nowTetrominonum);

        sTetromino.WhatIsNextTetrominos();

        isUsedHold = false;
    }

    //     public void ItetrominoCheck() 
    //     {
    //         if (nowTetrominonum == 5)
    //         {
    //             for (int i = 0; i < 4; ++i)
    //             {
    //                 for (int j = 0; j < 4; ++j)
    //                 {
    //                     if (TetrominosCheck(i, j) == true)
    //                     {
    //                         int _nowXinArr = startPosX + j + (int)movePos.x;
    // 
    //                         int _nowYinArr = startPosY + i + (int)movePos.y; // 안씀?
    // 
    //                         if (_nowXinArr < 0) return false; // 배열을 자꾸 벗어나기때문에 억지로 리턴시키는 부분
    // 
    //                         if (TetrisDataBoard.WIDTHMAX - 1 <= _nowXinArr) // 우벽을 벗어나면
    //                         {
    //                             testOut = 1;
    //                             return false;
    //                         }
    // 
    //                         if (_nowXinArr <= 0) // 좌벽을 벗어나면
    //                         {
    //                             testOut = -1;
    //                             return false;
    //                         }
    //                     }
    //                 }
    //             }
    //         }
    //     }
    public bool PreWallCheck()
    {
        for (int i = 0; i < 4; ++i)
        {
            for (int j = 0; j < 4; ++j)
            {
                if (TetrominosCheck(i, j) == true)
                {
                    int _nowXinArr = startPosX + j + (int)movePos.x;

                    int _nowYinArr = startPosY + i + (int)movePos.y; // 안씀?

                    TopOverCheck(_nowYinArr);

                    if (_nowXinArr < 0 || TetrisDataBoard.WIDTHMAX - 1 < _nowXinArr) return false; // 배열을 자꾸 벗어나기때문에 억지로 리턴시키는 부분

                    if (TetrisDataBoard.WIDTHMAX - 1 <= _nowXinArr) // 우벽을 벗어나면
                    {
                        testOut = 1;
                        return false;
                    }

                    if (_nowXinArr <= 0) // 좌벽을 벗어나면
                    {
                        testOut = -1;
                        return false;
                    }
                }
            }
        }
        return true;
    }

    public bool TopOverCheck(int _nowYinArr)
    {
        if (TetrisDataBoard.HEIGHTMAX - 1 < _nowYinArr)      // 천장 뚫는다면
        {
            overTop = true;
            return overTop;
        }
        else overTop = false;
        return overTop;
    }

    public bool PreCheckMove()
    {
        // 인풋을 받고나서 바로 실행, true면 진행 false면 리턴    
        for (int i = 0; i < 4; ++i)
        {
            for (int j = 0; j < 4; ++j)
            {
                /// 4x4 테트로미노배열안에서 빈공간이 아닌 것을 찾으면 true 
                if (TetrominosCheck(i, j) == true)
                {
                    int _nowXinArr = startPosX + j + (int)movePos.x;
                    int _nowYinArr = startPosY + i + (int)movePos.y;

                    //Debug.Log("현재 x값 " + _nowXinArr + " 현재 y값 " + _nowYinArr);

                    if (_nowXinArr < 0) return false; // 배열을 자꾸 벗어나기때문에 억지로 리턴시키는 부분
                    if (_nowYinArr > 23) return false; // 배열을 자꾸 벗어나기때문에 억지로 리턴시키는 부분

                    if ((renderBoard[_nowYinArr, _nowXinArr] == (int)eDefine.WALL))
                        return false;

                    if ((renderBoard[_nowYinArr, _nowXinArr] == (int)eDefine.HARDBOX))
                        return false;

                    /// 이따위로 알아먹지 못하게 쓰면 안된다.
                    // if ((gameArr[i + startPosY + (int)movePos.y, j + startPosX + (int)movePos.x] == (int)eDefine.WALL))
                    //  return false;
                    // if ((gameArr[i + startPosY + (int)movePos.y, j + startPosX + (int)movePos.x] == (int)eDefine.HARDBOX))
                    // return false;
                }
            }
        }
        return true;
    }

    public bool PreCheckMoveGhost(int ghostOffset)
    {
        // 인풋을 받고나서 바로 실행, true면 진행 false면 리턴    
        for (int i = 0; i < 4; ++i)
        {
            for (int j = 0; j < 4; ++j)
            {
                /// 4x4 테트로미노배열안에서 빈공간이 아닌 것을 찾으면 true 
                if (TetrominosCheck(i, j) == true)
                {
                    int _nowXinArr = startPosX + j + (int)movePos.x;
                    int _nowYinArr = startPosY + i + (int)movePos.y;

                    //Debug.Log("현재 x값 " + _nowXinArr + " 현재 y값 " + _nowYinArr);

                    if (_nowXinArr < 0) return false; // 배열을 자꾸 벗어나기때문에 억지로 리턴시키는 부분
                    if (_nowYinArr > 23) return false; // 배열을 자꾸 벗어나기때문에 억지로 리턴시키는 부분


                    if ((renderBoard[_nowYinArr + ghostOffset, _nowXinArr] == (int)eDefine.WALL))
                        return false;

                    if ((renderBoard[_nowYinArr + ghostOffset, _nowXinArr] == (int)eDefine.HARDBOX))
                        return false;

                    /// 이따위로 알아먹지 못하게 쓰면 안된다.
                    // if ((gameArr[i + startPosY + (int)movePos.y, j + startPosX + (int)movePos.x] == (int)eDefine.WALL))
                    //  return false;
                    // if ((gameArr[i + startPosY + (int)movePos.y, j + startPosX + (int)movePos.x] == (int)eDefine.HARDBOX))
                    // return false;
                }
            }
        }
        return true;
    }


    // 회전만을 위한 프리체크
    public bool PreCheckForRotation()
    {
        for (int i = 0; i < 4; ++i)
        {
            for (int j = 0; j < 4; ++j)
            {
                /// 4x4 테트로미노배열안에서 빈공간이 아닌 것을 찾으면 true  즉 박스만 골라내겠다.
                if (TetrominosCheck(i, j) == true)
                {
                    int _nowXinArr = startPosX + j + (int)movePos.x;
                    int _nowYinArr = startPosY + i + (int)movePos.y;

                    //Debug.Log("현재 x값 " + _nowXinArr + " 현재 y값 " + _nowYinArr);

                    if (_nowXinArr < 0 || TetrisDataBoard.WIDTHMAX - 1 < _nowXinArr) return false; // 배열을 자꾸 벗어나기때문에 억지로 리턴시키는 부분
                    if (_nowYinArr <= 0) return false;

                    if ((renderBoard[_nowYinArr, _nowXinArr] == (int)eDefine.WALL &&/*우측의 Y는 추가코드*/ _nowYinArr == 0)) return false; // 돌았는데 도는곳이 바닥이거나 굳은 블록이 있으면 못돌리게할것이다.

                    if ((renderBoard[_nowYinArr, _nowXinArr] == (int)eDefine.HARDBOX)) return false;
                }
            }
        }
        return true;
    }

    // 테트리스 보드를 돌면서 lBoxes 박스들의 색을 바꾸어 준다.
    public void ChangeColor(int color)
    {
        if (288 <= tempArr) tempArr = 0;

        if (7000 < UiManager.instance.TotalScore)
        {
            if (color == (int)eDefine.HARDBOX) color = nowTetrominonum + 2;
        }
        else if (color == (int)eDefine.HARDBOX) color = 9;



        if (color == (int)eDefine.DEADLINE) color = 10;

        // 고스트 번호들 테트로미노종류 인덱스에 +10 하면 고스트번호가 됌
        if (color == 12 || color == 13 || color == 14 || color == 15 || color == 16 || color == 17 || color == 18) color = 11;

        sTetrisDataBoard.lBoxes[tempArr].GetComponent<Renderer>().material = sTetromino.tetrominoMaterials.materials[color];
        tempArr++;
    }

    public void SetBottom()
    {
        for (int i = 0; i < TetrisDataBoard.HEIGHTMAX; ++i)
        {
            for (int j = 0; j < TetrisDataBoard.WIDTHMAX; ++j)
            {
                int _X = startPosX + j + (int)movePos.x;
                int _Y = startPosY + i + (int)movePos.y;

                /// 이친구 뭐하는 친구인가.
                //if (_Y > TetrisDataBoard.HEIGHTMAX || _X > TetrisDataBoard.WIDTHMAX) { }

                if (TetrominosCheck(i, j) == true)
                    sTetrisDataBoard.TetrisBoard[_Y, _X] = (int)eDefine.HARDBOX;
            }
        }

        // 착지할때 사운드 출력
        SoundManager.instance.dropSound.Play();
        UiManager.instance.dropScore += 10;
        NewTetromino();

        sTetromino.ResetTetrominoArr();
    }

    public void ChangeMaterial()
    {
        for (int i = 0; i < TetrisDataBoard.HEIGHTMAX; ++i)
        {
            for (int j = 0; j < TetrisDataBoard.WIDTHMAX; ++j)
            {
                ChangeColor(renderBoard[i, j]);
            }
        }
    }

    public void InitializeForRestart()
    {
        InitPos();

        sTetromino.ResetTetrominoArr();

        sTetrisDataBoard.RestartInit();

        sTetromino.InitialrizeForRestart();
    }

    // 타이머에 따라서 바닥으로 떨어지는 함수
    public void AutoDown(ref float _timer)
    {
        movePos.y -= 1;
        if (PreCheckMove() == false)
        {
            movePos.y += 1;
            SetBottom();
            _timer = 0;
        }
    }

    /// 엉청난 뎁쓰를 자랑하는 체크! while문으로 고쳐쓸 수 있지 않을까?
    /// 재귀로 바꿀 수 있을듯?
    public void ItetrominoWallCheck()
    {
        Vector2 savePos;
        savePos.x = movePos.x;
        savePos.y = movePos.y;

        // 돌렸다 가정하고
        if (nowTetrominonum == 5)
        {
            //벽을 뚫나 안뚫나 검사한다.
            if (PreWallCheck() == false && testOut == -1) //왼쪽을 뚫을때 
            {
                movePos.x += 1;

                if (PreWallCheck() == true) return;// 또 검사한다. 

                else if (PreWallCheck() == false)
                {
                    movePos.x += 1;

                    if (PreWallCheck() == true) return;// 또 검사한다. 

                    else if (PreWallCheck() == false)
                    {
                        movePos.x += 1;
                    }
                }
            }
            else if (PreWallCheck() == false && testOut == 1) // 우측을 뚫을때
            {
                movePos.x -= 1;


                if (PreWallCheck() == true) return;// 또 검사한다. 

                else if (PreWallCheck() == false)
                {
                    movePos.x -= 1;

                    if (PreWallCheck() == true) return;// 또 검사한다. 

                    else if (PreWallCheck() == false)
                    {
                        movePos.x -= 1;
                    }
                }
            }
        }
    }
    public void UpdateNowTetromino()
    {
        nowTetrominonum = sTetromino.llTetrominos.First.Value;

        //if (nowTetrominonum == 5) startPosY = 21;
        //else
        //startPosY = 21;
    }

    // 테트로미노 클래스의 로테이트를 받아서 그대로 넘겨준다.
    public void RotateRightFromTetromino()
    {
        sTetromino.RotateRight(nowTetrominonum);
    }

    // 테트로미노 클래스의 로테이트를 받아서 그대로 넘겨준다.
    public void RotateLeftFromTetromino()
    {
        sTetromino.RotateLeft(nowTetrominonum);
    }
    public int GameOverLineFromTetromino(int _DeadLine, int _HoizentalX)
    {
        return sTetrisDataBoard.TetrisBoard[_DeadLine, _HoizentalX];
    }
}