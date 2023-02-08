using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

    }
    public void MoveScene()
    {
        SceneManager.LoadScene("Game");
    }
    public override void Clear()
    {
        Debug.Log("LoginScene Clear!");
    }
}
