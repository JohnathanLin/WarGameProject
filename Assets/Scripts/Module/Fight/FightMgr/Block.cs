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
