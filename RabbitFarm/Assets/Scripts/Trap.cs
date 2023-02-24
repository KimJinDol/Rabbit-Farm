using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Trap : MonoBehaviour
{
    protected Vector3 trapPosition; // 트랩이 설치된 월드 위치
    protected Vector3Int trapPos; //  트랩이 설치된 셀 위치

    protected Grid gd;
    protected Tilemap tilemap;

    protected Animator anim;

    [SerializeField] protected int ad;


    // Start is called before the first frame update
    protected virtual void Start()
    {
        gd = GameObject.FindGameObjectWithTag("Grid").GetComponent<Grid>();
        tilemap = GameObject.FindGameObjectWithTag("TilePath").GetComponent<Tilemap>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }
}
