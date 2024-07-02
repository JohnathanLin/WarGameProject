using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseView : MonoBehaviour, IBaseView
{
    public int ViewId { get; set; }
    public BaseController Controller { get ; set ; }

    protected Canvas _canvas;

    protected Dictionary<string, GameObject> m_cache_gos = new Dictionary<string, GameObject>(); //����������ֵ�

    private bool _isInit = false; //�Ƿ��ʼ��

    void Awake()
    {
        _canvas = gameObject.GetComponent<Canvas>();
        OnAwake();
    }
    // Start is called before the first frame update
    void Start()
    {
        OnStart();
    }

    protected virtual void OnAwake()
    {

    }

    protected virtual void OnStart()
    {

    }


    public void ApplyControllerFunc(int controllerKey, string eventName, params object[] args)
    {
        this.Controller.ApplyControllerFunc(controllerKey, eventName, args);
    }

    public void ApplyFunc(string eventName, params object[] args)
    {
        this.Controller.ApplyFunc(eventName, args);
    }

    public virtual void Close(params object[] args)
    {
        SetVisible(false);//����
    }

    public void DestroyView()
    {
        Controller = null;
        Destroy(gameObject);
    }

    public virtual void InitData()
    {
        _isInit = true;
    }

    public virtual void InitUI()
    {
        
    }

    public bool IsInit()
    {
        return _isInit;
    }

    public bool IsShow()
    {
        return _canvas.enabled == true;
    }

    public virtual void Open(params object[] args)
    {
        
    }

    public void SetVisible(bool visible)
    {
        this._canvas.enabled = visible;
    }

    public GameObject Find(string res)
    {
        if (m_cache_gos.ContainsKey(res))
        {
            return m_cache_gos[res];
        }
        m_cache_gos.Add(res, transform.Find(res).gameObject);
        return m_cache_gos[res];
    }

    public T Find<T>(string res) where T : Component
    {
        GameObject obj = Find(res);
        return obj.GetComponent<T>();
    }

}
