using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Game;

        //Managers.UI.ShowPopupUI<UI_Inven>();

    }

    public override void Clear()
    {

    }

   

    // Start is called before the first frame update

}
