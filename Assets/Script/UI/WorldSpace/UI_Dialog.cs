using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Dialog : UI_Base
{
    enum GameObjects
    {
        Dialog
    }

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
    }

    private void Update()
    {
        Transform parent = transform.parent;
        transform.position = parent.position + Vector3.up * 3f + Vector3.forward * 1f;
        transform.rotation = Camera.main.transform.rotation;
    }
}