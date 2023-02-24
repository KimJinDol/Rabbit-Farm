using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapInfo : MonoBehaviour
{
    public Tilemap tilemap; 

    public List<Vector3> hasTile; // 타일이 배치 된 곳을 리스트로 저장
    public List<Vector3Int> addTrap; // 트랩이 배치 된 곳을 리스트로 저장
    public List<Vector3Int> localPos; // 

    private Vector3 setCellSize;

    public float cellSize = 1.28f;

    public void Start()
    {
        setCellSize = new Vector3(cellSize, cellSize, 0);
        tilemap = transform.GetComponent<Tilemap>();
        GetComponentInParent<Grid>().cellSize = setCellSize;

        for (int n = tilemap.cellBounds.xMin; n < tilemap.cellBounds.xMax; n++) // 타일맵 셀값을 가져와 해당 위치에 타일이 존재하면 리스트에 add
        {
            for (int p = tilemap.cellBounds.yMin; p < tilemap.cellBounds.yMax; p++)
            {
                Vector3Int localPlace = (new Vector3Int(n, p, (int)tilemap.transform.position.y));
                localPos.Add(localPlace);
                Vector3 place = tilemap.CellToWorld(localPlace);
                if (tilemap.HasTile(localPlace))
                {
                    hasTile.Add(place);
                }
            }
        }
    }

    public void AddTrap(Vector3Int hasTrapPos) // 아이템이 설치되면 해당 포지션 값을 리스트에 저장
    {
        addTrap.Add(hasTrapPos);
        /*for (int i = 0; i < addTrap.Count; i++)
        {
            Debug.Log("has Trap : " + addTrap[i].x + ", " + addTrap[i].y);
        }*/
    }

    public void DestroyTrap(Vector3Int position)
    {
        for (int i = 0; i < addTrap.Count; i++)
        {
            if(addTrap[i] == position)
            {
                addTrap.RemoveAt(i);
                break;
            }
        }
    }
}
