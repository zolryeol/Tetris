using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
///  ���Ŵ��� �̱��� ���� �̵�
/// </summary>
/// 
public class SceneMG : MonoBehaviour
{
    public static SceneMG instance = null;

    private void Awake()
    {
        if (instance != null)   //�̹� �����Ѵٸ� ��������;
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        DontDestroyOnLoad(gameObject); // ���� �̵��ؼ� ������� �ʰ� �ϴ� �Լ�
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
