using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� ����� �� ���̵��� ��ӹ޾����� �������� MonoBehaviour ����� �ʹ� ���� ��� �̹��� �׳� Ŭ������ �ٽ� ¥����
/// </summary>

static class BOXTYPE
{
    public const int WALL = 0;
    public const int EMPTY = 1;
    public const int FILLBOX = 2;
    public const int HARDBOX = 9;
    public const int DEADLINE = 98;
}
static class WHOWIN
{
    public const int LEFTWIN = 444;
    public const int RIGHTWIN = 666;
    public const int NOONEWIN = 555;
}
public class TetrominoForBattle
{
    public TetrominoForBattle() { }

    public int[,,,] tetroimino = new int[7, 4, 4, 4]// ��Ʈ�ι̳�� 7�����̴�. 
         {
            {
                {
                // S ��Ʈ�ι̳�
                    { 1, 2, 2, 1 },
                    { 1, 1, 2, 2 },
                    { 1, 1, 1, 1 },
                    { 1, 1, 1, 1 },
                },
                {
                    { 1, 1, 1, 2 },
                    { 1, 1, 2, 2 },
                    { 1, 1, 2, 1 },
                    { 1, 1, 1, 1 },
                },
                {
                    { 1, 1, 1, 1 },
                    { 1, 2, 2, 1 },
                    { 1, 1, 2, 2 },
                    { 1, 1, 1, 1 },
                },
                {
                    { 1, 1, 2, 1 },
                    { 1, 2, 2, 1 },
                    { 1, 2, 1, 1 },
                    { 1, 1, 1, 1 },
                }
            },

                // Z ��Ʈ�ι̳�
             {
                {
                    { 1, 1, 3, 3 },
                    { 1, 3, 3, 1 },
                    { 1, 1, 1, 1 },
                    { 1, 1, 1, 1 },
                },
                {
                    { 1, 1, 3, 1 },
                    { 1, 1, 3, 3 },
                    { 1, 1, 1, 3 },
                    { 1, 1, 1, 1 },
                },
                {
                    { 1, 1, 1, 1 },
                    { 1, 1, 3, 3 },
                    { 1, 3, 3, 1 },
                    { 1, 1, 1, 1 },
                },
                {
                    { 1, 3, 1, 1 },
                    { 1, 3, 3, 1 },
                    { 1, 1, 3, 1 },
                    { 1, 1, 1, 1 },
                }
            },

             // T ��Ʈ�ι̳�
         {
            {
                { 1, 1, 4, 1 },
                { 1, 4, 4, 4 },
                { 1, 1, 1, 1 },
                { 1, 1, 1, 1 }
            },
            {
                { 1, 1, 4, 1 },
                { 1, 1, 4, 4 },
                { 1, 1, 4, 1 },
                { 1, 1, 1, 1 },
            },
            {
                { 1, 1, 1, 1 },
                { 1, 4, 4, 4 },
                { 1, 1, 4, 1 },
                { 1, 1, 1, 1 },
            },
            {
                { 1, 1, 4, 1 },
                { 1, 4, 4, 1 },
                { 1, 1, 4, 1 },
                { 1, 1, 1, 1 },
            }
            },

            // L ��Ʈ�ι̳�
           {
            {
                { 1, 1, 1, 5 },
                { 1, 5, 5, 5 },
                { 1, 1, 1, 1 },
                { 1, 1, 1, 1 }
            },
            {
                { 1, 1, 5, 1 },
                { 1, 1, 5, 1 },
                { 1, 1, 5, 5 },
                { 1, 1, 1, 1 },
            },
            {
                { 1, 1, 1, 1 },
                { 1, 5, 5, 5 },
                { 1, 5, 1, 1 },
                { 1, 1, 1, 1 },
            },
            {
                { 1, 5, 5, 1 },
                { 1, 1, 5, 1 },
                { 1, 1, 5, 1 },
                { 1, 1, 1, 1 },
            }
            },
            // J ��Ʈ�ι̳�
           {
            {
                { 1, 6, 1, 1 },
                { 1, 6, 6, 6 },
                { 1, 1, 1, 1 },
                { 1, 1, 1, 1 }
            },
            {
                { 1, 1, 6, 6 },
                { 1, 1, 6, 1 },
                { 1, 1, 6, 1 },
                { 1, 1, 1, 1 },
            },
            {
                { 1, 1, 1, 1 },
                { 1, 6, 6, 6 },
                { 1, 1, 1, 6 },
                { 1, 1, 1, 1 },
            },
            {
                { 1, 1, 6, 1 },
                { 1, 1, 6, 1 },
                { 1, 6, 6, 1 },
                { 1, 1, 1, 1 },
            }
            },
            // I ��Ʈ�ι̳�
            {
            {
                { 1, 1, 1, 1 },
                { 7, 7, 7, 7 },
                { 1, 1, 1, 1 },
                { 1, 1, 1, 1 }
            },
            {
                { 1, 1, 7, 1 },
                { 1, 1, 7, 1 },
                { 1, 1, 7, 1 },
                { 1, 1, 7, 1 },
            },
            {
                { 1, 1, 1, 1 },
                { 1, 1, 1, 1 },
                { 7, 7, 7, 7 },
                { 1, 1, 1, 1 },
            },
            {
                { 1, 7, 1, 1 },
                { 1, 7, 1, 1 },
                { 1, 7, 1, 1 },
                { 1, 7, 1, 1 },
            }
            },
            // O��Ʈ�ι̳�
           {
            {
                { 1, 8, 8, 1 },
                { 1, 8, 8, 1 },
                { 1, 1, 1, 1 },
                { 1, 1, 1, 1 }
            },
            {
                { 1, 8, 8, 1 },
                { 1, 8, 8, 1 },
                { 1, 1, 1, 1 },
                { 1, 1, 1, 1 },
            },
            {
                { 1, 8, 8, 1 },
                { 1, 8, 8, 1 },
                { 1, 1, 1, 1 },
                { 1, 1, 1, 1 },
            },
            {
                { 1, 8, 8, 1 },
                { 1, 8, 8, 1 },
                { 1, 1, 1, 1 },
                { 1, 1, 1, 1 },
            }
            },
         };
}

