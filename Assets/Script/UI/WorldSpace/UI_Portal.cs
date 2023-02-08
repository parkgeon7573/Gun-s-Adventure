using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Portal : UI_Base
{
    enum GameObjects
    {
        Portal
    }

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
    }

    private void Update()
    {
        Transform parent = transform.parent;
        transform.position = parent.position + Vector3.up * 2f + Vector3.forward * 1f;
        transform.rotation = Camera.main.transform.rotation;
    }

}
