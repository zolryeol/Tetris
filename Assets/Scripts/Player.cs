using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 전에 만들어 둔 아이들을 상속받았으면 좋았지만 MonoBehaviour 기능을 너무 많이 썼다 이번엔 그냥 클래스로 다시 짜보자
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

    public int[,,,] tetroimino = new int[7, 4, 4, 4]// 테트로미노는 7가지이다. 
         {
            {
                {
                // S 테트로미노
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

                // Z 테트로미노
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

             // T 테트로미노
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

            // L 테트로미노
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
            // J 테트로미노
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
            // I 테트로미노
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
            // O테트로미노
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

    // 벽, 빈공간, 데드라인을 설정해주며 초기화한다.
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
/// 플레이어 클래스다. 
/// 라고 말해두고 사실상 랜더보드와 똑같다.
/// 
/// 배틀모드에서 이클래스를 플레이어1, 플레이어2 가 각각 가지고 있을예정이다.
/// 위에있는 테트로미노 및 데이터보드 클래스를 가지고 있을 예정
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

    ///콤보 관련
    bool isComboStart = false;
    public bool isSetBottom = false;
    private int comboCount = 0;

    public void PrintGhost(Vector2 _startPos, Vector2 movePos)
    {
        int ghostY = 0;

        while (PreCheckMoveGhost(ghostY, _startPos, movePos) == true) // 고스트용 바닥체크
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
        // 인풋을 받고나서 바로 실행, true면 진행 false면 리턴    
        for (int i = 0; i < 4; ++i)
        {
            for (int j = 0; j < 4; ++j)
            {
                /// 4x4 테트로미노배열안에서 빈공간이 아닌 것을 찾으면 true 
                if (TetrominosCheck_Battle(i, j) == true)
                {
                    int _nowXinArr = (int)_startPos.x + j + (int)_movePos.x;
                    int _nowYinArr = (int)_startPos.y + i + (int)_movePos.y;

                    //Debug.Log("현재 x값 " + _nowXinArr + " 현재 y값 " + _nowYinArr);

                    if (_nowXinArr < 0) return false; // 배열을 자꾸 벗어나기때문에 억지로 리턴시키는 부분
                    if (_nowYinArr > 23) return false; // 배열을 자꾸 벗어나기때문에 억지로 리턴시키는 부분


                    if ((renderBoard[_nowYinArr + ghostOffset, _nowXinArr] == BOXTYPE.WALL))
                        return false;

                    if ((renderBoard[_nowYinArr + ghostOffset, _nowXinArr] == BOXTYPE.HARDBOX))
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

    public void InitPlayerBoard_Battle()
    {
        nowRoationIndex = 0;
        attackLineCount = 0;

        holdingTetromino = FIRSTHOLDING;

        tetrisDataboard_battle = new TetrisDataBoardForBattle();

        tetromino_battle = new TetrominoForBattle();

        renderBoard = new int[TetrisDataBoardForBattle.HEIGHTMAX, TetrisDataBoardForBattle.WIDTHMAX];

        holdingRenderBoard = new int[4, 4];

        /// 테트로미노 생성해서 쌓아두는 부분
        tetrominoPocket = new NoRedundancyRandom(); // 생성자에서 큐 new 해준다.

        ListTetrominoSet = new LinkedList<Queue<int>>();

        ListTetrominoSet.AddFirst(tetrominoPocket.NRDDCRANDOM(0, 6));    /// 중복없이 7개 생성해서 첫번째로 넣는다.

        nowTetrominoTypeIndex = 0;
        nowTetrominoTypeIndex = ListTetrominoSet.First.Value.Dequeue();
        preViewTetrominos = ListTetrominoSet.First.Value.Peek();
    }

    public void TetrominoToRenderBoard_Battle(Vector2 _movePos, Vector2 _startPos)
    {
        PrintGhost(_startPos, _movePos);

        // 랜더보드판에 테트로미노 집어넣는 함수
        /// 
        for (int i = 0; i < TetrisDataBoardForBattle.HEIGHTMAX; ++i)
        {
            for (int j = 0; j < TetrisDataBoardForBattle.WIDTHMAX; ++j)
            {
                int _nowPosX = j + (int)_startPos.x + (int)_movePos.x;
                int _nowPosY = i + (int)_startPos.y + (int)_movePos.y;

                if (TetrominosCheck_Battle(i, j) == true) // 테트로미노의 빈칸 무시하고 집어넣는 함수
                {
                    renderBoard[_nowPosY, _nowPosX] = tetromino_battle.tetroimino[nowTetrominoTypeIndex, nowRoationIndex, i, j];
                }
                if (i < 4 && j < 4) // 테트로미노의 빈칸 무시하고 집어넣는 함수
                {
                    if (holdingTetromino == FIRSTHOLDING) continue;
                    holdingRenderBoard[i, j] = tetromino_battle.tetroimino[holdingTetromino, 0, i, j];
                }
            }
        }
    }

    public void DataBoardToRenderBoard_Battle()
    {
        // 랜더보드판에 데이터보드 집어넣는 함수
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
        //테트로미노 4X4 이기때문에 4번씩만 돈다
        // 빈칸(Empty)은 신경쓰지 않을 것이다
        if (_i < 4 && _j < 4 &&
           tetromino_battle.tetroimino[nowTetrominoTypeIndex, nowRoationIndex, _i, _j] != BOXTYPE.EMPTY) return true;
        else return false;
    }

    public bool PreCheckMoveForBattle(Vector2 _movePos, Vector2 _startPos)
    {
        // 인풋을 받고나서 바로 실행, true면 진행 false면 리턴    
        for (int i = 0; i < 4; ++i)
        {
            for (int j = 0; j < 4; ++j)
            {
                /// 4x4 테트로미노배열안에서 빈공간이 아닌 것을 찾으면 true 
                if (TetrominosCheck_Battle(i, j) == true)
                {
                    int _nowXinArr = (int)_startPos.x + j + (int)_movePos.x;
                    int _nowYinArr = (int)_startPos.y + i + (int)_movePos.y;

                    //Debug.Log("현재 x값 " + _nowXinArr + " 현재 y값 " + _nowYinArr);

                    if (_nowYinArr < 0 || TetrisDataBoardForBattle.HEIGHTMAX - 1 < _nowYinArr) return false; // 배열을 자꾸 벗어나기때문에 억지로 리턴시키는 부분
                    if (_nowXinArr > TetrisDataBoardForBattle.WIDTHMAX - 1) return false; // 배열을 자꾸 벗어나기때문에 억지로 리턴시키는 부분

                    if ((renderBoard[_nowYinArr, _nowXinArr] == BOXTYPE.WALL))
                        return false;

                    if ((renderBoard[_nowYinArr, _nowXinArr] == BOXTYPE.HARDBOX))
                        return false;
                }
            }
        }
        return true;
    }

    public void AttackLine(ref int _attackCount)    /// 상대방의 공격카운트를 가져와서 나의 블록을 높인다.
    {
        for (int k = 1; k < _attackCount; ++k) // 공격 라인만큼  반복한다.
        {
            for (int i = TetrisDataBoardForBattle.GameOVerLine; 1 < i; --i)
            {
                for (int j = 1; j < TetrisDataBoardForBattle.WIDTHMAX - 1; ++j)
                {
                    /// 라인 지울때마다 데드라인이 내려와서 막아주는 예외설정
                    if (tetrisDataboard_battle.tetrisBoard[i, j] == BOXTYPE.DEADLINE) continue;

                    tetrisDataboard_battle.tetrisBoard[i, j] = tetrisDataboard_battle.tetrisBoard[i - 1, j];
                }
            }
            // 위로 다 올리고 마지막에 공격줄 삽입

            for (int j = 1; j < TetrisDataBoardForBattle.WIDTHMAX - 1; ++j)
            {
                tetrisDataboard_battle.tetrisBoard[1, j] = BOXTYPE.HARDBOX;
            }
            tetrisDataboard_battle.tetrisBoard[1, Random.Range(1, TetrisDataBoardForBattle.WIDTHMAX - 1)] = BOXTYPE.EMPTY;
        }

        //         // 다시 데드라인 설정
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
                /// 라인 지울때마다 데드라인이 내려와서 막아주는 예외설정
                if (tetrisDataboard_battle.tetrisBoard[deletedLine + 1 + i, j] == BOXTYPE.DEADLINE) return;

                tetrisDataboard_battle.tetrisBoard[deletedLine + i, j] = tetrisDataboard_battle.tetrisBoard[deletedLine + 1 + i, j];
            }
        }
    }

    // 리스트안에 테트로미노세트가 2개이하면 생성해주는 함수
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
                if (tetrisDataboard_battle.tetrisBoard[i, j] == BOXTYPE.HARDBOX) _hardBoxCount++; // 모든칸을 조사하면서 가로축에 굳어 있는 블럭을 조사한다.

                if (_hardBoxCount == 10)    // 블럭이 10개라는 말은 한줄 다채워졌다는 뜻이다.
                {
                    for (int k = 1; k < TetrisDataBoardForBattle.WIDTHMAX - 1; ++k) // 가로열 10개 빈공간으로 지워준다.
                    {
                        tetrisDataboard_battle.tetrisBoard[i, k] = BOXTYPE.EMPTY;
                    }
                    attackLineCount++;

                    onlyonce++;
                    //Debug.Log("현재 어택카운트 = " + attackLineCount);

                    SoundManagerB.instance.lineClear.Play();

                    if (isSetBottom == true && isComboStart == false) // 바닥에 닿았나? 첫줄지움인가
                    {
                        comboCount = 0;
                        isComboStart = true;
                        Debug.Log("isComboStart가 True됨 + " + isComboStart);
                    }

                    else if (isSetBottom == true && isComboStart == true && onlyonce < 2) // 바닥에 닿았나? 그림고 전에 콤보가 시작되었나?
                    {
                        this.comboCount++;
                        AttackCombo();
                        Debug.Log("comboCount가 증가됨 + " + comboCount);
                    }

                    FillEmptyBlock_Battle(i); return;  // 한줄씩 떙기고 함수를 종류시킨다.
                }
            }
        }

        if (isSetBottom == true && onlyonce <= 0)
        {
            isComboStart = false; // 지운게 없다면 콤보 끊어준다.
            comboCount = 0;
            Debug.Log("콤보가 끊겼ㄷㅏ isComboStart가 false로 됨" + isComboStart);
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

                // 안전코드
                if (_Y > TetrisDataBoard.HEIGHTMAX || _Y < 0 || _X > TetrisDataBoard.WIDTHMAX || _X < 0) { /*Debug.Log("뭔가 넘어갔다.");*/ }

                if (TetrominosCheck_Battle(i, j) == true)
                {
                    tetrisDataboard_battle.tetrisBoard[_Y, _X] = BOXTYPE.HARDBOX;
                }
            }
        }

        isSetBottom = true;
        onlyonce = 0;

        Debug.Log("setBottom 함수에서 issetBottom이 True로 바뀜 " + isSetBottom);

        Debug.LogError("ComboCount " + comboCount);

        NewTetromino(ref _startPos, ref _movePos);

        AttackLine(ref _enemyAttackCount);

        //Debug.Log("공격 카운트 = " + attackLineCount);

        this.timer = 0;

        isUsedHolding = false;

        SoundManagerB.instance.dropSound.Play();
    }

    //여러가지 묶어서 쓰자
    public void NewTetromino(ref Vector2 _start, ref Vector2 _move)
    {
        InitPos_Battle(ref _start, ref _move);
        nowRoationIndex = 0;                                // 회전 기본값으로 설정

        nowTetrominoTypeIndex = ListTetrominoSet.First.Value.Dequeue();


        if (ListTetrominoSet.First.Value.Count >= 0)        // 리스트 첫번째 테트로미노세트를 다쓰면 지운다.
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

    // 테트로미노 회전
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
            RotateRightTetromino_Battle(); // 일단 돌린다.

            ItetrominoWallCheck(ref _movePos, _startPos);

            if (overTop_battle == true)   // 안전코드
            {
                _movePos.y -= 1;
                overTop_battle = false;
                return;
            }

            if (PreCheckForRotation_Battle(_movePos, _startPos) == false) RotateLeftTetromino_Battle();    // 돌려서 굳은블록이나 바닥이라면 반대로 돌린다.

            if (PreWallCheck(_movePos, _startPos) == false)       // 벽이 있으면 반대편으로 옮긴다.
            {
                if (wallOut == 1) _movePos.x -= 1;    // 오른쪽에 박으면
                else if (wallOut == -1) _movePos.x += 1; // 왼쪽에 박으면
            }
        }
        else
        {
            // 일단 돌린다.
            RotateLeftTetromino_Battle();
            ItetrominoWallCheck(ref _movePos, _startPos);

            if (overTop_battle == true)   // 안전코드
            {
                _movePos.y -= 1;
                overTop_battle = false;
                return;
            }

            if (PreCheckForRotation_Battle(_movePos, _startPos) == false) RotateRightTetromino_Battle();    // 돌려서 굳은블록이나 바닥이라면 반대로 돌린다.

            if (PreWallCheck(_movePos, _startPos) == false)       // 벽이 있으면 반대편으로 옮긴다.
            {
                if (wallOut == 1) _movePos.x -= 1;    // 오른쪽에 박으면
                else if (wallOut == -1) _movePos.x += 1; // 왼쪽에 박으면
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

                    //Debug.Log("현재 x값 " + _nowXinArr + " 현재 y값 " + _nowYinArr);

                    if (_nowXinArr < 0 || TetrisDataBoard.WIDTHMAX - 1 < _nowXinArr) return false; // 배열을 자꾸 벗어나기때문에 억지로 리턴시키는 부분

                    if (_nowYinArr <= 0) return false;

                    if (renderBoard[_nowYinArr, _nowXinArr] == BOXTYPE.WALL &&/*우측의 Y는 추가코드*/ _nowYinArr == 0) return false; // 돌았는데 도는곳이 바닥이거나 굳은 블록이 있으면 못돌리게할것이다.

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
            //벽을 뚫나 안뚫나 검사한다.
            if (PreWallCheck(_movePos, _startPos) == false && wallOut == -1) //왼쪽을 뚫을때 
            {
                _movePos.x += 1;

                if (PreWallCheck(_movePos, _startPos) == true) return;// 또 검사한다. 

                else if (PreWallCheck(_movePos, _startPos) == false)
                {
                    _movePos.x += 1;

                    if (PreWallCheck(_movePos, _startPos) == true) return;// 또 검사한다. 

                    else if (PreWallCheck(_movePos, _startPos) == false)
                    {
                        _movePos.x += 1;
                    }
                }
            }
            else if (PreWallCheck(_movePos, _startPos) == false && wallOut == 1) // 우측을 뚫을때
            {
                _movePos.x -= 1;

                if (PreWallCheck(_movePos, _startPos) == true) return;// 또 검사한다. 

                else if (PreWallCheck(_movePos, _startPos) == false)
                {
                    _movePos.x -= 1;

                    if (PreWallCheck(_movePos, _startPos) == true) return;// 또 검사한다. 

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

                    int _nowYinArr = (int)_startPos.y + i + (int)_MovePos.y; // 안씀?

                    TopOverCheck_Battle(_nowYinArr);

                    //if (_nowXinArr < 0 || TetrisDataBoard.WIDTHMAX - 1 < _nowXinArr) return false; // 배열을 자꾸 벗어나기때문에 억지로 리턴시키는 부분

                    if (TetrisDataBoard.WIDTHMAX - 1 <= _nowXinArr) // 우벽을 벗어나면
                    {
                        wallOut = 1;
                        return false;
                    }

                    if (_nowXinArr <= 0) // 좌벽을 벗어나면
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
        if (TetrisDataBoard.HEIGHTMAX - 1 < _nowYinArr)      // 천장 뚫는다면
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
        // 첫홀드라면
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
