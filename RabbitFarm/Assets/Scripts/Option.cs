using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Option : MonoBehaviour
{

    public void AtClickOptionButton()
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        Time.timeScale = 0; // 시간의 흐름을 0으로 만들어서 일시 정지 시킨다
    }

    public void AtClickCancelButton()
    {
        Debug.Log("Cancel");
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        Time.timeScale = 1; // 일시 정지 해제
    }

    public void AtClickHelpButton()
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        Time.timeScale = 0; 
    }

    public void AtClickInHelpCancelButton()
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        Time.timeScale = 1; // 일시 정지 해제
    }


    public void AtClickStageSelectButton()
    {
        Time.timeScale = 1; // 일시 정지 해제
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void AtClickReplayButton()
    {
        Time.timeScale = 1; // 일시 정지 해제
        SceneManager.LoadScene(2, LoadSceneMode.Single);

    }

    public void AtClickQuitButton()
    {
        Application.Quit();
    }

}
