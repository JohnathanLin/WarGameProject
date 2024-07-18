using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���ܰ�����
/// </summary>
public static class SkillHelper
{
    /// <summary>
    /// Ŀ���Ƿ��ڼ��ܵ�����Χ��
    /// </summary>
    /// <param name="skill"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    public static bool IsModelInSkillArea(this ISkill skill, ModelBase target)
    {
        ModelBase current = (ModelBase)skill;
        if (current.GetDis(target) <= skill.skillPro.AttackRange) 
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// ��ü��ܵ�����Ŀ��
    /// </summary>
    /// <param name="skill"></param>
    /// <returns></returns>
    public static List<ModelBase> GetTarget(this ISkill skill)
    {
        switch (skill.skillPro.Target)
        {
            //0:�����ָ���Ŀ��ΪĿ��
            case 0:
                return GetTarget_0(skill);

            //1:�ڹ�����Χ�ڵ�����Ŀ��
            case 1:
                return GetTarget_1(skill);

            //2.�ڹ�����Χ�ڵ�Ӣ�۵�Ŀ��
            case 2:
                return GetTarget_2(skill);
        }

        return null;
    }

    //0:�����ָ���Ŀ��ΪĿ��
    public static List<ModelBase> GetTarget_0(ISkill skill)
    {
        List<ModelBase> results = new List<ModelBase>();
        Collider2D col = Tools.ScreenPointToRay2D(Camera.main, Input.mousePosition);
        if (col != null)
        {
            ModelBase target = col.GetComponent<ModelBase>();
            if (target != null)
            {
                //���ܵ�Ŀ������ �� ����ָ���Ŀ������Ҫ�����ñ�һ��
                if (skill.skillPro.TargetType == target.Type) 
                {
                    results.Add(target);
                }
            }
        }

        return results;
    }

    //1:�ڹ�����Χ�ڵ�����Ŀ��

    public static List<ModelBase> GetTarget_1(ISkill skill)
    {
        List<ModelBase> results = new List<ModelBase>();
        for (int i = 0; i < GameApp.FightWorldManager.heroList.Count; i++) 
        {
            if (skill.IsModelInSkillArea(GameApp.FightWorldManager.heroList[i]))
            {
                results.Add(GameApp.FightWorldManager.heroList[i]);
            }
        }

        for (int i = 0; i < GameApp.FightWorldManager.enemyList.Count; i++)
        {
            if (skill.IsModelInSkillArea(GameApp.FightWorldManager.enemyList[i]))
            {
                results.Add(GameApp.FightWorldManager.enemyList[i]);
            }
        }

        return results;
    }

    //2.�ڹ�����Χ�ڵ�Ӣ�۵�Ŀ��
    public static List<ModelBase> GetTarget_2(ISkill skill)
    {
        List<ModelBase> results = new List<ModelBase>();
        for (int i = 0; i < GameApp.FightWorldManager.heroList.Count; i++)
        {
            if (skill.IsModelInSkillArea(GameApp.FightWorldManager.heroList[i]))
            {
                results.Add(GameApp.FightWorldManager.heroList[i]);
            }
        }

        return results;
    }
}
