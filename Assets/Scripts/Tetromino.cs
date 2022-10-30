using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 테트로미노의 정보를 담고있는 클래스
/// </summary>
public class Tetromino : MonoBehaviour
{
    public Renderer tetrominoMaterials;

    public int nowTetromino;
    public int holdingTetromino;        // 홀드한 친구를 저장하는 변수

    public bool isCreated;

    public TetrisDataBoard gameBoard;
    private GameObject temp;

    public bool isCollided;

    GameObject previewBlocksParent;
    GameObject holdBlocksParent;

    public int rotationIndex; // 시계회전하면 ++ 최대 3 까지; 반시계는 --;

    public int[,,,] tetroimino;
    public int[,,,] saveTetrominoArr;

    public LinkedList<int> llTetrominos;

    public const int NONE = 44; // 홀드 했는가 안했는가 확인용

    GameObject[] tetrominoType = new GameObject[7];

    string[] nameTetromino = { "Stetromino", "Ztetromino", "Ttetromino", "Ltetromino", "Jtetromino", "Itetromino", "Otetromino" };

    private void Awake()
    {
        rotationIndex = 0;

        previewBlocksParent = GameObject.Find("Trash").transform.gameObject;
        holdBlocksParent = GameObject.Find("Hold").transform.gameObject;

        holdingTetromino = NONE;
        llTetrominos = new LinkedList<int>();

        gameBoard = GameObject.Find("GameBoard").GetComponent<TetrisDataBoard>();

        tetroimino = new int[7, 4, 4, 4]// 테트로미노는 7가지이다. 
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

        //회전을 위한
        saveTetrominoArr = (int[,,,])tetroimino.Clone();

        for (int i = 0; i < 7; i++)
        {
            tetrominoType[i] = GameObject.Find(nameTetromino[i]);
        }

        InitTetromino();

        saveTetrominoArr = tetroimino;
    }

    private void Start()
    {
        while (llTetrominos.Count < 7)
        {
            llTetrominos.AddLast(Random.Range(0, 100) % 7); // 랜덤렌지에서 마지막숫자는 포함안함 그래서 +1해준다

            Debug.Log("랜덤값 확인용 " + "Count = " + llTetrominos.Count + "  value = " + llTetrominos.Last.Value);
        }
    }

    public void ReStartTetromino()
    {
        while (llTetrominos.Count != 0)
        {
            llTetrominos.RemoveLast();
        }

        while (llTetrominos.Count < 7)
        {
            llTetrominos.AddLast(Random.Range(0, 100) % 7); // 랜덤렌지에서 마지막숫자는 포함안함 그래서 +1해준다
        }
    }

    public void HoldTetromino()
    {
        // 홀드기능
        if (holdingTetromino == NONE)   // 첫 홀드라면
        {
            holdingTetromino = llTetrominos.First.Value;
            llTetrominos.RemoveFirst();
        }
        else
        {
            int temp = llTetrominos.First.Value;
            llTetrominos.First.Value = holdingTetromino;
            holdingTetromino = temp;
        }
        WhatIsNextTetrominos();
    }

    public void TetrominoGenerator(int nowTetromino)
    {
        // 맨앞 원소 제거후 마지막에 추가
        llTetrominos.RemoveFirst();

        while (llTetrominos.Count < 7) // 굳이 필요없을듯하지만 일단 넣어둔다.
        {
            llTetrominos.AddLast(Random.Range(0, 100) % 7);
        }
    }

    void InstantiateTetromino(int k, int i, int j, Vector2 vec2) // 조선제일검이 반복되는 것이 2개이상이면 함수로 빼라고 조언해주었다! 2022.3.21 오늘도 하나 배운다. 감사하다.
    {
        if (tetroimino[k, 0, i, j] == k + 2)
        {
            temp = Instantiate(gameBoard.emptyPrefab, new Vector3(vec2.x + j * 1.1F, vec2.y + i * 1.1F, 0), Quaternion.identity);

            temp.GetComponent<Renderer>().material = tetrominoMaterials.materials[k + 2];

            temp.transform.parent = tetrominoType[k].transform;
        }
    }



    void InitTetromino()
    {
        // 우측에 나올 인스턴트들을 미리 만들어둔다.

        Vector2 posOffSet;
        posOffSet.x = 15;
        posOffSet.y = 30;

        for (int k = 0; k < 7; ++k)
        {
            for (int i = 0; i < 4; ++i)
            {
                for (int j = 0; j < 4; ++j)
                {
                    InstantiateTetromino(k, i, j, posOffSet);
                }
            }
        }
    }

