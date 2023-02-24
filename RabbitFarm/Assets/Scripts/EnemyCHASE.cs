using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCHASE : MonoBehaviour
{
    public float speed = 10;
    public float rotateSpeed = 200;
    //Rigidbody2D rigid;

    private Transform target;
    private Transform self;

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Base").GetComponent<Transform>();
        self = gameObject.GetComponent<Transform>();
        //rigid = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        if (Vector2.Distance(transform.position, target.position) > 3) // 플레이어와 적의 거리가 3 이하가 되면
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            //rigid.velocity = new Vector2(target.position.x, rigid.velocity.y);
        }
        //rotate(self, target.position, rotateSpeed);
        var newRotation = Quaternion.LookRotation(transform.position - target.position);
        newRotation.x = 0.0f;
        newRotation.y = 0.0f;
    }
}