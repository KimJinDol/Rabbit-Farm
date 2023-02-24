using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public event System.Action OnDeath;
    public static Enemy enemyInstance;
    public Animator animator;
    public float t;

    private GameManager gameManager;
    [SerializeField]
    protected int hp;

    [SerializeField]
    protected float speed;

    [SerializeField]
    protected int ad;

    [SerializeField]
    protected int score;

    public GameObject target;

    [SerializeField]
    private Transform self;

    private EState targetCurrentState;
    protected EState state;
    


    private BoxCollider2D collider;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>().GetComponent<GameManager>();

        // target = GameObject.FindGameObjectWithTag("Base");
        collider = GetComponent<BoxCollider2D>();
        state = EState.READY;
        speed = 2.0f;

        gameManager.GameStartDelay();
    }

    public void GameStart()
    {
        state = EState.IDLE;
    }

    public void Awake()
    {
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Base");
        self = gameObject.GetComponent<Transform>();
        //rigid = GetComponent<Rigidbody2D>();
        enemyInstance = this;
    }


    //protected void BoarTargetChase()
    //{
    //    targetCurrentState = target.GetComponent<Base>().CurrentState();

    //    if (targetCurrentState != EState.DEAD && state != EState.READY && state != EState.CLEAR && state != EState.GAMEOVER)
    //    {
    //        transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), target.transform.position, speed * Time.deltaTime);
    //    }
    //}
    protected void TargetChase()
    {
        transform.position = Vector3.MoveTowards(new Vector3(transform.position.x, transform.position.y), new Vector3(target.transform.position.x,transform.position.y), speed * Time.deltaTime);
    }

    public void SetEnemyState(EState newState)
    {
        state = newState;
    }

    public EState currentState()
    {
        return state;
    }

    protected void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    public int Attack()
    {
        return ad;
    }
    public int Score()
    {
        return score;
    }

    public void GetDamaged(int damaged)
    {
        if (hp - damaged > 0)
        {
            hp -= damaged;
            Debug.Log("Get Damaged!");
        }
        else
        {
            Dead();
        }
    }

    public void Dead()
    {
        AddScore.addInstance.SetScoreText();
        // 죽는 애니메이션 재생
        animator.SetTrigger("DEAD");
        if(OnDeath != null)
        {
            OnDeath();
        }
        Invoke("DelayDestroy", 0.4f);
    }

    public void DelayDestroy()
    {
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    virtual protected void Update()
    {

    }
}
