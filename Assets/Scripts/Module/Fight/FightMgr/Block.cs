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
    public int RowIndex; //行
    public int ColIndex; //列
    public BlockType Type; //格子类型
    private SpriteRenderer selectSp; //选中的格子图片
    private SpriteRenderer gridSp; //网格图片
    private SpriteRenderer dirSp; //移动方向图片

    public void Awake()
    {
        selectSp = transform.Find("select").GetComponent<SpriteRenderer>();
        gridSp = transform.Find("grid").GetComponent<SpriteRenderer>();
        dirSp = transform.Find("dir").GetComponent<SpriteRenderer>();

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
