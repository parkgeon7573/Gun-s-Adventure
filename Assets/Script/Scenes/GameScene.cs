using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    [SerializeField]
    GameObject m_loadingObj;
    protected override void Init()
    {
        base.Init();

        m_loadingObj.SetActive(false);
        //Managers.UI.ShowPopupUI<UI_Inven>();

    }
    public override AsyncOperation GoNextScene(string name)
    {
        return base.GoNextScene(name);
    }

    public override void Clear()
    {

    }

   

    // Start is called before the first frame update

}
