using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_SprinKler : Trap
{
    private BoxCollider2D collider;

    public GameObject widthSpoutOfWater;
    public GameObject heightSpoutOfWater;

    private float fixWidthPos;
    private float fixHeightPos;
    
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        collider = gameObject.GetComponent<BoxCollider2D>();

        fixWidthPos = 2;
        fixHeightPos = -2;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Bomb();
        }
    }

    private void Bomb()
    {
        trapPos = tilemap.WorldToCell(transform.position);
        tilemap.GetComponent<TileMapInfo>().DestroyTrap(trapPos);
        collider.enabled = false;

        Vector3 position = transform.position;

        // 가로 물줄기
        Vector3 widthDropPos = new Vector3(fixWidthPos, position.y, 0);
        Instantiate(widthSpoutOfWater, widthDropPos, Quaternion.identity);

        // 세로 물줄기 
        Vector3 heightDropPos = new Vector3(position.x, fixHeightPos, 0);
        Instantiate(heightSpoutOfWater, heightDropPos, Quaternion.identity);

        Destroy(this.gameObject); 
    }
}
