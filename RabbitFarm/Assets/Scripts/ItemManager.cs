using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField] private GameObject trap_carrotBomb;

    // 울타리 오브젝트 

    void Start()
    {
        trap_carrotBomb = Resources.Load("Trap_carrotBomb") as GameObject;
        // 울타리 프리팹 불러오기
    }

    public void PlaceTrapCarrotBomb(Vector3Int position)
    {
        Instantiate(trap_carrotBomb, position, Quaternion.identity);
    }

}