public class TetrisDataBoardForBattle
{
    public TetrisDataBoardForBattle()
    {
        InitDataBoardForBattle();
    }

    public const int HEIGHTMAX = 24;
    public const int WIDTHMAX = 12;
    public const int GameOVerLine = 22;

    public int[,] tetrisBoard = new int[HEIGHTMAX, WIDTHMAX];

    // ��, �����, ��������� �������ָ� �ʱ�ȭ�Ѵ�.
    void InitDataBoardForBattle()
    {
        for (int i = 0; i < HEIGHTMAX; ++i)
        {
            for (int j = 0; j < WIDTHMAX; ++j)
            {
                if (j == 0 || j == WIDTHMAX - 1 || i == 0)
                {
                    tetrisBoard[i, j] = BOXTYPE.WALL;
                }
                else if (i == GameOVerLine)
                {
                    tetrisBoard[i, j] = BOXTYPE.DEADLINE;
                }
                else
                {
                    tetrisBoard[i, j] = BOXTYPE.EMPTY;
                }
            }
        }
    }
}

/// <summary>
/// �÷��̾� Ŭ������. 
/// ��� ���صΰ� ��ǻ� ��������� �Ȱ���.
/// 
/// ��Ʋ��忡�� ��Ŭ������ �÷��̾�1, �÷��̾�2 �� ���� ������ ���������̴�.
/// �����ִ� ��Ʈ�ι̳� �� �����ͺ��� Ŭ������ ������ ���� ����
/// </summary>

public class Player
{
    public const int FIRSTHOLDING = 44;
    public Player()
    {
        //tetrominoQ = new Queue<int>();
    }

    public LinkedList<Queue<int>> ListTetrominoSet;
    //public Queue<int> tetrominoQ;

    public TetrisDataBoardForBattle tetrisDataboard_battle;
    public TetrominoForBattle tetromino_battle;
    NoRedundancyRandom tetrominoPocket;

    public int preViewTetrominos;

