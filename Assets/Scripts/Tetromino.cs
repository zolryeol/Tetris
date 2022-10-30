using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��Ʈ�ι̳��� ������ ����ִ� Ŭ����
/// </summary>
public class Tetromino : MonoBehaviour
{
    public Renderer tetrominoMaterials;

    public int nowTetromino;
    public int holdingTetromino;        // Ȧ���� ģ���� �����ϴ� ����

    public bool isCreated;

    public TetrisDataBoard gameBoard;
    private GameObject temp;

    public bool isCollided;

    GameObject previewBlocksParent;
    GameObject holdBlocksParent;

    public int rotationIndex; // �ð�ȸ���ϸ� ++ �ִ� 3 ����; �ݽð�� --;

    public int[,,,] tetroimino;
    public int[,,,] saveTetrominoArr;

    public LinkedList<int> llTetrominos;

    public const int NONE = 44; // Ȧ�� �ߴ°� ���ߴ°� Ȯ�ο�

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

        tetroimino = new int[7, 4, 4, 4]// ��Ʈ�ι̳�� 7�����̴�. 
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

        //ȸ���� ����
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
            llTetrominos.AddLast(Random.Range(0, 100) % 7); // ������������ ���������ڴ� ���Ծ��� �׷��� +1���ش�

            Debug.Log("������ Ȯ�ο� " + "Count = " + llTetrominos.Count + "  value = " + llTetrominos.Last.Value);
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
            llTetrominos.AddLast(Random.Range(0, 100) % 7); // ������������ ���������ڴ� ���Ծ��� �׷��� +1���ش�
        }
    }

    public void HoldTetromino()
    {
        // Ȧ����
        if (holdingTetromino == NONE)   // ù Ȧ����
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
        // �Ǿ� ���� ������ �������� �߰�
        llTetrominos.RemoveFirst();

        while (llTetrominos.Count < 7) // ���� �ʿ������������ �ϴ� �־�д�.
        {
            llTetrominos.AddLast(Random.Range(0, 100) % 7);
        }
    }

    void InstantiateTetromino(int k, int i, int j, Vector2 vec2) // �������ϰ��� �ݺ��Ǵ� ���� 2���̻��̸� �Լ��� ����� �������־���! 2022.3.21 ���õ� �ϳ� ����. �����ϴ�.
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
        // ������ ���� �ν���Ʈ���� �̸� �����д�.

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

    public void RotateRight(int k /* ���� ���õ� ��Ʈ�ι̳�*/)
    {
        rotationIndex += 1;
        Debug.Log("nowRotaIndex = " + rotationIndex);

        if (3 < rotationIndex) rotationIndex = 0;
        // 
        //     /// ���� ȸ��
        //         int[,,] rotatedTetromino = new int[7, 4, 4];
        // 
        //         // �ӽ� �迭���ٰ� ����� ������
        //         for (int i = 0; i < 4; ++i)
        //         {
        //             for (int j = 0; j < 4; ++j)
        //             {
        //                 rotatedTetromino[k, 3 - j, i] = tetroimino[k, i, j];
        //             }
        //         }
        //         // ���� �迭���ٰ� ���� ����.
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

        /// ���ſ� ȸ���� ���� �ǵ��� �ֱ� ���� ����
        // ���۷��� Ÿ������ �ޱ⶧���� �̷��� ���� �ȵȴ�.
        // �׳ɴ����̾ƴ϶� Ŭ���� ���� �������� ĳ�������־���Ѵ�.
        //        tetroimino = (int[,,])saveTetrominoArr.Clone();
    }

    public void RotateLeft(int k /* ���� ���õ� ��Ʈ�ι̳�*/)
    {
        rotationIndex -= 1;
        Debug.Log("nowRotaIndex = " + rotationIndex);

        if (rotationIndex < 0) rotationIndex = 3;
        // 
        //         ///������ ȸ��
        //         int[,,] rotatedTetromino = new int[7, 4, 4];
        // 
        //         // �ӽ� �迭���ٰ� ����� ������
        //         for (int i = 0; i < 4; ++i)
        //         {
        //             for (int j = 0; j < 4; ++j)
        //             {
        //                 rotatedTetromino[k, j, 3 - i] = tetroimino[k, i, j];
        //             }
        //         }
        //         // ���� �迭���ٰ� ���� ����.
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
        // �ڽĵ��� �������.
        if (holdBlocksParent.transform.childCount != 0)
        {
            for (int i = 0; i < holdBlocksParent.transform.childCount; i++)
            {
                Destroy(holdBlocksParent.transform.GetChild(i).gameObject);
            }
        }
        // foreach�� ���ȴ��� 2��° ���ڿ� �����Ҽ������ for���� ������.
        if (holdingTetromino != NONE)
            Instantiate<GameObject>(tetrominoType[holdingTetromino], new Vector3(-12, 35, 0), Quaternion.identity, holdBlocksParent.transform);
    }
    public void WhatIsNextTetrominos()
    {
        // ������ ���� ���� ��Ʈ�ι̳븦 �ۼ�����. // �������� �ű�� �ȵǰ� ��������͵��� �����ؼ� ���� ���������Ѵ�.
        // ���� ������� ģ������ �������ٰ�;
        //for (int i = 0; i < llTetrominos.Count; ++i)
        int _printPos = 7;

        // �ڽĵ��� �������.
        for (int i = 0; i < previewBlocksParent.transform.childCount; i++)
        {
            Destroy(previewBlocksParent.transform.GetChild(i).gameObject);
        }

        // foreach�� ���ȴ��� 2��° ���ڿ� �����Ҽ������ for���� ������.

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
