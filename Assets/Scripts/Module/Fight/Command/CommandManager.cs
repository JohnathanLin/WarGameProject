using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���������
/// </summary>
public class CommandManager
{
    private Queue<BaseCommand> willDoCommandQueue; //��Ҫִ�е��������
    private Stack<BaseCommand> unDoStack; //���������� ջ
    private BaseCommand current; //��ǰ��ִ�е�ָ��

    public CommandManager()
    {
        willDoCommandQueue = new Queue<BaseCommand>();
        unDoStack = new Stack<BaseCommand>();
    }

    //�Ƿ�������ִ����
    public bool IsRunningCommand
    {
        get
        {
            return current != null;
        }
    }

    //�������
    public void AddCommand(BaseCommand command)
    {
        willDoCommandQueue.Enqueue(command); //��ӵ��������
        unDoStack.Push(command); //��ӵ�����ջ
    }

    public void Update(float dt)
    {
        if (current == null)
        {
            if (willDoCommandQueue.Count > 0)
            {
                current = willDoCommandQueue.Dequeue();
                current.Do(); //ִ��
            }
        }
        else
        {
            if (current.Update(dt))
            {
                current = null;
            }
        }
    }

    public void Clear()
    {
        willDoCommandQueue.Clear();
        unDoStack.Clear();
        current = null;
    }

    //������һ������
    public void UnDo()
    {
        if (unDoStack.Count > 0)
        {
            unDoStack.Pop().UnDo();
        }
    }
}
