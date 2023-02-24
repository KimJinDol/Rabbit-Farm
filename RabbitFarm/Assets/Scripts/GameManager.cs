using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EState
{
    READY,
    IDLE,
    DAMAGED,
    DEAD,
    CLEAR,
    GAMEOVER
}

public class GameManager : MonoBehaviour
{
    public EState state;
    // 폰
    private GameObject player;
    private GameObject[] enemy;

    int m_CurrentScore;
    // timer
//     [SerializeField] private int timer; // 타임어택 총 타임
//     private int min; // 남은 분
//     private int sec; // 남은 초
//     private string timerString;
//     private GameObject timerText;
    private GameObject readyImage;
    private IEnumerator startCoroutine;

    private bool bGameOver;
    private bool bGameClear;

    // [SerializeField] private GameObject gameOver; // gameOver UI

    void Start()
    {
        state = EState.READY;
        player = FindObjectOfType<Base>().gameObject;
        enemy = GameObject.FindGameObjectsWithTag("Enemy");
        //gameOver = GameObject.FindGameObjectWithTag("GameOver");
        bGameClear = false;
        bGameOver = false;

//         timerText = GameObject.FindGameObjectWithTag("Timer");
//         timer = 120;
//         timerString = timer / 60 + " : " + timer % 60;
//         timerText.GetComponent<UnityEngine.UI.Text>().text = timerString; // 캐릭터 자산 

        /*gameOver.transform.parent.gameObject.SetActive(false);
        gameOver.gameObject.SetActive(false);*/
        FindObjectOfType<Option>().transform.GetChild(0).gameObject.SetActive(false);

        readyImage = GameObject.FindGameObjectWithTag("Ready");

        for(int i = 0; i <4; i++)
        {
            readyImage.transform.GetChild(0).transform.GetChild(i).gameObject.SetActive(false);
        }
        startCoroutine = GameStartDelay();
        StartCoroutine(startCoroutine);
    }

    public IEnumerator GameStartDelay()
    {
        readyImage.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        readyImage.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
        readyImage.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        readyImage.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(false);
        readyImage.transform.GetChild(0).transform.GetChild(2).gameObject.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        readyImage.transform.GetChild(0).transform.GetChild(2).gameObject.SetActive(false);
        readyImage.transform.GetChild(0).transform.GetChild(3).gameObject.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        GameStart();
        readyImage.transform.GetChild(0).transform.GetChild(3).gameObject.SetActive(false);
    }

    void GameStart()
    {
        StopCoroutine(startCoroutine);
        state = EState.IDLE;
        player.GetComponent<Base>().GameStart();
        for (int i = 0; i < FindObjectsOfType<Enemy>().Length-40; i++)
        { 
            enemy[i].gameObject.GetComponent<Enemy>().GameStart();
        }
    }
    public IEnumerator ChangeStateDelay()
    {
        yield return new WaitForSeconds(1.0f);
        
    }

    public void GameOver()
    {
        Debug.Log("gameOVER!!!!!!!!!!!!!!!!!!!!");
        state = EState.GAMEOVER;
        GameObject.FindGameObjectWithTag("GameOver").transform.GetChild(0).gameObject.SetActive(true);
        player.GetComponent<Base>().SetPlayerState(EState.GAMEOVER);
        enemy = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i<FindObjectsOfType<Enemy>().Length; i++)
        {
            enemy[i].GetComponentInParent<Enemy>().SetEnemyState(EState.GAMEOVER);
        }
        StartCoroutine(ChangeStateDelay());
    }

    public void GameClear()
    {
        state = EState.CLEAR;
        GameObject.FindGameObjectWithTag("GameClear").transform.GetChild(0).gameObject.SetActive(true);
        player.GetComponent<Base>().SetPlayerState(EState.CLEAR);
        enemy = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < FindObjectsOfType<Enemy>().Length; i++)
        {
            enemy[i].GetComponentInParent<Enemy>().SetEnemyState(EState.GAMEOVER);
        }
        StartCoroutine(ChangeStateDelay());
    }


    private float sumDeltaTime_timer = 0;

    void Update()
    {
//         if (player.GetComponent<Base>().CurrentState() == EState.IDLE &&
//             state != EState.GAMEOVER && state != EState.CLEAR && state != EState.READY &&state != EState.DEAD) 
//         {
//             sumDeltaTime_timer += Time.deltaTime;
//             if (sumDeltaTime_timer >= 1) // 타이머 업데이트
//             {
//                 timer -= 1;
//                 sumDeltaTime_timer -= 1;
// 
//                 min = timer / 60;
//                 sec = timer % 60;
//                 timerString = min + " : " + sec;
//                 timerText.GetComponent<UnityEngine.UI.Text>().text = timerString;
//                 if (timer <= 0)
//                 {
//                     if (bGameClear) return;
//                     GameClear();
//                     bGameClear = true;
//                 }
//             }
//             
//         }
        if(player.GetComponent<Base>().CurrentState() == EState.DEAD && state != EState.GAMEOVER) // 플레이어 사망 시 게임 오버
        {
            if (bGameOver) return;
            GameOver();
            bGameOver = true;
        }
        if(AddScore.addInstance.currentScore >= 200)
        {
            if (bGameClear) return;
            GameClear();
            bGameClear = true;
        }
    }
}
