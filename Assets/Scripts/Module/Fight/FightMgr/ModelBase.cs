using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelBase : MonoBehaviour
{
    public int Id; //����Id
    public Dictionary<string, string> data; //���ݱ�
    public int Step; //�ж���
    public int Attack; //������
    public int Type; //����
    public int MaxHp; //���Ѫ��
    public int CurHp; //��ǰѪ��

    public int RowIndex;
    public int ColIndex;
    public SpriteRenderer bodySp; //����ͼƬ��Ⱦ���
    public GameObject stopObj; //ֹͣ�ж��ı������
    public Animator ani; //�������

    private void Awake()
    {
        bodySp = transform.Find("body").GetComponent<SpriteRenderer>();
        stopObj = transform.Find("stop").gameObject;
        ani = transform.Find("body").GetComponent<Animator>();
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        AddEvents();
    }

    protected virtual void OnDestroy()
    {
        RemoveEvents();
    }

    protected virtual void AddEvents()
    {
        GameApp.MessageCenter.AddEvent(gameObject, Defines.OnSelectEvent, OnSelectCallBack);
        GameApp.MessageCenter.AddEvent(Defines.OnUnSelectEvent, OnUnSelectCallBack);
    }

    protected virtual void RemoveEvents() 
    {
        GameApp.MessageCenter.RemoveEvent(Defines.OnSelectEvent, OnSelectCallBack);
        GameApp.MessageCenter.RemoveEvent(Defines.OnUnSelectEvent, OnUnSelectCallBack);
    }

    //ѡ�лص�
    protected virtual void OnSelectCallBack(System.Object args)
    {
        //ִ��δѡ��
        GameApp.MessageCenter.PostEvent(Defines.OnUnSelectEvent);
        //test
        bodySp.color = Color.red;
    }

    //δѡ�лص�
    protected virtual void OnUnSelectCallBack(System.Object args)
    {
        bodySp.color = Color.white;
    }
}
