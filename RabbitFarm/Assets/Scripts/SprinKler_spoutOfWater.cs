using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprinKler_spoutOfWater : Trap
{
    private BoxCollider2D collider;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        StartCoroutine(BombDelay());

        ad = 1;

        collider = gameObject.GetComponent<BoxCollider2D>();

        //anim = GetComponent<Animator>();
    }

    private IEnumerator BombDelay()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.GetComponentInParent<Enemy>().GetDamaged(ad);
        }
         
        trapPos = tilemap.WorldToCell(transform.position);
        tilemap.GetComponent<TileMapInfo>().DestroyTrap(trapPos);
        collider.enabled = false;
    }
}
