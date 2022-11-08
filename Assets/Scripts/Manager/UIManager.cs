using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
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
    }

    public void TurnOffPanel()
    {
        settingPanel.SetActive(false);
    }


    public void SetText()
    {
        for (int i = 0; i < texts.Length; i++)
        {
            texts[i].text = KeySetting.keys[(KeyAction) i].ToString();
        }
    }
}
