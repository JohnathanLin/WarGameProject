using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Idle,
    Enter,
}
/// <summary>
/// ս�������������ڹ���ս����ص�ʵ�壨���ˡ�Ӣ�ۡ���ͼ�����ӵȣ���
/// </summary>
public class FightWorldManager
{
    public GameState state = GameState.Idle;

    private FightUnitBase current;
    public List<Hero> heroList; //ս���е�Ӣ�ۼ���

    public FightWorldManager()
    {
        heroList = new List<Hero>();
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
            case GameState.Enter:
                _current = new FightEnter();
                break;
        }

        _current.Init();
    }

    //���Ӣ��
    public void AddHero(Block b, Dictionary<string, string> data)
    {
        GameObject obj = UnityEngine.Object.Instantiate(Resources.Load($"Model/{data["Model"]}")) as GameObject;
        obj.transform.position = new Vector3(b.transform.position.x, b.transform.position.y, -1);
        Debug.Log("���շ��õ�λ�ã�" + obj.transform.position);
        Hero hero = obj.AddComponent<Hero>();
        hero.Init(data, b.RowIndex, b.ColIndex);
        b.Type = BlockType.Obstacle;

        heroList.Add(hero);
    }
}

