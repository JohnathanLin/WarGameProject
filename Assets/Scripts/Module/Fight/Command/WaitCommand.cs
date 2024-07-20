using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ȴ�ָ��
/// </summary>
public class WaitCommand : BaseCommand
{
    private float time;
    System.Action callback;

    public WaitCommand(float t, System.Action callback = null) {
        this.time= t;
        this.callback = callback;
    }

    public override bool Update(float dt)
    {
        this.time -= dt;
        if (this.time <= 0)
        {
            callback?.Invoke();
            return true;
        }
        return false;
    }
}
