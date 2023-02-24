using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Trap_CarrotBomb : Trap
{
    [SerializeField] private float boundDistance;

    private CircleCollider2D collider;

    private GameObject[] enemy;
    //GameObject mole;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        ad = 1;
        boundDistance = 3.0f;

        collider = gameObject.GetComponent<CircleCollider2D>();
        anim = GetComponent<Animator>();
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("Enemy")&& !collision.gameObject.CompareTag("Base"))
        {
            anim.SetTrigger("Bomb");
            Debug.Log("Enemy Check");
            Bomb();
        }
    }

    private IEnumerator BombDelay()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(this.gameObject);
    }
    private void Bomb()
    {
        enemy = GameObject.FindGameObjectsWithTag("Enemy");

        for (int i = 0; i < enemy.Length; i++)
        {
            if (Vector2.Distance(new Vector2(transform.position.x, transform.position.y),
                new Vector2(enemy[i].transform.position.x, enemy[i].transform.position.y)) <= boundDistance)
            {
                enemy[i].GetComponentInParent<Enemy>().GetDamaged(ad);
            }
        }
        trapPos = tilemap.WorldToCell(transform.position);
        tilemap.GetComponent<TileMapInfo>().DestroyTrap(trapPos);
        collider.enabled = false;
        StartCoroutine(BombDelay());
    }
}
