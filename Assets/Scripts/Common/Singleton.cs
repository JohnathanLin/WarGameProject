using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ����
/// </summary>
public class Singleton<T>
{
    private static readonly T instance = Activator.CreateInstance<T>();

    public static T Instance
    { get { return instance; } }

    //��ʼ��
    public virtual void Init()
    {

    }

    //ÿִ֡��

    public virtual void Update(float dt)
    {

    }
    
    //�ͷ�
    public virtual void onDestroy()
    {

    }
}
