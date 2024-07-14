using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

//�򵥵�Tip����
public class TipView : BaseView
{
    public override void Open(params global::System.Object[] args)
    {
        base.Open(args);
        Find<Text>("content/txt").text = args[0].ToString();
        Sequence seq = DOTween.Sequence();
        seq.Append(Find("content").transform.DOScaleY(1, 0.15f)).SetEase(Ease.OutBack);
        seq.AppendInterval(0.75f);
        seq.Append(Find("content").transform.DOScaleY(0, 0.15f)).SetEase(Ease.Linear);
        seq.AppendCallback(delegate ()
        {
            GameApp.ViewManager.Close(ViewId);
        });
    }
}
