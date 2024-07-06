using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Idle
}
/// <summary>
/// 战斗管理器（用于管理战斗相关的实体（敌人、英雄、地图、格子等））
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

