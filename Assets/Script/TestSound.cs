using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSound : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Managers.Sound.Play("BGM/BGM", Define.Sound.Bgm);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
