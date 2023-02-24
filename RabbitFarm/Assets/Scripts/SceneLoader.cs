using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    private void Update()
    {
        if (AddScore.addInstance.currentScore >= 200)
        {
            Invoke("End01",3.0f);
        }
    }
    public void AtOpeningSelectStart()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void AtSelectedStage1()
    {
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }

    public void CutScene01()
    {
        SceneManager.LoadScene(3, LoadSceneMode.Single);
    }

    public void ExplainScene01()
    {
        SceneManager.LoadScene(4, LoadSceneMode.Single);
    }
    public void End01()
    {
        AddScore.addInstance.currentScore = 0;
        SceneManager.LoadScene(5, LoadSceneMode.Single);
    }
}
