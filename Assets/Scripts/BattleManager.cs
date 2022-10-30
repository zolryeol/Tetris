using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ��Ʋ������ �������� ������ ������ ����ϴ� �Ŵ���Ŭ��
/// </summary>

public class BattleManager : MonoBehaviour
{
    Player playerLeft;
    Player playerRight;

    [SerializeField]
    GameObject boxPrefab;
    [SerializeField]
    Renderer BoxMaterial;

    GameObject[,,] Boxes;
    GameObject[,,] HoldingBox;
    GameObject[,,] NextBox;


    Vector2 leftPlayerMove;
    Vector2 rightPlayerMove;

    Vector2 startPos;

    int winner;
    [Header("UI")]
    [SerializeField] //��Ʈ����Ʈ ��� �Ѵ�. C#����̴�.
    GameObject LeftPannel;
    [SerializeField]
    GameObject RightPannel;

    //     [SerializeField]
    //     [Header("Audios")]
    //     AudioSource bgm;
    //     [SerializeField]
    //     AudioSource dropSound;
    //     [SerializeField]
    //     AudioSource lineClear;


    private void Awake()
    {
        LeftPannel.SetActive(false);
        RightPannel.SetActive(false);

        winner = WHOWIN.NOONEWIN;

        playerLeft = new Player();
        playerRight = new Player();

        leftPlayerMove.x = 0;
        leftPlayerMove.y = 0;

        rightPlayerMove.x = 0;
        rightPlayerMove.y = 0;

        startPos.x = 4;
        startPos.y = 21;

        Boxes = new GameObject[2, TetrisDataBoard.HEIGHTMAX, TetrisDataBoard.WIDTHMAX];
        HoldingBox = new GameObject[7, 4, 4];
        NextBox = new GameObject[7, 4, 4];

        playerLeft.InitPlayerBoard_Battle();

        playerRight.InitPlayerBoard_Battle();

        CreateBoxes_Battle();

        //         /// ��������ڸ� ���ٰ� ��ôµ� �̰� �ƴѵ��ϴ�. ť�� ��ī���Ϸ��� ��� �ؾ��ϴ°�?
        // 
        //         tetrominoPocket = new NoRedundancyRandom();
        //         tetrominoPocket.FillPocket();       // ��ģ���� Qset�� ������;
        // 
        //         playerLeft.newQueue();  // �޸��Ҵ� new
        //         playerRight.newQueue();
        // 
        //         playerLeft.CopyTetrominoPocket(tetrominoPocket.QSet);
        //         playerRight.CopyTetrominoPocket(tetrominoPocket.QSet);
        // 
        //         tetrominoPocket.QSet.Dequeue();
    }

    public void CreateBoxes_Battle()
    {
        // ���ѹ��� ������ �������� �޾� �ڽ�������Ʈ�� �����Ѵ�.
        Transform playerLeftAndRight = GameObject.Find("PlayerLeft").transform;
        Transform holdingbox = GameObject.Find("HoldingBox").transform;


        float _interval = 1.2f; // ������ �Ÿ�
        float _boardDistance = 0;        // �÷��̾� ���峢���� �Ÿ�

        float _verticalNext = 24;
        float _verticalHold = 12;

        for (int k = 0; k < 2; ++k)
        {
            for (int i = 0; i < TetrisDataBoard.HEIGHTMAX; ++i)
            {
                for (int j = 0; j < TetrisDataBoard.WIDTHMAX; ++j)
                {
                    Boxes[k, i, j] = Instantiate<GameObject>(boxPrefab, new Vector3(_boardDistance + j * _interval, i * _interval, 0), Quaternion.identity, playerLeftAndRight);

                    if (i < 4 && j < 4)
                    {
                        HoldingBox[k, i, j] = Instantiate<GameObject>(boxPrefab, new Vector3((k * 8) + 16 + j * _interval, _verticalHold + i * _interval, 0), Quaternion.identity, holdingbox);
                        NextBox[k, i, j] = Instantiate<GameObject>(boxPrefab, new Vector3((k * 8) + 16 + j * _interval, +_verticalNext + i * _interval, 0), Quaternion.identity, holdingbox);
                    }
                }
            }
            playerLeftAndRight = GameObject.Find("PlayerRight").transform;
            _boardDistance = 30;
        }

    }

    void ChangeColor_Battle() // ���ٲٴ� �κ�
    {
        for (int i = 0; i < TetrisDataBoard.HEIGHTMAX; ++i)
        {
            for (int j = 0; j < TetrisDataBoard.WIDTHMAX; ++j)
            {
                // �����÷��̾� ������
                ChangeColorForLeft(playerLeft.renderBoard[i, j], i, j);
                // �����÷��̾� ������
                ChangeColorForRight(playerRight.renderBoard[i, j], i, j);

                // Ȧ�� ������
                if (i < 4 && j < 4)
                {
                    ChangeColorForHolding_Left(playerLeft.holdingRenderBoard[i, j], i, j);
                    ChangeColorForHolding_Right(playerRight.holdingRenderBoard[i, j], i, j);

                    ChangeColorForNext_Left(playerLeft.preViewTetrominos, i, j);
                    ChangeColorForNext_Right(playerRight.preViewTetrominos, i, j);
                }


            }
        }
    }

