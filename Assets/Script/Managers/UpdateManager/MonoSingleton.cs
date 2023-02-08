using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ΩÃ±€≈Ê ∆–≈œ ≈€«√∏¥
/// </summary>
public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    protected static T instance = null;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType(typeof(T)) as T;

                if (instance == null)
                {
                    return null;
                }
            }
            return instance;
        }
    }
}
