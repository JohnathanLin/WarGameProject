using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//ģ�ͻ���
public class BaseModel
{
    public BaseController controller;
    public BaseModel(BaseController ctl)
    {
        this.controller = ctl;
    }

    public virtual void Init()
    {


    }
}
