using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//ѡ��
public class OptionItem : MonoBehaviour
{
    OptionData op_data;

    public void Init(OptionData data)
    {
        op_data = data; 
    }

    // Start is called before the first frame update
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(delegate()
        {
            GameApp.MessageCenter.PostTmpEvent(op_data.EventName); //ִ�����ñ������õ�Event�¼�
            GameApp.ViewManager.Close(ViewType.SelectOptionView); //�ر�ѡ�����
            
        });

        transform.Find("txt").GetComponent<Text>().text = op_data.Name;
    }

}
