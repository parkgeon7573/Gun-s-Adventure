using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BossScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        Managers.Sound.Play("BGM/BGM", Define.Sound.Bgm);
        //Managers.UI.ShowPopupUI<UI_Inven>();

    }
    private void OnDisable()
    {
        Managers.Sound.Clear();
    }
    public override void Clear()
    {
        
    }



    // Start is called before the first frame update

}