    public int[,] renderBoard;
    public int[,] holdingRenderBoard;
    int nowRoationIndex;
    public int nowTetrominoTypeIndex;
    public int holdingTetromino;
    public bool isRotationChanged = false;

    int wallOut;
    bool overTop_battle;

    public int attackLineCount;
    public bool isUsedHolding;

    public float timer;
    float waitingTime = 0.85f;

    ///�޺� ����
    bool isComboStart = false;
    public bool isSetBottom = false;
    private int comboCount = 0;

    public void PrintGhost(Vector2 _startPos, Vector2 movePos)
    {
        int ghostY = 0;

        while (PreCheckMoveGhost(ghostY, _startPos, movePos) == true) // ��Ʈ�� �ٴ�üũ
        {
            ghostY -= 1;
        }
        ghostY += 1;

        for (int i = 0; i < 4; ++i)
        {
            for (int j = 0; j < 4; ++j)
            {
                if (TetrominosCheck_Battle(i, j) == true)
                {
                    int _X = (int)_startPos.x + j + (int)movePos.x;
                    int _Y = (int)_startPos.y + i + (int)movePos.y;

                    renderBoard[_Y + ghostY, _X] = tetromino_battle.tetroimino[nowTetrominoTypeIndex, nowRoationIndex, i, j] + 10;
                }
            }
        }
    }