    /// Ȧ���
    void ChangeColorForHolding_Left(int boxTypeNum, int _i, int _j)
    {
        if (boxTypeNum == BOXTYPE.EMPTY) boxTypeNum = 12;

        HoldingBox[0, _i, _j].GetComponent<Renderer>().material = BoxMaterial.materials[boxTypeNum];
    }

    void ChangeColorForHolding_Right(int boxTypeNum, int _i, int _j)
    {
        if (boxTypeNum == BOXTYPE.EMPTY) boxTypeNum = 12;

        HoldingBox[1, _i, _j].GetComponent<Renderer>().material = BoxMaterial.materials[boxTypeNum];
    }

    /// ��������Ʈ�ι̳��

    void ChangeColorForNext_Right(int boxTypeNum, int _i, int _j)
    {
        if (playerRight.tetromino_battle.tetroimino[playerRight.preViewTetrominos, 0, _i, _j] != BOXTYPE.EMPTY)
        {
            NextBox[1, _i, _j].GetComponent<Renderer>().material = BoxMaterial.materials[boxTypeNum + 2];
        }
        else
        {
            boxTypeNum = 12;
            NextBox[1, _i, _j].GetComponent<Renderer>().material = BoxMaterial.materials[boxTypeNum];
        }
    }

    void ChangeColorForNext_Left(int boxTypeNum, int _i, int _j)
    {
        if (playerLeft.tetromino_battle.tetroimino[playerLeft.preViewTetrominos, 0, _i, _j] != BOXTYPE.EMPTY)
        {
            NextBox[0, _i, _j].GetComponent<Renderer>().material = BoxMaterial.materials[boxTypeNum + 2];
        }
        else
        {
            boxTypeNum = 12;
            NextBox[0, _i, _j].GetComponent<Renderer>().material = BoxMaterial.materials[boxTypeNum];
        }
    }


    void ChangeColorForLeft(int boxTypeNum, int _i, int _j)
    {
        if (boxTypeNum == BOXTYPE.DEADLINE) boxTypeNum = 10;

        if (boxTypeNum == BOXTYPE.HARDBOX) boxTypeNum = 9;

        if (boxTypeNum == 12 || boxTypeNum == 13 || boxTypeNum == 14 || boxTypeNum == 15 || boxTypeNum == 16 || boxTypeNum == 17 || boxTypeNum == 18) boxTypeNum = 11;

        /// box �� �迭 ù��° 0�� ������ ����
        Boxes[0, _i, _j].GetComponent<Renderer>().material = BoxMaterial.materials[boxTypeNum];
    }
    void ChangeColorForRight(int boxTypeNum, int _i, int _j)
    {
        /// box �迭 ù��° 1�� �������� ����
        if (boxTypeNum == BOXTYPE.DEADLINE) boxTypeNum = 10;

        if (boxTypeNum == BOXTYPE.HARDBOX) boxTypeNum = 9;

        if (boxTypeNum == 12 || boxTypeNum == 13 || boxTypeNum == 14 || boxTypeNum == 15 || boxTypeNum == 16 || boxTypeNum == 17 || boxTypeNum == 18) boxTypeNum = 11;

        Boxes[1, _i, _j].GetComponent<Renderer>().material = BoxMaterial.materials[boxTypeNum];
    }

    // Ű��ǲ
    public void KeyInput()
    {
        /// Player 1 ����
        if (Input.GetKeyDown(KeyCode.A))
        {
            leftPlayerMove.x -= 1;
            if (playerLeft.PreCheckMoveForBattle(leftPlayerMove, startPos) == false) leftPlayerMove.x += 1;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            leftPlayerMove.x += 1;
            if (playerLeft.PreCheckMoveForBattle(leftPlayerMove, startPos) == false) leftPlayerMove.x -= 1;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            playerLeft.RotationCheckAndRotate_Battle(ref leftPlayerMove, startPos);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            leftPlayerMove.y -= 1;
            if (playerLeft.PreCheckMoveForBattle(leftPlayerMove, startPos) == false)
            {
                leftPlayerMove.y += 1;
                playerLeft.SetBottom(ref leftPlayerMove, ref startPos, ref playerRight.attackLineCount);
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            while (playerLeft.PreCheckMoveForBattle(leftPlayerMove, startPos) == true)
            {
                leftPlayerMove.y -= 1;
            }
            leftPlayerMove.y += 1;

            playerLeft.SetBottom(ref leftPlayerMove, ref startPos, ref playerRight.attackLineCount);

            Debug.Log(playerLeft.ListTetrominoSet.First.Value.Peek());
            Debug.Log(playerRight.ListTetrominoSet.First.Value.Peek());

        } //����

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (playerLeft.isUsedHolding == false)
            {
                playerLeft.Holding();
                playerLeft.InitPos_Battle(ref startPos, ref leftPlayerMove);
            }
            else { }
        } // Ȧ��

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            playerLeft.isRotationChanged = !playerLeft.isRotationChanged;
        } // �ݴ�ȸ��

