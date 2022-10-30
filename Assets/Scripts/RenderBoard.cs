using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
///  ������� �����ϰ� ���������� ��������.
///  Tetromino Ŭ������  TetrisDataBoard Ŭ������ �������ִ�.
/// </summary>

public class RenderBoard : MonoBehaviour
{
    // ��Ʈ�ι̳밡 ���� ó�� �����ϴ� ��ġ
    public int startPosX;
    public int startPosY;

    // ���� ���õ� ��Ʈ�ι̳��� ������ �Ǵ��ϴ� ����
    public int nowTetrominonum;

    // �̵��� �� �ʿ��� ��ǥ
    public Vector2 movePos;

    // �����Ǵ� �������� ������
    public int[,] renderBoard;

    // ����Ʈ�ȿ� ����ִ� ������ �ٷ������
    static int tempArr = 0;

    public TetrisDataBoard sTetrisDataBoard;   // �⺻ ����
    public Tetromino sTetromino;               // ��Ʈ�ι̳�

    // ����ٲٱ� Ű�� ���� �Ҹ�
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

    //�����ΰ� ó���� �� ������ ��ġ������ �ʱ�ȭ���ش�.
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

    // �������� = �⺻����
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

        while (PreCheckMoveGhost(ghostY) == true) // ��Ʈ�� �ٴ�üũ
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

    // �������� + ��Ʈ�ι̳�
    public void ConsolidateTetrominoToBoard()
    {
        /// ��Ʈ ����
        PrintGhost();

        for (int i = 0; i < TetrisDataBoard.HEIGHTMAX; ++i)
        {
            for (int j = 0; j < TetrisDataBoard.WIDTHMAX; ++j)
            {
                int nowPosY = i + startPosY + (int)movePos.y;
                int nowPosX = j + startPosX + (int)movePos.x;

                if (TetrominosCheck(i, j) == true)
                {

                    // ���־������� ��Ʈ�ι̳� �ǿ� ��� �κ�
                    renderBoard[nowPosY, nowPosX] = sTetromino.tetroimino[nowTetrominonum, sTetromino.rotationIndex, i, j];
                }
            }
        }
    }

    // ��Ʈ�ι̳��� ��ĭ�� ������ ���ĭ�� �����ϱ����� �Լ�
    public bool TetrominosCheck(int _i, int _j)
    {
        if (_i < 4 && _j < 4 &&
            sTetromino.tetroimino[nowTetrominonum, sTetromino.rotationIndex, _i, _j] != (int)eDefine.EMPTY) return true;
        else return false;
    }

