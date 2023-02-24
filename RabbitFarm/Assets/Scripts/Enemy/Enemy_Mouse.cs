using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Mouse : Enemy
{
    public float t;
    // Start is called before the first frame update
    protected override void Start() 
    {
        base.Start();
        score = 1;
        hp = 1;
        speed = 1.5f;
        ad = 1;
        transform.rotation = Quaternion.Euler(transform.localEulerAngles.x, transform.localEulerAngles.y,
    transform.localEulerAngles.z + -90.0f);
    }

    // Update is called once per frame
    protected override void Update()
    {
        t += Time.deltaTime;
        if(t >= 4.0f)
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
}
