using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Enemy : ModelBase, ISkill
{
    public SkillProperty skillPro { get; set; }

    private Slider hpSlider;

    protected override void Start()
    {
        base.Start();

        hpSlider = transform.Find("hp/bg").GetComponent<Slider>();

        data = GameApp.ConfigManager.GetConfigData("enemy").GetDataById(Id);

        Type = int.Parse(this.data["Type"]);
        Attack = int.Parse(this.data["Attack"]);
        Step = int.Parse(this.data["Step"]);
        MaxHp = int.Parse(this.data["Hp"]);
        CurHp = MaxHp;

        skillPro = new SkillProperty(int.Parse(data["Skill"]));
    }

    //ѡ��
    protected override void OnSelectCallBack(System.Object args)
    {
        if (GameApp.CommandManager.IsRunningCommand)
        {
            return;
        }

        base.OnSelectCallBack(args);

        GameApp.ViewManager.Open(ViewType.EnemyDesView, this);
    }

    //δѡ��
    protected override void OnUnSelectCallBack(System.Object args)
    {
        base.OnUnSelectCallBack(args);


        GameApp.ViewManager.Close(ViewType.EnemyDesView);
    }

    public void ShowSkillArea()
    {

    }

    public void HideSkillArea()
    {

    }

    //����
    public override void GetHit(ISkill skill)
    {
        //����������Ч
        GameApp.SoundManager.PlayEffect("hit", transform.position);
        //��Ѫ
        CurHp -= skill.skillPro.Attack;
        //��ʾ�˺�����
        GameApp.ViewManager.ShowHitNum($"-{skill.skillPro.Attack}", Color.red, transform.position);
        //������Ч
        PlayEffect(skill.skillPro.AttackEffect);

        //�ж��Ƿ�����
        if (CurHp <= 0)
        {
            CurHp = 0;

            PlayAni("die");

            Destroy(gameObject, 1.2f);

            //�ӵ��˼������Ƴ�
            GameApp.FightWorldManager.RemoveEnemy(this);
        }

        StopAllCoroutines();
        StartCoroutine(ChangeColor());
        StartCoroutine(UpdateHpSlider()); 
    }

    private IEnumerator ChangeColor()
    {
        bodySp.material.SetFloat("_FlashAmount", 1);
        yield return new WaitForSeconds(0.25f);
        bodySp.material.SetFloat("_FlashAmount", 0);
    }

    private IEnumerator UpdateHpSlider()
    {
        hpSlider.gameObject.SetActive(true);
        hpSlider.DOValue((float)CurHp / (float)MaxHp, 0.25f);
        yield return new WaitForSeconds(0.75f);
        hpSlider.gameObject.SetActive(false);
    }
}
