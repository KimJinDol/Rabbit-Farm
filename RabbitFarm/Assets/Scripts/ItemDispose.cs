using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class ItemDispose : MonoBehaviour,IDragHandler, IDropHandler, IEndDragHandler
{
    private GameManager gameManager;
    private Base player;
    private ItemBuffer itemBuffer;
    private ItemProperty selectTrap;
    private Tilemap tilemap;
    private Grid gd;

    private Vector3 cellPos;
    private Vector3 dropPos;
    private Vector3 cellSize;
    private Vector3Int cellPosInt;
    private Vector3Int tempPosInt;


    private Color originColor; // 원래 타일 색
    private Color emphasisColor; // 강조 타일 색
    private Color Deployable; // 배치 가능 타일 색
    private Color nonDispose; // 배치 불가 타일 색

    private string seletedTrapName;

    private bool cellMove; // 셀단위 커서가 움직였는지
    private bool availDispose; // 배치할 수 있는 곳인지
    private bool existTrap; // 해당 위치에 트랩이 존재하는지

    public bool bCanDrag;


    private UnityEngine.UI.Text costText;

    void Awake()
    {
        for (int i = -6; i < 9; i++)
        {
        Vector3Int pos = new Vector3Int(i, -5, 0);
        }
    }

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        player = FindObjectOfType<Base>();
        itemBuffer = FindObjectOfType<ItemBuffer>();
        tilemap = FindObjectOfType<Tilemap>();
        gd = GameObject.FindGameObjectWithTag("Grid").GetComponent<Grid>();
        costText = GameObject.FindGameObjectWithTag("Cost").GetComponent<UnityEngine.UI.Text>();

        cellMove = false;
        cellSize = tilemap.cellSize;
        availDispose = false; // 커서가 타일맵 범위를 벗어났는지 판단

        originColor = Color.white;
        Deployable = Color.gray;
        nonDispose = new Color(10, 0, 0, 0);

  
        bCanDrag = true;
    }



    public void OnDrag(PointerEventData eventData) // 타일 강조표시
    {
        if (gameManager.state != EState.IDLE) return;
        if (!bCanDrag) return;
        seletedTrapName = EventSystem.current.currentSelectedGameObject.name;
        transform.position = Input.mousePosition;
        cellPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        cellPosInt = gd.LocalToCell(cellPos); // 커서가 위치한 셀의 로컬좌표를 받아옴 

        if (tilemap.HasTile(cellPosInt)) // 해당 셀에 타일이 존재하면 실행
        {
            emphasisColor = Deployable;
            for (int i = 0; i < tilemap.GetComponent<TileMapInfo>().addTrap.Count; i++)
            {

                if (cellPosInt == tilemap.GetComponent<TileMapInfo>().addTrap[i])
                {
                    existTrap = true;
                    emphasisColor = nonDispose;
                    break;
                }
                else
                {
                    existTrap = false;
                    emphasisColor = Deployable;
                }
            }
            if (cellMove)
            {
                tilemap.SetColor(tempPosInt, originColor);
                tempPosInt = cellPosInt;
            }
            if (tempPosInt == cellPosInt)
            {
                tilemap.SetTileFlags(cellPosInt, TileFlags.None);
                originColor = tilemap.GetColor(cellPosInt);
                tilemap.SetColor(cellPosInt, emphasisColor);
            }
            else
            {
                cellMove = true;
            }
            availDispose = true;
        }
        else
        {
            availDispose = false;
            tilemap.SetColor(tempPosInt, originColor);
        }
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        transform.localPosition = Vector2.zero;
    }

    public void OnDrop(PointerEventData eventData)
    {

        if (availDispose)
        {
            if (!existTrap)
            {
                Debug.Log("cell pos : " + cellPosInt.x + ", " + cellPosInt.y + ", " +  cellPosInt.z);
                dropPos = new Vector3(tilemap.CellToWorld(cellPosInt).x, tilemap.CellToWorld(cellPosInt).y, 0);
                currntSelectedTrap(); // 선택된 아이템의 프리팹 정보를 가져와 selectTrap 변수에 할당
                Instantiate(selectTrap.prefab, dropPos, Quaternion.identity);
                tilemap.GetComponent<TileMapInfo>().AddTrap(cellPosInt);
            }
            tilemap.SetColor(tempPosInt, originColor);
        }

    }

    private void currntSelectedTrap() // 선택된 아이템의 프리팹 정보를 가져와 selectTrap 변수에 할당
    {
        for(int i = 0; i<itemBuffer.items.Count; i++)
        {
            if (EventSystem.current.currentSelectedGameObject.name == itemBuffer.items[i].name) // 선택된 아이템 종류와 아이템 버퍼 요소가 같다면
            {
                selectTrap = itemBuffer.items[i];

                player.SetCost(player.GetCost() - itemBuffer.items[i].cost);
                string newCost = player.GetCost().ToString();
                costText.text = newCost;
                Debug.Log("sell trap : " + player.GetCost());
            }
        }
        Debug.Log(EventSystem.current.currentSelectedGameObject.name);
    }

}
