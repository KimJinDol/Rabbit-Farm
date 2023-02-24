using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Trap_hole : Trap
{
    private BoxCollider2D collider;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        ad = 1;

        collider = gameObject.GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.gameObject.CompareTag("Enemy"))
        if(collision.gameObject.GetComponent<Enemy_Mouse>() || collision.gameObject.GetComponent<Enemy_Boar>())
        {
            collision.GetComponentInParent<Enemy>().GetDamaged(ad);
            trapPos = tilemap.WorldToCell(transform.position);
            tilemap.GetComponent<TileMapInfo>().DestroyTrap(trapPos);
            collider.enabled = false;
            Destroy(this.gameObject);
        }
        
    }
}
