using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Ÿ��Ʋ������ �޴��� ������ ���̴�.
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


    // ��ǲ���� ���缱�� �ε����� �����ϰ� ���⼭ �Ű������� �޾� ���� �ٲپ� �ش�.
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

    // Ű��ǲ Ŭ������ �ľ��ϴ°� ����غ���
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
