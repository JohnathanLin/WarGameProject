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

    public SoundManager()
    {
        clips = new Dictionary<string, AudioClip>();
        bgmSource = GameObject.Find("game").GetComponent<AudioSource>();
    }

    //����bgm
    public void PlayBGM(string res)
    {
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
}
