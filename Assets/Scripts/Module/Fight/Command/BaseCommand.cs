using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//������ࣨ������ �ƶ� ʹ�ü��ܵȣ�
public class BaseCommand 
{
    public ModelBase model; //����Ķ���
    protected bool isFinish; //�Ƿ�������

    public BaseCommand(ModelBase model)
    {
        this.model = model;
        isFinish = false;
    }

    public BaseCommand()
    {

    }

    public virtual bool Update(float dt)
    {
        return isFinish;
    }

    //ִ������
    public virtual void Do()
    {
         
    }

    //����
    public virtual void UnDo()
    {

    }
}
