using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �Զ�ɾ������
/// </summary>
public class DestroyObj : MonoBehaviour
{
    public float timer;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, timer);
    }
}
