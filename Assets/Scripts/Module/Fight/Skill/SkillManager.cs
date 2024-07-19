using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager
{
    private GameTimer timer; //��ʱ��
    //skill:ʹ�õļ��� targetList:���ܵ�����Ŀ�� callback:�ص�
    private Queue<(ISkill skill, List<ModelBase> targetList, System.Action callback)> skillQueue; //���ܶ���

    public SkillManager() 
    {
        timer = new GameTimer();
        skillQueue = new Queue<(ISkill skill, List<ModelBase> targetList, System.Action callback)>();

    }

    //��Ӽ���
    public void AddSkill(ISkill skill, List<ModelBase> targetList = null, System.Action callback = null)
    {
        skillQueue.Enqueue((skill, targetList, callback));
    }

    //ʹ�ü���
    public void UseSkill(ISkill skill, List<ModelBase> targetList, System.Action callback)
    {
        ModelBase current = (ModelBase)skill;
        //����һ��Ŀ��
        if (targetList.Count > 0)
        {
            current.LookAtModel(targetList[0]);
        }
        current.PlaySound(skill.skillPro.Sound); //������Ч
        current.PlayAni(skill.skillPro.AniName); //���Ŷ���

        //�ӳٹ���
        timer.Register(skill.skillPro.AttackTime, delegate ()
        {
            //���ܵ�������ø���
            int atkCount = skill.skillPro.AttackCount >= targetList.Count? targetList.Count: skill.skillPro.AttackCount;

            for (int i = 0; i < atkCount; i++)
            {
                targetList[i].GetHit(skill); //����
            }
        });

        //���ܵĳ���ʱ��
        timer.Register(skill.skillPro.Time, delegate ()
        {
            //�ص�����
            current.PlayAni("idle");
            callback?.Invoke();
        });

    }

    public void Update(float dt)
    {
        timer.OnUpdate(dt);
        if (timer.Count() == 0 && skillQueue.Count > 0)
        {
            //��һ��ʹ�õļ���
            var next = skillQueue.Dequeue();
            if (next.targetList != null)
            {
                UseSkill(next.skill, next.targetList, next.callback); //ʹ�ü���
            }
        }
    }

    //�Ƿ�������һ������
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

    //��ռ���
    public void Clear()
    {
        timer.Break();
        skillQueue.Clear();
    }
}
