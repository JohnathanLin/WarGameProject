using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BlockType
{
    Null,
    Obstacle,
}

public class Block : MonoBehaviour
{
    public int RowIndex; //��
    public int ColIndex; //��
    public BlockType Type; //��������
    private SpriteRenderer selectSp; //ѡ�еĸ���ͼƬ
    private SpriteRenderer gridSp; //����ͼƬ
    private SpriteRenderer dirSp; //�ƶ�����ͼƬ

    private void Awake()
    {
        selectSp = transform.Find("select").GetComponent<SpriteRenderer>();
        gridSp = transform.Find("grid").GetComponent<SpriteRenderer>();
        dirSp = transform.Find("dir").GetComponent<SpriteRenderer>();

        GameApp.MessageCenter.AddEvent(gameObject, Defines.OnSelectEvent, OnSelectCallBack);
    }

    private void OnDestroy()
    {
        GameApp.MessageCenter.RemoveEvent(gameObject, Defines.OnSelectEvent, OnSelectCallBack);
    }

    public void ShowGrid(Color color)
    {
        gridSp.enabled = true;
        gridSp.color = color;
    }

    public void HideGrid()
    {
        gridSp.enabled = false; 
    }

    void OnSelectCallBack(System.Object args)
    {
        GameApp.MessageCenter.PostEvent(Defines.OnUnSelectEvent);
    }

    private void OnMouseEnter()
    {
        selectSp.enabled = true;
    }

    private void OnMouseExit()
    {
        selectSp.enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
}
