using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 타이틀씬에서 메뉴를 조작할 것이다.
/// </summary>
/// 
public class MenuController : MonoBehaviour
{
    [SerializeField]
    Image startButton;
    [SerializeField]
    Image battleButton;
    [SerializeField]
    Image noneButton;

    Image[] images = new Image[3];

    public Color selectColor;

    int nowSelectedIndex;

    private void Awake()
    {
        ///selectColor = Color.blue;
        images[0] = startButton;
        images[1] = battleButton;
        images[2] = noneButton;
    }

    void Update()
    {
        KeyInput();

        ChangeButtonColor(nowSelectedIndex);
    }


    // 인풋에서 현재선택 인덱스를 조절하고 여기서 매개변수로 받아 색을 바꾸어 준다.
    void ChangeButtonColor(int _SelectedButton)
    {
        for (int i = 0; i < images.Length; ++i)
        {
            if (_SelectedButton == i)
            {
                images[i].color = selectColor;
            }
            else images[i].color = Color.gray;
        }
    }

    // 키인풋 클래스를 파야하는가 고민해보자
    void KeyInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (0 < nowSelectedIndex) nowSelectedIndex -= 1;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (nowSelectedIndex < 2) nowSelectedIndex += 1;

        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            switch (nowSelectedIndex)
            {
                case 0:
                    {
                        SceneMG.instance.GoSingleScene();
                        break;
                    }

                case 1:
                    {
                        SceneMG.instance.GoBattleScene();
                        break;
                    }

                case 2:
                    {
                        Application.Quit();
                        break;
                    }
            }
        }
    }
}
