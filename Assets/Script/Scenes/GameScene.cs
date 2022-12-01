using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    void Start()
    {
        Init();
    }
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Tutorial;

        Managers.UI.ShowPopupUI<UI_Inven>();
    }
    public override void Clear()
    {

    }

    // Start is called before the first frame update
   
}
