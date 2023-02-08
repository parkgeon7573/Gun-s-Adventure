using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateManager : MonoSingleton<UpdateManager>
{
    List<IUpdateableObject> _updateableObjectList = new List<IUpdateableObject>();

    void Update()
    {
        for (int i = 0; i < Instance._updateableObjectList.Count; ++i)
        {
            Instance._updateableObjectList[i].OnUpdate();
        }

    }

    public void RegisterUpdateablObject(IUpdateableObject obj)
    {
        if (!Instance._updateableObjectList.Contains(obj))
        {
            Instance._updateableObjectList.Add(obj);
        }
    }

    public void DeregisterUpdateableObject(IUpdateableObject obj)
    {
        if (Instance._updateableObjectList.Contains(obj))
        {
            Instance._updateableObjectList.Remove(obj);
        }
    }
}