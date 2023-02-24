using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Mole : Enemy
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        score = 2;
        hp = 1;
        speed = 2.0f;
        ad = 1;

        transform.rotation = Quaternion.Euler(transform.localEulerAngles.x, transform.localEulerAngles.y,
    transform.localEulerAngles.z + -90.0f);
    }

    // Update is called once per frame
    protected override void Update()
    {
        t += Time.deltaTime;
        if (t >= 4.0f)
        {
            base.Update();
            if (state != EState.GAMEOVER)
            {
                TargetChase();
            }
        }
        //Vector2 direction = target.transform.position - transform.position;
        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

    }
    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.gameObject.GetComponent<Trap_Fence>())
    //    {
    //        Debug.Log("DD");
    //        animator.SetBool("CollisionHit", true);
    //    }
    //}
    //private void OnTriggerExit2D(Collider2D other)
    //{
    //    animator.SetBool("CollisionHit", false);
    //}
}