        /// Player 2 ����

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            rightPlayerMove.x -= 1;
            if (playerRight.PreCheckMoveForBattle(rightPlayerMove, startPos) == false) rightPlayerMove.x += 1;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            rightPlayerMove.x += 1;
            if (playerRight.PreCheckMoveForBattle(rightPlayerMove, startPos) == false) rightPlayerMove.x -= 1;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            //playerRight.IsAbleToRotate(ref rightPlayerMove, startPos);
            playerRight.RotationCheckAndRotate_Battle(ref rightPlayerMove, startPos);

        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            rightPlayerMove.y -= 1;
            if (playerRight.PreCheckMoveForBattle(rightPlayerMove, startPos) == false)
            {
                rightPlayerMove.y += 1;
                playerRight.SetBottom(ref rightPlayerMove, ref startPos, ref playerLeft.attackLineCount);
            }
        }

        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            while (playerRight.PreCheckMoveForBattle(rightPlayerMove, startPos) == true)
            {
                rightPlayerMove.y -= 1;
            }
            rightPlayerMove.y += 1;

            playerRight.SetBottom(ref rightPlayerMove, ref startPos, ref playerLeft.attackLineCount);
        } // ����

        if (Input.GetKeyDown(KeyCode.Slash))
        {
            if (playerRight.isUsedHolding == false)
            {
                playerRight.Holding();
                playerRight.InitPos_Battle(ref startPos, ref rightPlayerMove);
            }
            else { }
        } // Ȧ��

        if (Input.GetKeyDown(KeyCode.Comma))
        {
            playerRight.isRotationChanged = !playerRight.isRotationChanged;
        } // �ݴ�ȸ��

        /// ��Ÿ ����

        if (Input.GetKeyDown(KeyCode.Escape)) { SceneMG.instance.GoTitleScene(); }
    }

    int GameOverCheck_Battle(Player playerLeft, Player playerRight)
    {
        //Debug.Log(playerLeft.tetrisDataboard_battle.tetrisBoard[TetrisDataBoardForBattle.GameOVerLine, 6]);
        //Debug.Log(playerRight.tetrisDataboard_battle.tetrisBoard[TetrisDataBoardForBattle.GameOVerLine, 6]);

        for (int x = 1; x < TetrisDataBoard.WIDTHMAX - 1; ++x)
        {
            if (playerLeft.tetrisDataboard_battle.tetrisBoard[TetrisDataBoardForBattle.GameOVerLine, x] == BOXTYPE.HARDBOX)
            {
                winner = WHOWIN.RIGHTWIN;
                RightPannel.SetActive(true);
                SoundManagerB.instance.bgm.Stop();
                SoundManagerB.instance.dropSound.volume = 0;

                return winner;
            }
            else if (playerRight.tetrisDataboard_battle.tetrisBoard[TetrisDataBoardForBattle.GameOVerLine, x] == BOXTYPE.HARDBOX)
            {
                winner = WHOWIN.LEFTWIN;
                LeftPannel.SetActive(true);
                SoundManagerB.instance.bgm.Stop();
                SoundManagerB.instance.dropSound.volume = 0;
                return winner;
            }
        }
        return winner;
    }

    private void Start()
    {
        SoundManagerB.instance.bgm.Play();
    }
    void Update()
    {
        GameOverCheck_Battle(playerLeft, playerRight);

        playerLeft.Timer_battle(ref leftPlayerMove, ref startPos, ref playerRight.attackLineCount);
        playerRight.Timer_battle(ref rightPlayerMove, ref startPos, ref playerLeft.attackLineCount);

        playerLeft.DataBoardToRenderBoard_Battle();
        playerRight.DataBoardToRenderBoard_Battle();

        KeyInput();

        playerLeft.DeleteLine_Battle();
        playerRight.DeleteLine_Battle();

        playerLeft.TetrominoToRenderBoard_Battle(leftPlayerMove, startPos);
        playerRight.TetrominoToRenderBoard_Battle(rightPlayerMove, startPos);

        ChangeColor_Battle();

        playerLeft.isSetBottom = false;
        playerRight.isSetBottom = false;

        playerRight.onlyonce = 0;
        playerLeft.onlyonce = 0;


    }
}