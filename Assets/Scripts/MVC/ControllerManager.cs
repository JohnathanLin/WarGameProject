using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// ������������
/// </summary>
public class ControllerManager
{
    private Dictionary<int, BaseController> _modules;//�洢���������ֵ�
    public ControllerManager() {
        _modules = new Dictionary<int, BaseController>();
    }

    public void Register(ControllerType controllerType, BaseController controller)
    {
        Register((int)controllerType, controller);
    }

    //ע�������
    public void Register(int controllerKey, BaseController controller)
    {
        if (_modules.ContainsKey(controllerKey) == false)
        {
            _modules.Add(controllerKey, controller);
        }
    }

    //ִ�����п�������Init����
    public void InitAllModule()
    {
        foreach (var item in _modules)
        {
            item.Value.Init();
        }
    }

    //�Ƴ�������
    public void Unregister(int controllerKey)
    {
        if ( _modules.ContainsKey(controllerKey))
        {
            _modules.Remove(controllerKey);
        }
    }

    public void Clear()
    {
        _modules.Clear();
    }

    public void ClearAllModules()
    {
        List<int> keys = _modules.Keys.ToList();
        for (int i = 0;i < keys.Count; i++)
        {
            _modules[keys[i]].Destroy();
            _modules.Remove(keys[i]);
        }
    }

    //��ģ�崥����Ϣ
    public void ApplyFunc(int controllerKey, string eventName, System.Object[] args)
    {
        if (_modules.ContainsKey(controllerKey))
        {
            _modules[controllerKey].ApplyFunc(eventName, args);
        }
    }

    //��ȡĳ��������model����
    public BaseModel GetControllerModel(int controllerKey)
    {
        if (_modules.ContainsKey(controllerKey))
        {
            return _modules[controllerKey].GetModel();
        } 
        else
        {
            return null;
        }
    }
    
}
