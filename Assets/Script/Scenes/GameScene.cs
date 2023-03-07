using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    [SerializeField]
    CameraMove m_camera;
    [SerializeField]
    PlayerController m_player;
    [SerializeField]
    WaypointController m_wayCtr;
    [SerializeField]
    GameObject m_loadingObj;
    protected override void Init()
    {
        base.Init();
        m_camera.transform.position = m_player.transform.position;
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
    public void CreateMonster()
    {
        StartCoroutine(DelaycreateMonster());
    }
    IEnumerator DelaycreateMonster()
    {
        yield return new WaitForSeconds(3f);
        Managers.Resource.Instantiate("Charactor/Monster", m_wayCtr.transform);
    }
    private void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            CreateMonster();
        }
    }

    // Start is called before the first frame update

}
