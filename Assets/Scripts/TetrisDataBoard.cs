using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��Ʈ���� ������ ����10 ����23
/// </summary>
public enum eDefine
{
    WALL = 0,

    EMPTY = 1,

    FILLBOX = 2,

    HARDBOX = 99,

    DEADLINE = 98,
}

public class TetrisDataBoard : MonoBehaviour
{
    public List<GameObject> lBoxes = new List<GameObject>();

    // const�� ���� ���ο��� static���� �ٲ��, static�� �ٸ������� Ŭ�����̸��� ����� ������ �� �ִ�.
    public const int WIDTHMAX = 12;
    public const int HEIGHTMAX = 24;
    public const int STARTPOSX = 4;
    public const int STARTPOSY = 21;

    public GameObject emptyPrefab;

    public int[,] TetrisBoard;

    public void InitDataBoard()
    {
        TetrisBoard = new int[HEIGHTMAX, WIDTHMAX]; // ���� �ʿ��ϱ⶧���� ���� 12ĭ ���� 24ĭ���� ������ (���� ��,��,�Ʒ��� ���� ���̴�)

        for (int i = 0; i < HEIGHTMAX; ++i)
        {
            for (int j = 0; j < WIDTHMAX; ++j)
            {
                if (j == 0 || j == WIDTHMAX - 1 || i == 0)
                {
                    TetrisBoard[i, j] = (int)eDefine.WALL;
                }
                else if (i == CGameManager.GameOverLine)
                {
                    TetrisBoard[i, j] = (int)eDefine.DEADLINE;
                }
                else
                {
                    TetrisBoard[i, j] = (int)eDefine.EMPTY;
                }
            }
        }
    }

    private void Awake()
    {
        
        Transform empty = GameObject.Find("Empty").transform;

        SoundManager.instance.bgm.volume = 0.2f;
        SoundManager.instance.bgm.loop = true;
        SoundManager.instance.bgm.Play();

        TetrisBoard = new int[HEIGHTMAX, WIDTHMAX]; // ���� �ʿ��ϱ⶧���� ���� 12ĭ ���� 24ĭ���� ������ (���� ��,��,�Ʒ��� ���� ���̴�)

        for (int i = 0; i < HEIGHTMAX; ++i)
        {
            for (int j = 0; j < WIDTHMAX; ++j)
            {
                if (j == 0 || j == WIDTHMAX - 1 || i == 0)
                {
                    TetrisBoard[i, j] = (int)eDefine.WALL;
                }
                else if (i == CGameManager.GameOverLine)
                {
                    TetrisBoard[i, j] = (int)eDefine.DEADLINE;
                }
                else
                {
                    TetrisBoard[i, j] = (int)eDefine.EMPTY;
                }

                GameObject temp = Instantiate(emptyPrefab, new Vector3(j * 1.1F, i * 1.1F, 0), Quaternion.identity, empty);

                lBoxes.Add(temp);
            }
        }
    }
    public void InitBoard()
    {
        for (int i = 0; i < HEIGHTMAX; ++i)
        {
            for (int j = 0; j < WIDTHMAX; ++j)
            {

                if (j == 0 || j == WIDTHMAX - 1 || i == 0)
                {
                    TetrisBoard[i, j] = (int)eDefine.WALL;
                }
                else if (i == CGameManager.GameOverLine)
                {
                    TetrisBoard[i, j] = (int)eDefine.DEADLINE;
                }

                else
                {
                    TetrisBoard[i, j] = (int)eDefine.EMPTY;
                }


                GameObject temp = Instantiate(emptyPrefab, new Vector3(j * 1.1F, i * 1.1F, 0), Quaternion.identity, GameObject.Find("Empty").transform);
                lBoxes.Add(temp);
            }
        }
    }

    // ����ۿ� �ʱ�ȭ
    public void RestartInit()
    {
        for (int i = 0; i < HEIGHTMAX; ++i)
        {
            for (int j = 0; j < WIDTHMAX; ++j)
            {
                if (j == 0 || j == WIDTHMAX - 1 || i == 0)
                    TetrisBoard[i, j] = (int)eDefine.WALL;

                else if (i == CGameManager.GameOverLine)
                {
                    TetrisBoard[i, j] = (int)eDefine.DEADLINE;
                }

                else
                    TetrisBoard[i, j] = (int)eDefine.EMPTY;
            }
        }
    }

}