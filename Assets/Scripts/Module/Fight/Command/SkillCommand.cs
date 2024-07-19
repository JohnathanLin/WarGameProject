using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����ָ��
/// </summary>
public class SkillCommand : BaseCommand
{
    ISkill skill;
    public SkillCommand(ModelBase model):base(model)
    {
         skill = model as ISkill;
    }

    public override void Do()
    {
        base.Do();
        List<ModelBase> resultList = skill.GetTarget();
        if (resultList.Count > 0)
        {
            //��Ŀ��
            GameApp.SkillManager.AddSkill(skill, resultList);//��������ӵ����ܹ�����

        }

    }

    public override bool Update(float dt)
    {
        if (GameApp.SkillManager.IsRunningSkill() == false)
        {
            model.IsStop = true;
            return true;
        } else
        {
            return false;
        }
    }
}