    public void RotateRight(int k /* 현재 선택된 테트로미노*/)
    {
        rotationIndex += 1;
        Debug.Log("nowRotaIndex = " + rotationIndex);

        if (3 < rotationIndex) rotationIndex = 0;
        // 
        //     /// 과거 회전
        //         int[,,] rotatedTetromino = new int[7, 4, 4];
        // 
        //         // 임시 배열에다가 뒤집어서 넣은후
        //         for (int i = 0; i < 4; ++i)
        //         {
        //             for (int j = 0; j < 4; ++j)
        //             {
        //                 rotatedTetromino[k, 3 - j, i] = tetroimino[k, i, j];
        //             }
        //         }
        //         // 원래 배열에다가 덮어 쓴다.
        //         for (int i = 0; i < 4; ++i)
        //         {
        //             for (int j = 0; j < 4; ++j)
        //             {
        //                 tetroimino[k, i, j] = rotatedTetromino[k, i, j];
        //             }
        //         }
    }

    public void ResetTetrominoArr()
    {
        rotationIndex = 0;

        /// 과거에 회전한 값을 되돌려 주기 위한 내용
        // 래퍼런스 타입으로 받기때문에 이렇게 쓰면 안된다.
        // 그냥대입이아니라 클론을 만들어서 대입하자 캐스팅해주어야한다.
        //        tetroimino = (int[,,])saveTetrominoArr.Clone();
    }

    public void RotateLeft(int k /* 현재 선택된 테트로미노*/)
    {
        rotationIndex -= 1;
        Debug.Log("nowRotaIndex = " + rotationIndex);

        if (rotationIndex < 0) rotationIndex = 3;
        // 
        //         ///과거의 회전
        //         int[,,] rotatedTetromino = new int[7, 4, 4];
        // 
        //         // 임시 배열에다가 뒤집어서 넣은후
        //         for (int i = 0; i < 4; ++i)
        //         {
        //             for (int j = 0; j < 4; ++j)
        //             {
        //                 rotatedTetromino[k, j, 3 - i] = tetroimino[k, i, j];
        //             }
        //         }
        //         // 원래 배열에다가 덮어 쓴다.
        //         for (int i = 0; i < 4; ++i)
        //         {
        //             for (int j = 0; j < 4; ++j)
        //             {
        //                 tetroimino[k, i, j] = rotatedTetromino[k, i, j];
        //             }
        //         }
    }

    public void WhatIsHoldingTetoromino()
    {
        // 자식들을 다지운다.
        if (holdBlocksParent.transform.childCount != 0)
        {
            for (int i = 0; i < holdBlocksParent.transform.childCount; i++)
            {
                Destroy(holdBlocksParent.transform.GetChild(i).gameObject);
            }
        }
        // foreach로 돌렸더니 2번째 인자에 접근할수가없어서 for문을 돌린다.
        if (holdingTetromino != NONE)
            Instantiate<GameObject>(tetrominoType[holdingTetromino], new Vector3(-12, 35, 0), Quaternion.identity, holdBlocksParent.transform);
    }
    public void WhatIsNextTetrominos()
    {
        // 우측에 다음 나올 테트로미노를 작성하자. // 포지션을 옮기면 안되고 만들어진것들을 복사해서 새로 만들어줘야한다.
        // 물론 만들어진 친구들은 삭제해줄것;
        //for (int i = 0; i < llTetrominos.Count; ++i)
        int _printPos = 7;

        // 자식들을 다지운다.
        for (int i = 0; i < previewBlocksParent.transform.childCount; i++)
        {
            Destroy(previewBlocksParent.transform.GetChild(i).gameObject);
        }

        // foreach로 돌렸더니 2번째 인자에 접근할수가없어서 for문을 돌린다.

        for (LinkedListNode<int> llint = llTetrominos.First.Next; llint != null; llint = llint.Next)
        {
            Instantiate<GameObject>(tetrominoType[llint.Value], new Vector3(30, (5 * _printPos), 0), Quaternion.identity, previewBlocksParent.transform);
            _printPos--;
        }
    }

    public void InitialrizeForRestart()
    {
        ReStartTetromino();

        holdingTetromino = NONE;

        WhatIsHoldingTetoromino();

        WhatIsNextTetrominos();
    }
}
