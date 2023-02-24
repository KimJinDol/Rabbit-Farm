using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursor : MonoBehaviour
{
    public Texture2D cursorTextureClick;
    public Texture2D cursorTextureidle;

    public bool hotSppotIsCenter = false; // 텍스처 중심을 마우스 좌표로 할 것인지
    public Vector2 adjustHotSpot; // 마우스 좌표값 설정할 변수
    private Vector2 hotSpot;

    void Start()
    {
        StartCoroutine(MyCursor());
    }
    private IEnumerator MyCursor()
    {
        yield return new WaitForEndOfFrame(); // 모든 렌더링이 완료될 때까지 대기

        if (hotSppotIsCenter)
        { // 마우스 좌표값 설정
            hotSpot.x = cursorTextureidle.width/2;
            hotSpot.y = cursorTextureidle.height/2;
        }
        else
        {
            adjustHotSpot = new Vector2(0, 0);
            hotSpot = adjustHotSpot;
        }
        Cursor.SetCursor(cursorTextureidle, hotSpot, CursorMode.Auto);
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0)) // 좌클릭 시
        {
            Cursor.SetCursor(cursorTextureClick, hotSpot, CursorMode.Auto);
        }
        
        if(Input.GetMouseButtonUp(0))
        {
            Cursor.SetCursor(cursorTextureidle, hotSpot, CursorMode.Auto);
        }
    }
}
