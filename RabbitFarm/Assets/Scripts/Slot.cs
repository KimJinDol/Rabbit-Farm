using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Slot : MonoBehaviour //, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    [HideInInspector]
    public ItemProperty item;
    public UnityEngine.UI.Image image;
    private UnityEngine.UI.Text itemNameText;

    private UnityEngine.UI.Text costText;
    private UnityEngine.UI.Image costImage;

    private Base player;
    private ItemDispose itemDispose;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Base").GetComponent<Base>(); // 플레이어의 재화 정보를 update로 가져와서 활성화 할 아이템 구분
        itemDispose = transform.GetChild(0).GetComponent<ItemDispose>();
    }

    public void SetItem(ItemProperty item)
    {
        this.item = item;
        costText = this.gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).GetComponent<UnityEngine.UI.Text>();
        costImage = this.gameObject.transform.GetChild(1).GetComponent<Image>();
        itemNameText = this.gameObject.transform.GetChild(2).GetComponent<UnityEngine.UI.Text>();

        if (item==null)
        {
            costText.gameObject.SetActive(false);

            image.enabled = false;
            
            gameObject.name = "Empty";
            gameObject.GetComponent<Image>().enabled = false;

        }
        else
        {
            costText.gameObject.SetActive(true);
            costText.text = item.cost.ToString();
            costText.fontSize = 35;

            image.enabled = true;
            image.sprite = item.sprite;

            gameObject.name = item.name;
            gameObject.GetComponent<Image>().enabled = true;

            itemNameText.text = item.itemNameText;
        }
    }
    private void Update()
    {
        if(player.GetCost() < item.cost)
        {
            itemDispose.bCanDrag = false;
        }
        else
        {
            itemDispose.bCanDrag = true;
        }
    }
}
