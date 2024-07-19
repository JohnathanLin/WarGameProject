using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager
{
    private GameTimer timer; //计时器
    //skill:使用的技能 targetList:技能的作用目标 callback:回调
    private Queue<(ISkill skill, List<ModelBase> targetList, System.Action callback)> skillQueue; //技能队列

    public SkillManager() 
    {
        timer = new GameTimer();
        skillQueue = new Queue<(ISkill skill, List<ModelBase> targetList, System.Action callback)>();

    }

    //添加技能
    public void AddSkill(ISkill skill, List<ModelBase> targetList = null, System.Action callback = null)
    {
        skillQueue.Enqueue((skill, targetList, callback));
    }

    //使用技能
    public void UseSkill(ISkill skill, List<ModelBase> targetList, System.Action callback)
    {
        ModelBase current = (ModelBase)skill;
        //看向一个目标
        if (targetList.Count > 0)
        {
            current.LookAtModel(targetList[0]);
        }
        current.PlaySound(skill.skillPro.Sound); //播放音效
        current.PlayAni(skill.skillPro.AniName); //播放动画

        //延迟攻击
        timer.Register(skill.skillPro.AttackTime, delegate ()
        {
            //技能的最多作用个数
            int atkCount = skill.skillPro.AttackCount >= targetList.Count? targetList.Count: skill.skillPro.AttackCount;

            for (int i = 0; i < atkCount; i++)
            {
                targetList[i].GetHit(skill); //受伤
            }
        });

        //技能的持续时长
        timer.Register(skill.skillPro.Time, delegate ()
        {
            //回到待机
            current.PlayAni("idle");
            callback?.Invoke();
        });

    }

    public void Update(float dt)
    {
        timer.OnUpdate(dt);
        if (timer.Count() == 0 && skillQueue.Count > 0)
        {
            //下一个使用的技能
            var next = skillQueue.Dequeue();
            if (next.targetList != null)
            {
                UseSkill(next.skill, next.targetList, next.callback); //使用技能
            }
        }
    }

    //是否正在跑一个技能
    public bool IsRunningSkill()
    {
        if (timer.Count() == 0 && skillQueue.Count == 0)
        {
            return false;
        } else
        {
            return true;
        }
    }

    //清空技能
    public void Clear()
    {
        timer.Break();
        skillQueue.Clear();
    }
}
