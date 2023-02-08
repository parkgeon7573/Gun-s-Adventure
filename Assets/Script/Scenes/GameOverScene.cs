using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScene : BaseScene
{
    public override void Clear()
    {
        throw new System.NotImplementedException();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            GoNextScene("Game");
        }
    }
}
