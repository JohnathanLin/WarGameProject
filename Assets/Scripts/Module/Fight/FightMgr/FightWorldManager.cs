using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public enum GameState
{
    Idle,
    Enter,
    Player,
    Enemy,
}
/// <summary>
/// ս�������������ڹ���ս����ص�ʵ�壨���ˡ�Ӣ�ۡ���ͼ�����ӵȣ���
/// </summary>
public class FightWorldManager
{
    public GameState state = GameState.Idle;

    private FightUnitBase current;
    public List<Hero> heroList; //ս���е�Ӣ�ۼ���

    public List<Enemy> enemyList; //���Ｏ��

    public int RoundCount; //�غ���

    public FightWorldManager()
    {
        heroList = new List<Hero>();
        enemyList = new List<Enemy>();
        ChangeState(GameState.Idle);
    }

    public FightUnitBase Current
    {
        get
        {
            return current;
        }
    }

    //����ս�� ��ʼ�� һЩ��Ϣ ������Ϣ
    public void EnterFight()
    {
        RoundCount = 1;
        heroList = new List<Hero>();
        enemyList = new List<Enemy>();
        //�������еĵ��˽ű����д洢
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Enemy"); // ���������Enemy��ǩ
        Debug.Log("Enemy objs length: " + objs.Length);
        for (int i = 0; i < objs.Length; i++)
        {
            Enemy enemy = objs[i].GetComponent<Enemy>();
            //��ǰλ�ñ�ռ���ˣ�Ҫ�Ѷ�Ӧ�ķ�����������Ϊ�ϰ���
            GameApp.MapManager.ChangeBlockType(enemy.RowIndex, enemy.ColIndex, BlockType.Obstacle);
            enemyList.Add(enemy);
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
            case GameState.Player:
                _current = new FightPlayerUnit();
                break;
            case GameState.Enemy:
                _current = new FightEnemyUnit();
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

    //�Ƴ�����
    public void RemoveEnemy(Enemy enemy)
    {
        enemyList.Remove(enemy);

        GameApp.MapManager.ChangeBlockType(enemy.RowIndex, enemy.ColIndex, BlockType.Null);
    }


    //�Ƴ�Ӣ��
    public void RemoveHero(Hero hero)
    {
        heroList.Remove(hero);

        GameApp.MapManager.ChangeBlockType(hero.RowIndex, hero.ColIndex, BlockType.Null);
    }

    //����Ӣ���ж�
    public void ResetHeros()
    {
        for (int i = 0;i < heroList.Count; i++)
        {
            heroList[i].IsStop = false;
        }
    }

    //���õ����ж�
    public void ResetEnemies()
    {
        for (int i = 0;i <  enemyList.Count;i++)
        {
            enemyList[i].IsStop = false;
        }
    }
    /// <summary>
    /// �����Ŀ�������Ӣ��
    /// </summary>
    /// <param name="model">Ŀ��</param>
    /// <returns></returns>
    public ModelBase GetMinDisHero(ModelBase model)
    {
        if (heroList.Count == 0)
        {
            return null;
        }
        Hero hero = heroList[0];
        float min_dis = hero.GetDis(model);
        for (int i = 1;i < heroList.Count; i++)
        {
            float dis = heroList[i].GetDis(model);
            if (dis < min_dis)
            {
                min_dis = dis;
                hero = heroList[i];
            }
        }
        return hero;
    }
}

