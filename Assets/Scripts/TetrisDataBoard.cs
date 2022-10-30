using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 테트리스 보드판 가로10 세로23
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

    // const를 쓰면 내부에서 static으로 바뀌고, static은 다른곳에서 클래스이름에 점찍고 접근할 수 있다.
    public const int WIDTHMAX = 12;
    public const int HEIGHTMAX = 24;
    public const int STARTPOSX = 4;
    public const int STARTPOSY = 21;

    public GameObject emptyPrefab;

    public int[,] TetrisBoard;

    public void InitDataBoard()
    {
        TetrisBoard = new int[HEIGHTMAX, WIDTHMAX]; // 벽이 필요하기때문에 가로 12칸 세로 24칸으로 설정함 (벽은 좌,우,아래만 있을 것이다)

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

        TetrisBoard = new int[HEIGHTMAX, WIDTHMAX]; // 벽이 필요하기때문에 가로 12칸 세로 24칸으로 설정함 (벽은 좌,우,아래만 있을 것이다)

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

    // 재시작용 초기화
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