using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Idle
}
/// <summary>
/// ս�������������ڹ���ս����ص�ʵ�壨���ˡ�Ӣ�ۡ���ͼ�����ӵȣ���
/// </summary>
public class FightWorldManager
{
    public GameState state = GameState.Idle;

    private FightUnitBase current;

    public FightWorldManager()
    {
        ChangeState(GameState.Idle);
    }

    public FightUnitBase Current
    {
        get
        {
            return current;
        }
    }

    public void Update(float dt)
    {
        if (current != null && current.Update(dt) == false)
        {
            //todo
        }
        else
        {
            current = null;
        }
    }

    public void ChangeState(GameState state)
    {
        FightUnitBase _current = current;
        this.state = state;
 
        switch (state)
        {
            case GameState.Idle:
                _current = new FightIdle();
                break;
        }

        _current.Init();
    }
}

