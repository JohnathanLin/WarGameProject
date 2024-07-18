using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ����������
/// </summary>
public class SoundManager
{
    private AudioSource bgmSource;//����bgm����Ƶ���

    private Dictionary<string, AudioClip> clips; //��Ƶ�����ֵ�

    private bool isStop;

    public bool IsStop
    {
        get { return isStop; }
        set {
            isStop = value;
            if (isStop)
            {
                bgmSource.Pause();
            }
            else 
            { 
                bgmSource.Play();
            }
        }
    }

    private float bgmVolume;
    public float BgmVolume
    {
        get { return bgmVolume; }
        set
        {
            bgmVolume = value;
            bgmSource.volume = bgmVolume;
        }
    }

    private float effectVolume;
    public float EffectVolume
    {
        get { return effectVolume; }
        set
        {
            effectVolume = value;
        }
    }
    public SoundManager()
    {
        clips = new Dictionary<string, AudioClip>();
        bgmSource = GameObject.Find("game").GetComponent<AudioSource>();

        isStop = false;
        bgmVolume = 1;
        effectVolume = 1;
    }

    //����bgm
    public void PlayBGM(string res)
    {
        if (isStop == true)
        {
            return;
        }

        //û�е�ǰ�ֵ�
        if (clips.ContainsKey(res) == false) 
        {
            //������Ƶ
            AudioClip clip = Resources.Load<AudioClip>($"Sounds/{res}");
            clips.Add(res, clip );
            bgmSource.clip = clips[res];
            bgmSource.Play(); //����
        }
    }

    public void PlayEffect(string name, Vector3 pos)
    {
        if (isStop == true)
        {
            return;
        }

        AudioClip clip = null;
        if (clips.ContainsKey(name) == false)
        {
            clip = Resources.Load<AudioClip>($"Sounds/{name}");
            clips.Add(name, clip);
        }
        AudioSource.PlayClipAtPoint(clips[name], pos);
    }
}
