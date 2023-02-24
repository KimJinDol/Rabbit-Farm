using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Base : MonoBehaviour
{
    public static Base instance;
    Vector3 baseTransform;
    [SerializeField]
    Transform boarTransform;
    // Start is called before the first frame update

    private GameManager gameManager;

    [SerializeField] private int costSpeed; // 재화 차는 속도
    [SerializeField] private int MaxCost;
    [SerializeField] private int cost;
    private GameObject costText;

    [SerializeField] private int MaxHP;
    [SerializeField] private int currentHP;

    [SerializeField] private Collider2D collider;

    private EState state;

    private Animator anim;

    private GameObject hpImage;

    public void Awake() // 변수 할당
    {
        costSpeed = 1;

        MaxCost = 999;
        cost = 20;

        MaxHP = 3;
        currentHP = MaxHP;

        state = EState.READY;
        instance = this;
    }

    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>().GetComponent<GameManager>();

        costText = GameObject.FindGameObjectWithTag("Cost");

        collider = GetComponent<BoxCollider2D>();

        anim = GetComponent<Animator>();
        anim.SetBool("Start", false);

        hpImage = GameObject.FindGameObjectWithTag("Stat");

        gameManager.GameStartDelay();
        baseTransform = new Vector3(-4, 10, 0);
    }

    public void GameStart() // 게임이 시작되면 호출
    {
        state = EState.IDLE;
        anim.SetBool("Start", true);
    }

    private void OnTriggerEnter2D(Collider2D collision) // 적과 닿았을 때 활성화
    {
        if (!collision.GetComponentInParent<Enemy>()) return;
        Damaged(collision.GetComponentInParent<Enemy>().Attack()); // 플레이어 getDamaged;
        collision.gameObject.GetComponentInParent<Enemy>().Dead(); // 맞닿은 적 사망
    }

    public void SetCost(int newCost)
    {
        cost = newCost;
    }

    public int GetCost() // 현재 가진 재화를 반환
    {
        return cost;
    }

    private IEnumerator Delay() // 데미지 입은 애니메이션 딜레이 함수
    {
        yield return new WaitForSeconds(3.0f);
        anim.SetBool("Damaged", false);
    }

    public void Damaged(int newDamaged) // 적에게 데미지를 입었을 때 호출
    {
        currentHP -= newDamaged;
        Debug.Log("Player Current HP : " + currentHP);
        hpImage.transform.GetChild(currentHP).GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/image/I_HP02");
        if (currentHP <= 0)
        {
            Dead();
        }
        else
        {
            anim.SetBool("Damaged", true);
            StartCoroutine(Delay());
        }
        
    }

    public void SetPlayerState (EState newState) // 플레이어의 상태 갱신
    {
        state = newState;
    }

    public EState CurrentState() // 플레이어의 상태 반환
    {
        return state;
    }

    private void Dead() // 플레이어 사망 시 호출
    {
        anim.SetBool("Dead", true);
        Debug.Log("Player DEAD");
        state = EState.DEAD;
    }

    private float sumDeltaTime = 0.0f;
    
    // Update is called once per frame
    void Update() 
    {
        if (state == EState.IDLE)
        {
            if (cost < MaxCost) // 3초마다 1재화 충전
                {
                sumDeltaTime += Time.deltaTime;
                if (sumDeltaTime >= costSpeed) {
                    cost += 1;
                    costText.GetComponent<UnityEngine.UI.Text>().text = cost.ToString(); // 자산을 UI에 동기화
                    sumDeltaTime -= costSpeed;
                }
            }
            else
            {
                state = EState.CLEAR;
            }
        }
    }
}
