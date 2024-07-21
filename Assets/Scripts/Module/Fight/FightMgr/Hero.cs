using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Ӣ�۽ű�
public class Hero : ModelBase, ISkill
{
    public SkillProperty skillPro { get; set; }

    private Slider hpSlider;

    protected override void Start()
    {
        base.Start();

        hpSlider = transform.Find("hp/bg").GetComponent<Slider>();
    }

    public void Init(Dictionary<string, string> data, int row, int col)
    {
        this.data = data;
        this.RowIndex = row;
        this.ColIndex = col;
        Id = int.Parse(this.data["Id"]);
        Type = int.Parse(this.data["Type"]);
        Attack = int.Parse(this.data["Attack"]);
        Step = int.Parse(this.data["Step"]);
        MaxHp = int.Parse(this.data["Hp"]);
        CurHp = MaxHp;

        skillPro = new SkillProperty(int.Parse(this.data["Skill"]));
    }

    //ѡ��
    protected override void OnSelectCallBack(System.Object args)
    {
        //��һغ� ����ѡ�н�ɫ
        if (GameApp.FightWorldManager.state == GameState.Player)
        {
            //���ܲ���
            if (IsStop) 
            {
                return;
            }

            if (GameApp.CommandManager.IsRunningCommand)
            {
                return;
            }

            //ִ��δѡ��
            GameApp.MessageCenter.PostEvent(Defines.OnUnSelectEvent);

            if (IsStop == false) 
            {
                //��ʾ·��
                GameApp.MapManager.ShowStepGrid(this, Step);

                //�����ʾ·��ָ��
                GameApp.CommandManager.AddCommand(new ShowPathCommand(this));

                //���ѡ���¼�
                addOptionEvents();
            }

            GameApp.ViewManager.Open(ViewType.HeroDesView, this);
        }
    }

    private void addOptionEvents()
    {
        GameApp.MessageCenter.AddTmpEvent(Defines.OnAttackEvent, onAttackCallBack);
        GameApp.MessageCenter.AddTmpEvent(Defines.OnIdleEvent, onIdleCallBack);
        GameApp.MessageCenter.AddTmpEvent(Defines.OnCancelEvent, onCancelCallBack);
    }

    private void onAttackCallBack(System.Object arg)
    {
        GameApp.CommandManager.AddCommand(new ShowSkillAreaCommand(this));
    }

    private void onIdleCallBack(System.Object arg)
    {
        IsStop = true;
        Debug.Log("idle");
    }

    private void onCancelCallBack(System.Object arg)
    {
        GameApp.CommandManager.UnDo();
    }

    //δѡ��
    protected override void OnUnSelectCallBack(System.Object args)
    {
        base.OnUnSelectCallBack(args);
        GameApp.ViewManager.Close(ViewType.HeroDesView);
    }

    //��ʾ��������
    public void ShowSkillArea()
    {
        GameApp.MapManager.ShowAttackStep(this, skillPro.AttackRange, Color.red);
    }   

    //���ؼ��ܹ�������
    public void HideSkillArea()
    {
        GameApp.MapManager.HideAttackStep(this, skillPro.AttackRange);
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

            //��Ӣ�ۼ������Ƴ�
            GameApp.FightWorldManager.RemoveHero(this);
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
