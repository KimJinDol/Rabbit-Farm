using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Fence : Trap
{
    [SerializeField] private int recycleNum;

    private BoxCollider2D collider;

    public GameObject moleTarget;

    //Vector3 moleTransform;

    protected override void Start()
    {
        base.Start();
        ad = 0;
        recycleNum = 1;
        collider = gameObject.GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        moleTarget = GameObject.FindGameObjectWithTag("Mole");
        //moleTransform = new Vector3(-0.5f, 0, 0);
    }

    private IEnumerator CrumbleDelay()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (collision.gameObject.GetComponent<Enemy_Boar>())
            {
                trapPos = tilemap.WorldToCell(transform.position);
                tilemap.GetComponent<TileMapInfo>().DestroyTrap(trapPos);
                anim.SetTrigger("crumble");
                StartCoroutine(CrumbleDelay());
            }
        }
        if (collision.gameObject.GetComponent<Enemy_Mole>())
        {
            Invoke("DelayTP", 0.2f);
        }
    }

    public void DelayTP()
    {
        moleTarget.transform.position = tilemap.WorldToCell(transform.position);
    }
}
