using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageCenter
{
    private Dictionary<string, System.Action<object>> msgDic; //存储普通的消息字典
    private Dictionary<string, System.Action<object>> tmpMsgDic; //存储临时的消息字典
    private Dictionary<System.Object, Dictionary<string, System.Action<object>>> objMsgDic; //存储特定对象的消息

    public MessageCenter()
    {
        msgDic = new Dictionary<string, System.Action<object>>();
        tmpMsgDic = new Dictionary<string, System.Action<object>>();
        objMsgDic = new Dictionary<System.Object, Dictionary<string, System.Action<object>>>();
    }

    public void AddEvent(string eventName, System.Action<object> callback)
    {
        if (msgDic.ContainsKey(eventName))
        {
            msgDic[eventName] += callback;
        } else
        {
            msgDic.Add(eventName, callback);
        }
    }

    public void RemoveEvent(string eventName, System.Action<object> callback)
    {
        if (msgDic.ContainsKey(eventName))
        {
            msgDic[eventName] -= callback;
            if (msgDic[eventName] == null)
            {
                msgDic.Remove(eventName);
            }
        }
    }

    public void PostEvent(string eventName, object arg = null)
    {
        if (msgDic.ContainsKey(eventName))
        {
            msgDic[eventName].Invoke(arg);
        }
    }

    public void AddEvent(System.Object listenerObj, string eventName, System.Action<object> callback)
    {
        if (objMsgDic.ContainsKey(listenerObj))
        {
            if (objMsgDic[listenerObj].ContainsKey(eventName))
            {
                objMsgDic[listenerObj][eventName] += callback;
            }
            else
            {
                objMsgDic[listenerObj].Add(eventName, callback);
            }
        }
        else
        {
            Dictionary<string, System.Action<object>> _tempDic = new Dictionary<string, System.Action<object>>();
            _tempDic.Add(eventName, callback);
            objMsgDic[listenerObj] = _tempDic;
        }
    }

    public void RemoveEvent(System.Object listenerObj, string eventName, System.Action<object> callback)
    {
        if (objMsgDic.ContainsKey(listenerObj))
        {
            if (objMsgDic[listenerObj].ContainsKey(eventName))
            {
                objMsgDic[listenerObj][eventName] -= callback;
                if (objMsgDic[listenerObj][eventName] == null)
                {
                    objMsgDic[listenerObj].Remove(eventName);
                    if (objMsgDic[listenerObj].Count == 0)
                    {
                        objMsgDic.Remove(listenerObj);
                    }
                }
            }
        }
    }

    public void RemoveObjAllEvent(System.Object listenerObj)
    {
        if (objMsgDic.ContainsKey(listenerObj))
        {
            objMsgDic.Remove(listenerObj);
        }
    }

    public void PostEvent(System.Object listenerObj, string eventName, System.Object arg = null)
    {
        if (objMsgDic.ContainsKey(listenerObj))
        {
            if (objMsgDic[listenerObj].ContainsKey(eventName))
            {
                objMsgDic[listenerObj][eventName].Invoke(arg);
            }
        }
    }

    public void AddTmpEvent(string eventName, System.Action<object> callback)
    {
        if (tmpMsgDic.ContainsKey(eventName))
        {
            tmpMsgDic[eventName] = callback;
        } else
        {
            tmpMsgDic.Add(eventName, callback);
        }
    }

    public void PostTmpEvent(string eventName, System.Object arg = null)
    {
        if (tmpMsgDic.ContainsKey(eventName))
        {
            tmpMsgDic[eventName].Invoke(arg);
            tmpMsgDic[eventName] = null;
            tmpMsgDic.Remove(eventName);
        }
    }

}
