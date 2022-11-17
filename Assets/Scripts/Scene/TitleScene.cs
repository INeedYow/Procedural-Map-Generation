using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class TitleScene : MonoBehaviour
{
    const int defaultMin = 10;
    const int defaultMax = 99;

    public InputField minInput;
    public InputField maxInput;
    public Text minText;
    public Text maxText;
    GameObject startButton;


    int min = defaultMin;
    int max = defaultMax;


    private void Awake() 
    {
        startButton = GameObject.Find("Start Button");

        minInput.text = defaultMin.ToString();
        maxInput.text = defaultMax.ToString();
        minText.text = minInput.text;
        maxText.text = maxInput.text;
    }


    public void OnChangeMin()
    {
        int temp;

        if (Int32.TryParse(minInput.text, out temp))
        {
            if (temp < 10)
                return;

            min = temp;
            minText.text = minInput.text;
            Check();
        }
    }

    public void OnChangeMax()
    {
        int temp;

        if (Int32.TryParse(maxInput.text, out temp))
        {
            if (temp < 10)
                return;

            max = temp;
            maxText.text = maxInput.text;
            Check();
        }
    }

    void Check()
    {
        SwitchButton(CanStart());
    }

    bool CanStart()
    {
        return min <= max; 
    }

    void SwitchButton(bool isActive)
    {
        startButton.SetActive(isActive);
    }

    public void GameStart()
    {
        PlayerPrefs.SetInt("min", min);
        PlayerPrefs.SetInt("max", max);

        SceneManager.LoadSceneAsync("Loading Scene");
    }
}
