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

    private bool _isStop; //�Ƿ��ƶ�����

    public bool IsStop
    {
        get 
        {
            return _isStop; 
        }
        set 
        {
            stopObj.SetActive(value);

            if (value == true)
            {
                bodySp.color = Color.gray;
            } else
            {
                bodySp.color = Color.white;
            }
            _isStop = value;
        }
    }



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

        GameApp.MapManager.ShowStepGrid(this, Step);
    }

    //δѡ�лص�
    protected virtual void OnUnSelectCallBack(System.Object args)
    {
        GameApp.MapManager.HideStepGrid(this, Step);
    }

    public void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    //�ƶ���ָ���±�ĸ��� 
    public virtual bool Move(int rowIndex, int colIndex, float dt)
    {
        Vector3 pos = GameApp.MapManager.GetBlockPos(rowIndex, colIndex);

        pos.z = transform.position.z;

        if (transform.position.x > pos.x && transform.localScale.x > 0)
        {
            //ת��
            Flip();
        }

        if (transform.position.x < pos.x && transform.localScale.x < 0) 
        {
            Flip();
        }

        //�����Ŀ�ĵغܽ� ����true 
        if (Vector3.Distance(transform.position, pos) <= 0.02f)
        {
            this.RowIndex = rowIndex;
            this.ColIndex = colIndex;
            transform.position = pos;
            return true;
        }

        transform.position = Vector3.MoveTowards(transform.position, pos, dt);

        return false;
    }

    //���Ŷ���
    public void PlayAni(string aniName)
    {
        ani.Play(aniName);
    }

    //����
    public virtual void GetHit(ISkill skill)
    {

    }

    //������Ч����Ч���壩
    public virtual void PlayEffect(string name)
    {
        GameObject obj = Instantiate(Resources.Load($"Effect/{name}")) as GameObject;
        obj.transform.position = transform.position;
    }

    //��������model�ľ��루���������±���㣩
    public float GetDis(ModelBase model)
    {
        return Mathf.Abs(RowIndex - model.RowIndex) + Mathf.Abs(ColIndex - model.ColIndex);
    }

    //������Ч(���������˵�)
    public void PlaySound(string name)
    {
        GameApp.SoundManager.PlayEffect(name, transform.position);
    }

    //����ĳ��model
    public void LookAtModel(ModelBase model)
    {
        if((model.transform.position.x > transform.position.x) && (transform.localScale.x < 0))
        {
            Flip();
        } else if ((model.transform.position.x < transform.position.x) && (transform.localScale.x > 0))
        {
            Flip();
        }
    }
}
