using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    Color defaultColor = Color.white;
    Color focusedColor = Color.yellow;

    public static UIManager Instance { private set; get; }
    public GameObject settingPanel;

    public Text[] texts;




    private void Awake() 
    {
        Instance = this;

        if (settingPanel == null)
        {
            settingPanel = GameObject.Find("Setting Panel");
        }
    }


    public void TurnOnPanel()
    {
        settingPanel.SetActive(true);
        SetText();
        GameManager.Instance.IsPaused = true;
    }

    public void TurnOffPanel()
    {
        settingPanel.SetActive(false);
        GameManager.Instance.IsPaused = false;
    }


    public void SetText()
    {
        for (int i = 0; i < texts.Length; i++)
        {
            texts[i].text = KeySetting.keys[(KeyAction) i].ToString();
            SetFocus(i, false);
        }
    }

    public void SetFocus(int index, bool isFocus)
    {
        texts[index].color =  isFocus ? focusedColor : defaultColor;
    }
}
