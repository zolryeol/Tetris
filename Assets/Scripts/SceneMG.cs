using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
///  씬매니저 싱글톤 씬의 이동
/// </summary>
/// 
public class SceneMG : MonoBehaviour
{
    public static SceneMG instance = null;

    private void Awake()
    {
        if (instance != null)   //이미 존재한다면 지워버려;
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        DontDestroyOnLoad(gameObject); // 씬을 이동해서 사라지지 않게 하는 함수
    }

    public void GoTitleScene()
    {
        SceneManager.LoadScene("Title");
    }
    public void GoSingleScene()
    {
        SceneManager.LoadScene("tetris");
    }
    public void GoBattleScene()
    {
        SceneManager.LoadScene("battle");
    }
}
