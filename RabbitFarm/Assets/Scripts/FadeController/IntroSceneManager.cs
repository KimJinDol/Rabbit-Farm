using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroSceneManager : MonoBehaviour
{
    public FadeController fader;
    private void Start()
    {
        StartCoroutine(Activate());
    }
    IEnumerator Activate()
    {
        fader.FadeOut(0.7f);
        yield return new WaitForSeconds(3.0f);
    }
}
