using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Idle,
    Enter,
    Player,
    Enemy,
}
/// <summary>
/// 战斗管理器（用于管理战斗相关的实体（敌人、英雄、地图、格子等））
/// </summary>
public class FightWorldManager
{
    public GameState state = GameState.Idle;

    private FightUnitBase current;
    public List<Hero> heroList; //战斗中的英雄集合

    public List<Enemy> enemyList; //怪物集合

    public int RoundCount; //回合数

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

    //进入战斗 初始化 一些信息 敌人信息
    public void EnterFight()
    {
        RoundCount = 1;
        heroList = new List<Hero>();
        enemyList = new List<Enemy>();
        //将场景中的敌人脚本进行存储
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Enemy"); // 给怪物添加Enemy标签
        Debug.Log("Enemy objs length: " + objs.Length);
        for (int i = 0; i < objs.Length; i++)
        {
            Enemy enemy = objs[i].GetComponent<Enemy>();
            //当前位置被占用了，要把对应的方块类型设置为障碍物
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

    //添加英雄
    public void AddHero(Block b, Dictionary<string, string> data)
    {
        GameObject obj = UnityEngine.Object.Instantiate(Resources.Load($"Model/{data["Model"]}")) as GameObject;
        obj.transform.position = new Vector3(b.transform.position.x, b.transform.position.y, -1);
        Debug.Log("最终放置的位置：" + obj.transform.position);
        Hero hero = obj.AddComponent<Hero>();
        hero.Init(data, b.RowIndex, b.ColIndex);
        b.Type = BlockType.Obstacle;

        heroList.Add(hero);
    }

    //移除怪物
    public void RemoveEnemy(Enemy enemy)
    {
        enemyList.Remove(enemy);
    }

    //重置英雄行动
    public void ResetHeros()
    {
        for (int i = 0;i < heroList.Count; i++)
        {
            heroList[i].IsStop = false;
        }
    }

    //重置敌人行动
    public void ResetEnemies()
    {
        for (int i = 0;i <  enemyList.Count;i++)
        {
            enemyList[i].IsStop = false;
        }
    }
}