    //����Ŭ��� �ϴ� �κ�
    public void DeleteLine()
    {
        for (int i = 0; i < TetrisDataBoard.HEIGHTMAX; ++i)
        {
            int _hardBoxCount = 0;

            for (int j = 0; j < TetrisDataBoard.WIDTHMAX; ++j)
            {
                if (renderBoard[i, j] == (int)eDefine.HARDBOX) _hardBoxCount++; // ���ĭ�� �����ϸ鼭 �����࿡ ���� �ִ� ���� �����Ѵ�.

                if (_hardBoxCount == 10)    // ���� 10����� ���� ���� ��ä�����ٴ� ���̴�.
                {
                    for (int k = 1; k < TetrisDataBoard.WIDTHMAX - 1; ++k) // ���ο� 10�� ��������� �����ش�.
                    {
                        sTetrisDataBoard.TetrisBoard[i, k] = (int)eDefine.EMPTY;  // 
                    }
                    FillEmptyBlock(i); return;  // ���پ� ����� �Լ��� ������Ų��.                                                               
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
                /// ���� ���ﶧ���� ��������� �����ͼ� �����ִ� ���ܼ���
                if (sTetrisDataBoard.TetrisBoard[deletedLine + 1 + i, j] == (int)eDefine.DEADLINE) return;

                sTetrisDataBoard.TetrisBoard[deletedLine + i, j] = sTetrisDataBoard.TetrisBoard[deletedLine + 1 + i, j];
            }
        }
    }

    public void NewTetromino()
    {
        InitPos();

        // ��ũ�帮��Ʈ�� ����
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
    //                         int _nowYinArr = startPosY + i + (int)movePos.y; // �Ⱦ�?
    // 
    //                         if (_nowXinArr < 0) return false; // �迭�� �ڲ� ����⶧���� ������ ���Ͻ�Ű�� �κ�
    // 
    //                         if (TetrisDataBoard.WIDTHMAX - 1 <= _nowXinArr) // �캮�� �����
    //                         {
    //                             testOut = 1;
    //                             return false;
    //                         }
    // 
    //                         if (_nowXinArr <= 0) // �º��� �����
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

                    int _nowYinArr = startPosY + i + (int)movePos.y; // �Ⱦ�?

                    TopOverCheck(_nowYinArr);

                    if (_nowXinArr < 0 || TetrisDataBoard.WIDTHMAX - 1 < _nowXinArr) return false; // �迭�� �ڲ� ����⶧���� ������ ���Ͻ�Ű�� �κ�

                    if (TetrisDataBoard.WIDTHMAX - 1 <= _nowXinArr) // �캮�� �����
                    {
                        testOut = 1;
                        return false;
                    }

                    if (_nowXinArr <= 0) // �º��� �����
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
        if (TetrisDataBoard.HEIGHTMAX - 1 < _nowYinArr)      // õ�� �մ´ٸ�
        {
            overTop = true;
            return overTop;
        }
        else overTop = false;
        return overTop;
    }

    public bool PreCheckMove()
    {
        // ��ǲ�� �ް��� �ٷ� ����, true�� ���� false�� ����    
        for (int i = 0; i < 4; ++i)
        {
            for (int j = 0; j < 4; ++j)
            {
                /// 4x4 ��Ʈ�ι̳�迭�ȿ��� ������� �ƴ� ���� ã���� true 
                if (TetrominosCheck(i, j) == true)
                {
                    int _nowXinArr = startPosX + j + (int)movePos.x;
                    int _nowYinArr = startPosY + i + (int)movePos.y;

                    //Debug.Log("���� x�� " + _nowXinArr + " ���� y�� " + _nowYinArr);

                    if (_nowXinArr < 0) return false; // �迭�� �ڲ� ����⶧���� ������ ���Ͻ�Ű�� �κ�
                    if (_nowYinArr > 23) return false; // �迭�� �ڲ� ����⶧���� ������ ���Ͻ�Ű�� �κ�

                    if ((renderBoard[_nowYinArr, _nowXinArr] == (int)eDefine.WALL))
                        return false;

                    if ((renderBoard[_nowYinArr, _nowXinArr] == (int)eDefine.HARDBOX))
                        return false;

                    /// �̵����� �˾Ƹ��� ���ϰ� ���� �ȵȴ�.
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
        // ��ǲ�� �ް��� �ٷ� ����, true�� ���� false�� ����    
        for (int i = 0; i < 4; ++i)
        {
            for (int j = 0; j < 4; ++j)
            {
                /// 4x4 ��Ʈ�ι̳�迭�ȿ��� ������� �ƴ� ���� ã���� true 
                if (TetrominosCheck(i, j) == true)
                {
                    int _nowXinArr = startPosX + j + (int)movePos.x;
                    int _nowYinArr = startPosY + i + (int)movePos.y;

                    //Debug.Log("���� x�� " + _nowXinArr + " ���� y�� " + _nowYinArr);

                    if (_nowXinArr < 0) return false; // �迭�� �ڲ� ����⶧���� ������ ���Ͻ�Ű�� �κ�
                    if (_nowYinArr > 23) return false; // �迭�� �ڲ� ����⶧���� ������ ���Ͻ�Ű�� �κ�


                    if ((renderBoard[_nowYinArr + ghostOffset, _nowXinArr] == (int)eDefine.WALL))
                        return false;

                    if ((renderBoard[_nowYinArr + ghostOffset, _nowXinArr] == (int)eDefine.HARDBOX))
                        return false;

                    /// �̵����� �˾Ƹ��� ���ϰ� ���� �ȵȴ�.
                    // if ((gameArr[i + startPosY + (int)movePos.y, j + startPosX + (int)movePos.x] == (int)eDefine.WALL))
                    //  return false;
                    // if ((gameArr[i + startPosY + (int)movePos.y, j + startPosX + (int)movePos.x] == (int)eDefine.HARDBOX))
                    // return false;
                }
            }
        }
        return true;
    }


    // ȸ������ ���� ����üũ
    public bool PreCheckForRotation()
    {
        for (int i = 0; i < 4; ++i)
        {
            for (int j = 0; j < 4; ++j)
            {
                /// 4x4 ��Ʈ�ι̳�迭�ȿ��� ������� �ƴ� ���� ã���� true  �� �ڽ��� ��󳻰ڴ�.
                if (TetrominosCheck(i, j) == true)
                {
                    int _nowXinArr = startPosX + j + (int)movePos.x;
                    int _nowYinArr = startPosY + i + (int)movePos.y;

                    //Debug.Log("���� x�� " + _nowXinArr + " ���� y�� " + _nowYinArr);

                    if (_nowXinArr < 0 || TetrisDataBoard.WIDTHMAX - 1 < _nowXinArr) return false; // �迭�� �ڲ� ����⶧���� ������ ���Ͻ�Ű�� �κ�
                    if (_nowYinArr <= 0) return false;

                    if ((renderBoard[_nowYinArr, _nowXinArr] == (int)eDefine.WALL &&/*������ Y�� �߰��ڵ�*/ _nowYinArr == 0)) return false; // ���Ҵµ� ���°��� �ٴ��̰ų� ���� ����� ������ ���������Ұ��̴�.

                    if ((renderBoard[_nowYinArr, _nowXinArr] == (int)eDefine.HARDBOX)) return false;
                }
            }
        }
        return true;
    }

    // ��Ʈ���� ���带 ���鼭 lBoxes �ڽ����� ���� �ٲپ� �ش�.
    public void ChangeColor(int color)
    {
        if (288 <= tempArr) tempArr = 0;

        if (7000 < UiManager.instance.TotalScore)
        {
            if (color == (int)eDefine.HARDBOX) color = nowTetrominonum + 2;
        }
        else if (color == (int)eDefine.HARDBOX) color = 9;



        if (color == (int)eDefine.DEADLINE) color = 10;

        // ��Ʈ ��ȣ�� ��Ʈ�ι̳����� �ε����� +10 �ϸ� ��Ʈ��ȣ�� ��
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

                /// ��ģ�� ���ϴ� ģ���ΰ�.
                //if (_Y > TetrisDataBoard.HEIGHTMAX || _X > TetrisDataBoard.WIDTHMAX) { }

                if (TetrominosCheck(i, j) == true)
                    sTetrisDataBoard.TetrisBoard[_Y, _X] = (int)eDefine.HARDBOX;
            }
        }

        // �����Ҷ� ���� ���
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

    // Ÿ�̸ӿ� ���� �ٴ����� �������� �Լ�
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

    /// ��û�� ������ �ڶ��ϴ� üũ! while������ ���ľ� �� ���� ������?
    /// ��ͷ� �ٲ� �� ������?
    public void ItetrominoWallCheck()
    {
        Vector2 savePos;
        savePos.x = movePos.x;
        savePos.y = movePos.y;

        // ���ȴ� �����ϰ�
        if (nowTetrominonum == 5)
        {
            //���� �ճ� �ȶճ� �˻��Ѵ�.
            if (PreWallCheck() == false && testOut == -1) //������ ������ 
            {
                movePos.x += 1;

                if (PreWallCheck() == true) return;// �� �˻��Ѵ�. 

                else if (PreWallCheck() == false)
                {
                    movePos.x += 1;

                    if (PreWallCheck() == true) return;// �� �˻��Ѵ�. 

                    else if (PreWallCheck() == false)
                    {
                        movePos.x += 1;
                    }
                }
            }
            else if (PreWallCheck() == false && testOut == 1) // ������ ������
            {
                movePos.x -= 1;


                if (PreWallCheck() == true) return;// �� �˻��Ѵ�. 

                else if (PreWallCheck() == false)
                {
                    movePos.x -= 1;

                    if (PreWallCheck() == true) return;// �� �˻��Ѵ�. 

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

    // ��Ʈ�ι̳� Ŭ������ ������Ʈ�� �޾Ƽ� �״�� �Ѱ��ش�.
    public void RotateRightFromTetromino()
    {
        sTetromino.RotateRight(nowTetrominonum);
    }

    // ��Ʈ�ι̳� Ŭ������ ������Ʈ�� �޾Ƽ� �״�� �Ѱ��ش�.
    public void RotateLeftFromTetromino()
    {
        sTetromino.RotateLeft(nowTetrominonum);
    }
    public int GameOverLineFromTetromino(int _DeadLine, int _HoizentalX)
    {
        return sTetrisDataBoard.TetrisBoard[_DeadLine, _HoizentalX];
    }
}