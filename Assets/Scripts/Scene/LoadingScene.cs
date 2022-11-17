using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    public Slider progressBar;
    public Text text;

    private void Start() 
    {
        StartCoroutine(Loading());
    }

    IEnumerator Loading()
    {
        yield return null;
        AsyncOperation oper = SceneManager.LoadSceneAsync("Play Scene");
        oper.allowSceneActivation = false;

        while (!oper.isDone)
        {
            yield return null;

            if (progressBar.value < 0.9f)
            {
                progressBar.value = Mathf.MoveTowards(progressBar.value, 0.9f, Time.deltaTime);
            }
            else if (oper.progress >= 0.9f)
            {   
                progressBar.value = Mathf.MoveTowards(progressBar.value, 1f, Time.deltaTime);
            }



            if (progressBar.value >= 1f)
            {
                text.text = "Press SpaceBar to Start";
            }

            if (Input.GetKeyDown(KeyCode.Space) &&
                progressBar.value >= 1f && 
                oper.progress >= 0.9f)
            {
                oper.allowSceneActivation = true;
            }
        }
    }

}