    public bool PreCheckMoveGhost(int ghostOffset, Vector2 _startPos, Vector2 _movePos)
    {
        // ��ǲ�� �ް��� �ٷ� ����, true�� ���� false�� ����    
        for (int i = 0; i < 4; ++i)
        {
            for (int j = 0; j < 4; ++j)
            {
                /// 4x4 ��Ʈ�ι̳�迭�ȿ��� ������� �ƴ� ���� ã���� true 
                if (TetrominosCheck_Battle(i, j) == true)
                {
                    int _nowXinArr = (int)_startPos.x + j + (int)_movePos.x;
                    int _nowYinArr = (int)_startPos.y + i + (int)_movePos.y;

                    //Debug.Log("���� x�� " + _nowXinArr + " ���� y�� " + _nowYinArr);

                    if (_nowXinArr < 0) return false; // �迭�� �ڲ� ����⶧���� ������ ���Ͻ�Ű�� �κ�
                    if (_nowYinArr > 23) return false; // �迭�� �ڲ� ����⶧���� ������ ���Ͻ�Ű�� �κ�


                    if ((renderBoard[_nowYinArr + ghostOffset, _nowXinArr] == BOXTYPE.WALL))
                        return false;

                    if ((renderBoard[_nowYinArr + ghostOffset, _nowXinArr] == BOXTYPE.HARDBOX))
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

    public void InitPlayerBoard_Battle()
    {
        nowRoationIndex = 0;
        attackLineCount = 0;

        holdingTetromino = FIRSTHOLDING;

        tetrisDataboard_battle = new TetrisDataBoardForBattle();

        tetromino_battle = new TetrominoForBattle();

        renderBoard = new int[TetrisDataBoardForBattle.HEIGHTMAX, TetrisDataBoardForBattle.WIDTHMAX];

        holdingRenderBoard = new int[4, 4];

        /// ��Ʈ�ι̳� �����ؼ� �׾Ƶδ� �κ�
        tetrominoPocket = new NoRedundancyRandom(); // �����ڿ��� ť new ���ش�.

        ListTetrominoSet = new LinkedList<Queue<int>>();

        ListTetrominoSet.AddFirst(tetrominoPocket.NRDDCRANDOM(0, 6));    /// �ߺ����� 7�� �����ؼ� ù��°�� �ִ´�.

        nowTetrominoTypeIndex = 0;
        nowTetrominoTypeIndex = ListTetrominoSet.First.Value.Dequeue();
        preViewTetrominos = ListTetrominoSet.First.Value.Peek();
    }

    public void TetrominoToRenderBoard_Battle(Vector2 _movePos, Vector2 _startPos)
    {
        PrintGhost(_startPos, _movePos);

        // ���������ǿ� ��Ʈ�ι̳� ����ִ� �Լ�
        /// 
        for (int i = 0; i < TetrisDataBoardForBattle.HEIGHTMAX; ++i)
        {
            for (int j = 0; j < TetrisDataBoardForBattle.WIDTHMAX; ++j)
            {
                int _nowPosX = j + (int)_startPos.x + (int)_movePos.x;
                int _nowPosY = i + (int)_startPos.y + (int)_movePos.y;

                if (TetrominosCheck_Battle(i, j) == true) // ��Ʈ�ι̳��� ��ĭ �����ϰ� ����ִ� �Լ�
                {
                    renderBoard[_nowPosY, _nowPosX] = tetromino_battle.tetroimino[nowTetrominoTypeIndex, nowRoationIndex, i, j];
                }
                if (i < 4 && j < 4) // ��Ʈ�ι̳��� ��ĭ �����ϰ� ����ִ� �Լ�
                {
                    if (holdingTetromino == FIRSTHOLDING) continue;
                    holdingRenderBoard[i, j] = tetromino_battle.tetroimino[holdingTetromino, 0, i, j];
                }
            }
        }
    }

    public void DataBoardToRenderBoard_Battle()
    {
        // ���������ǿ� �����ͺ��� ����ִ� �Լ�
        for (int i = 0; i < TetrisDataBoardForBattle.HEIGHTMAX; ++i)
        {
            for (int j = 0; j < TetrisDataBoardForBattle.WIDTHMAX; ++j)
            {
                renderBoard[i, j] = tetrisDataboard_battle.tetrisBoard[i, j];
            }
        }
    }

    public bool TetrominosCheck_Battle(int _i, int _j)
    {
        //��Ʈ�ι̳� 4X4 �̱⶧���� 4������ ����
        // ��ĭ(Empty)�� �Ű澲�� ���� ���̴�
        if (_i < 4 && _j < 4 &&
           tetromino_battle.tetroimino[nowTetrominoTypeIndex, nowRoationIndex, _i, _j] != BOXTYPE.EMPTY) return true;
        else return false;
    }

    public bool PreCheckMoveForBattle(Vector2 _movePos, Vector2 _startPos)
    {
        // ��ǲ�� �ް��� �ٷ� ����, true�� ���� false�� ����    
        for (int i = 0; i < 4; ++i)
        {
            for (int j = 0; j < 4; ++j)
            {
                /// 4x4 ��Ʈ�ι̳�迭�ȿ��� ������� �ƴ� ���� ã���� true 
                if (TetrominosCheck_Battle(i, j) == true)
                {
                    int _nowXinArr = (int)_startPos.x + j + (int)_movePos.x;
                    int _nowYinArr = (int)_startPos.y + i + (int)_movePos.y;

                    //Debug.Log("���� x�� " + _nowXinArr + " ���� y�� " + _nowYinArr);

                    if (_nowYinArr < 0 || TetrisDataBoardForBattle.HEIGHTMAX - 1 < _nowYinArr) return false; // �迭�� �ڲ� ����⶧���� ������ ���Ͻ�Ű�� �κ�
                    if (_nowXinArr > TetrisDataBoardForBattle.WIDTHMAX - 1) return false; // �迭�� �ڲ� ����⶧���� ������ ���Ͻ�Ű�� �κ�

                    if ((renderBoard[_nowYinArr, _nowXinArr] == BOXTYPE.WALL))
                        return false;

                    if ((renderBoard[_nowYinArr, _nowXinArr] == BOXTYPE.HARDBOX))
                        return false;
                }
            }
        }
        return true;
    }

    public void AttackLine(ref int _attackCount)    /// ������ ����ī��Ʈ�� �����ͼ� ���� ����� ���δ�.
    {
        for (int k = 1; k < _attackCount; ++k) // ���� ���θ�ŭ  �ݺ��Ѵ�.
        {
            for (int i = TetrisDataBoardForBattle.GameOVerLine; 1 < i; --i)
            {
                for (int j = 1; j < TetrisDataBoardForBattle.WIDTHMAX - 1; ++j)
                {
                    /// ���� ���ﶧ���� ��������� �����ͼ� �����ִ� ���ܼ���
                    if (tetrisDataboard_battle.tetrisBoard[i, j] == BOXTYPE.DEADLINE) continue;

                    tetrisDataboard_battle.tetrisBoard[i, j] = tetrisDataboard_battle.tetrisBoard[i - 1, j];
                }
            }
            // ���� �� �ø��� �������� ������ ����

            for (int j = 1; j < TetrisDataBoardForBattle.WIDTHMAX - 1; ++j)
            {
                tetrisDataboard_battle.tetrisBoard[1, j] = BOXTYPE.HARDBOX;
            }
            tetrisDataboard_battle.tetrisBoard[1, Random.Range(1, TetrisDataBoardForBattle.WIDTHMAX - 1)] = BOXTYPE.EMPTY;
        }

        //         // �ٽ� ������� ����
        //         for (int i = 1; i < TetrisDataBoardForBattle.WIDTHMAX - 1; ++i)
        //         {
        //             tetrisDataboard_battle.tetrisBoard[TetrisDataBoardForBattle.GameOVerLine, i] = BOXTYPE.DEADLINE;
        //         }

        _attackCount = 0;


    }

    public void FillEmptyBlock_Battle(int deletedLine)
    {
        for (int i = 0; i < TetrisDataBoardForBattle.HEIGHTMAX - deletedLine - 1; ++i)
        {
            for (int j = 1; j < TetrisDataBoardForBattle.WIDTHMAX - 1; ++j)
            {
                /// ���� ���ﶧ���� ��������� �����ͼ� �����ִ� ���ܼ���
                if (tetrisDataboard_battle.tetrisBoard[deletedLine + 1 + i, j] == BOXTYPE.DEADLINE) return;

                tetrisDataboard_battle.tetrisBoard[deletedLine + i, j] = tetrisDataboard_battle.tetrisBoard[deletedLine + 1 + i, j];
            }
        }
    }

    // ����Ʈ�ȿ� ��Ʈ�ι̳뼼Ʈ�� 2�����ϸ� �������ִ� �Լ�
    void FillTetrominoSet()
    {
        if (ListTetrominoSet.Count < 3)
            ListTetrominoSet.AddLast(tetrominoPocket.NRDDCRANDOM(0, 6));
    }

    public int onlyonce = 0;
    public void DeleteLine_Battle()
    {
        for (int i = 0; i < TetrisDataBoardForBattle.HEIGHTMAX; ++i)
        {
            int _hardBoxCount = 0;

            for (int j = 0; j < TetrisDataBoardForBattle.WIDTHMAX; ++j)
            {
                if (tetrisDataboard_battle.tetrisBoard[i, j] == BOXTYPE.HARDBOX) _hardBoxCount++; // ���ĭ�� �����ϸ鼭 �����࿡ ���� �ִ� ���� �����Ѵ�.

                if (_hardBoxCount == 10)    // ���� 10����� ���� ���� ��ä�����ٴ� ���̴�.
                {
                    for (int k = 1; k < TetrisDataBoardForBattle.WIDTHMAX - 1; ++k) // ���ο� 10�� ��������� �����ش�.
                    {
                        tetrisDataboard_battle.tetrisBoard[i, k] = BOXTYPE.EMPTY;
                    }
                    attackLineCount++;

                    onlyonce++;
                    //Debug.Log("���� ����ī��Ʈ = " + attackLineCount);

                    SoundManagerB.instance.lineClear.Play();

                    if (isSetBottom == true && isComboStart == false) // �ٴڿ� ��ҳ�? ù�������ΰ�
                    {
                        comboCount = 0;
                        isComboStart = true;
                        Debug.Log("isComboStart�� True�� + " + isComboStart);
                    }

                    else if (isSetBottom == true && isComboStart == true && onlyonce < 2) // �ٴڿ� ��ҳ�? �׸��� ���� �޺��� ���۵Ǿ���?
                    {
                        this.comboCount++;
                        AttackCombo();
                        Debug.Log("comboCount�� ������ + " + comboCount);
                    }

                    FillEmptyBlock_Battle(i); return;  // ���پ� ����� �Լ��� ������Ų��.
                }
            }
        }

        if (isSetBottom == true && onlyonce <= 0)
        {
            isComboStart = false; // ����� ���ٸ� �޺� �����ش�.
            comboCount = 0;
            Debug.Log("�޺��� ���大�� isComboStart�� false�� ��" + isComboStart);
        }

        if (attackLineCount < 2) attackLineCount = 0;

    }

    void AttackCombo()
    {
        switch (comboCount)
        {
            case 1:
                SoundManagerB.instance.ComboSoundPlay(11);
                break;
            case 2:
                attackLineCount += 1;
                SoundManagerB.instance.ComboSoundPlay(comboCount);
                break;
            case 3:
                attackLineCount += 1;
                SoundManagerB.instance.ComboSoundPlay(comboCount);
                break;
            case 4:
                attackLineCount += 2;
                SoundManagerB.instance.ComboSoundPlay(comboCount);
                break;
            case 5:
                attackLineCount += 2;
                SoundManagerB.instance.ComboSoundPlay(comboCount);
                break;
            case 6:
                attackLineCount += 3;
                SoundManagerB.instance.ComboSoundPlay(comboCount);
                break;
            case 7:
                attackLineCount += 3;
                SoundManagerB.instance.ComboSoundPlay(comboCount);
                break;
            case 8:
                attackLineCount += 4;
                SoundManagerB.instance.ComboSoundPlay(comboCount);
                break;
            case 9:
                attackLineCount += 4;
                SoundManagerB.instance.ComboSoundPlay(10);
                break;
            case 10:
                attackLineCount += 4;
                SoundManagerB.instance.ComboSoundPlay(10);
                break;
        }
        if (11 <= comboCount)
        {
            attackLineCount += 5;
            SoundManagerB.instance.ComboSoundPlay(10);
        }
    }

    public void SetBottom(ref Vector2 _movePos, ref Vector2 _startPos, ref int _enemyAttackCount)
    {
        for (int i = 0; i < 4; ++i)
        {
            for (int j = 0; j < 4; ++j)
            {
                int _X = (int)_startPos.x + j + (int)_movePos.x;
                int _Y = (int)_startPos.y + i + (int)_movePos.y;

                // �����ڵ�
                if (_Y > TetrisDataBoard.HEIGHTMAX || _Y < 0 || _X > TetrisDataBoard.WIDTHMAX || _X < 0) { /*Debug.Log("���� �Ѿ��.");*/ }

                if (TetrominosCheck_Battle(i, j) == true)
                {
                    tetrisDataboard_battle.tetrisBoard[_Y, _X] = BOXTYPE.HARDBOX;
                }
            }
        }

        isSetBottom = true;
        onlyonce = 0;

        Debug.Log("setBottom �Լ����� issetBottom�� True�� �ٲ� " + isSetBottom);

        Debug.LogError("ComboCount " + comboCount);

        NewTetromino(ref _startPos, ref _movePos);

        AttackLine(ref _enemyAttackCount);

        //Debug.Log("���� ī��Ʈ = " + attackLineCount);

        this.timer = 0;

        isUsedHolding = false;

        SoundManagerB.instance.dropSound.Play();
    }

    //�������� ��� ����
    public void NewTetromino(ref Vector2 _start, ref Vector2 _move)
    {
        InitPos_Battle(ref _start, ref _move);
        nowRoationIndex = 0;                                // ȸ�� �⺻������ ����

        nowTetrominoTypeIndex = ListTetrominoSet.First.Value.Dequeue();


        if (ListTetrominoSet.First.Value.Count >= 0)        // ����Ʈ ù��° ��Ʈ�ι̳뼼Ʈ�� �پ��� �����.
        {
            ListTetrominoSet.RemoveFirst();
            FillTetrominoSet();
        }

        if (ListTetrominoSet.First.Value.Count != 0)
        {
            preViewTetrominos = ListTetrominoSet.First.Value.Peek();
        }
        else
        {
            preViewTetrominos = ListTetrominoSet.First.Next.Value.Peek();
        }
        //nowTetrominoTypeIndex = QQtetrominoSet[0].Dequeue();
        //if (lltetrominoSet.First.Value.Count < 7) lltetrominoSet.RemoveFirst();
    }

    public void InitPos_Battle(ref Vector2 _startPos, ref Vector2 _movePos)
    {
        _startPos.x = TetrisDataBoard.STARTPOSX;
        _startPos.y = TetrisDataBoard.STARTPOSY;

        _movePos.x = 0;
        _movePos.y = 0;
    }

    // ��Ʈ�ι̳� ȸ��
    public void RotateRightTetromino_Battle()
    {
        nowRoationIndex++;
        if (3 < nowRoationIndex) nowRoationIndex = 0;
    }

    public void RotateLeftTetromino_Battle()
    {
        nowRoationIndex--;
        if (nowRoationIndex < 0) nowRoationIndex = 3;
    }

    public void RotationCheckAndRotate_Battle(ref Vector2 _movePos, Vector2 _startPos)
    {
        if (isRotationChanged == false)
        {
            RotateRightTetromino_Battle(); // �ϴ� ������.

            ItetrominoWallCheck(ref _movePos, _startPos);

            if (overTop_battle == true)   // �����ڵ�
            {
                _movePos.y -= 1;
                overTop_battle = false;
                return;
            }

            if (PreCheckForRotation_Battle(_movePos, _startPos) == false) RotateLeftTetromino_Battle();    // ������ ��������̳� �ٴ��̶�� �ݴ�� ������.

            if (PreWallCheck(_movePos, _startPos) == false)       // ���� ������ �ݴ������� �ű��.
            {
                if (wallOut == 1) _movePos.x -= 1;    // �����ʿ� ������
                else if (wallOut == -1) _movePos.x += 1; // ���ʿ� ������
            }
        }
        else
        {
            // �ϴ� ������.
            RotateLeftTetromino_Battle();
            ItetrominoWallCheck(ref _movePos, _startPos);

            if (overTop_battle == true)   // �����ڵ�
            {
                _movePos.y -= 1;
                overTop_battle = false;
                return;
            }

            if (PreCheckForRotation_Battle(_movePos, _startPos) == false) RotateRightTetromino_Battle();    // ������ ��������̳� �ٴ��̶�� �ݴ�� ������.

            if (PreWallCheck(_movePos, _startPos) == false)       // ���� ������ �ݴ������� �ű��.
            {
                if (wallOut == 1) _movePos.x -= 1;    // �����ʿ� ������
                else if (wallOut == -1) _movePos.x += 1; // ���ʿ� ������
            }
        }

    }

    bool PreCheckForRotation_Battle(Vector2 _movePos, Vector2 _startPos)
    {
        for (int i = 0; i < 4; ++i)
        {
            for (int j = 0; j < 4; ++j)
            {
                if (TetrominosCheck_Battle(i, j) == true)
                {
                    int _nowXinArr = (int)_startPos.x + j + (int)_movePos.x;
                    int _nowYinArr = (int)_startPos.y + i + (int)_movePos.y;

                    //Debug.Log("���� x�� " + _nowXinArr + " ���� y�� " + _nowYinArr);

                    if (_nowXinArr < 0 || TetrisDataBoard.WIDTHMAX - 1 < _nowXinArr) return false; // �迭�� �ڲ� ����⶧���� ������ ���Ͻ�Ű�� �κ�

                    if (_nowYinArr <= 0) return false;

                    if (renderBoard[_nowYinArr, _nowXinArr] == BOXTYPE.WALL &&/*������ Y�� �߰��ڵ�*/ _nowYinArr == 0) return false; // ���Ҵµ� ���°��� �ٴ��̰ų� ���� ����� ������ ���������Ұ��̴�.

                    if (renderBoard[_nowYinArr, _nowXinArr] == BOXTYPE.HARDBOX) return false;
                }
            }
        }
        return true;
    }

    public void ItetrominoWallCheck(ref Vector2 _movePos, Vector2 _startPos)
    {
        if (nowTetrominoTypeIndex == 5)
        {
            //���� �ճ� �ȶճ� �˻��Ѵ�.
            if (PreWallCheck(_movePos, _startPos) == false && wallOut == -1) //������ ������ 
            {
                _movePos.x += 1;

                if (PreWallCheck(_movePos, _startPos) == true) return;// �� �˻��Ѵ�. 

                else if (PreWallCheck(_movePos, _startPos) == false)
                {
                    _movePos.x += 1;

                    if (PreWallCheck(_movePos, _startPos) == true) return;// �� �˻��Ѵ�. 

                    else if (PreWallCheck(_movePos, _startPos) == false)
                    {
                        _movePos.x += 1;
                    }
                }
            }
            else if (PreWallCheck(_movePos, _startPos) == false && wallOut == 1) // ������ ������
            {
                _movePos.x -= 1;

                if (PreWallCheck(_movePos, _startPos) == true) return;// �� �˻��Ѵ�. 

                else if (PreWallCheck(_movePos, _startPos) == false)
                {
                    _movePos.x -= 1;

                    if (PreWallCheck(_movePos, _startPos) == true) return;// �� �˻��Ѵ�. 

                    else if (PreWallCheck(_movePos, _startPos) == false)
                    {
                        _movePos.x -= 1;
                    }
                }
            }
        }
    }
    public bool PreWallCheck(Vector2 _MovePos, Vector2 _startPos)
    {
        for (int i = 0; i < 4; ++i)
        {
            for (int j = 0; j < 4; ++j)
            {
                if (TetrominosCheck_Battle(i, j) == true)
                {
                    int _nowXinArr = (int)_startPos.x + j + (int)_MovePos.x;

                    int _nowYinArr = (int)_startPos.y + i + (int)_MovePos.y; // �Ⱦ�?

                    TopOverCheck_Battle(_nowYinArr);

                    //if (_nowXinArr < 0 || TetrisDataBoard.WIDTHMAX - 1 < _nowXinArr) return false; // �迭�� �ڲ� ����⶧���� ������ ���Ͻ�Ű�� �κ�

                    if (TetrisDataBoard.WIDTHMAX - 1 <= _nowXinArr) // �캮�� �����
                    {
                        wallOut = 1;
                        return false;
                    }

                    if (_nowXinArr <= 0) // �º��� �����
                    {
                        wallOut = -1;
                        return false;
                    }
                }
            }
        }
        return true;
    }

    bool TopOverCheck_Battle(int _nowYinArr)
    {
        if (TetrisDataBoard.HEIGHTMAX - 1 < _nowYinArr)      // õ�� �մ´ٸ�
        {
            overTop_battle = true;
            return overTop_battle;
        }
        else overTop_battle = false;
        return overTop_battle;
    }

    public void Holding()
    {
        Debug.Log(isUsedHolding);
        // ùȦ����
        if (holdingTetromino == FIRSTHOLDING)
        {
            holdingTetromino = nowTetrominoTypeIndex;
            nowTetrominoTypeIndex = ListTetrominoSet.First.Value.Dequeue();
        }
        else
        {
            int temp = holdingTetromino;

            holdingTetromino = nowTetrominoTypeIndex;

            nowTetrominoTypeIndex = temp;

            isUsedHolding = true;
        }
    }

    public void Timer_battle(ref Vector2 _movePos, ref Vector2 _startPos, ref int _enemyAttackCount)
    {
        this.timer += Time.deltaTime;

        if (this.timer > waitingTime)
        {
            this.timer = 0;
            AutoDown_battle(ref this.timer, ref _movePos, ref _startPos, ref _enemyAttackCount);
        }
    }

    public void AutoDown_battle(ref float _timer, ref Vector2 _movePos, ref Vector2 _startPos, ref int _enemyAttackCount)
    {
        _movePos.y -= 1;
        if (PreCheckMoveForBattle(_movePos, _startPos) == false)
        {
            _movePos.y += 1;
            SetBottom(ref _movePos, ref _startPos, ref _enemyAttackCount);
        }
    }

    /// //////////////////////////////////////////////////
}
