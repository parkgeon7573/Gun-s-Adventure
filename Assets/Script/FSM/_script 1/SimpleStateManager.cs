//==================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//==================================================================
public class SimpleStateManager : FSM<SimpleStateManager>
{
    public float _messageShowTime = 2f;
    
    float _curTime = 0f;
    public bool _IsTimePass {  get { return _curTime > _messageShowTime; } }
    public void ResetTimer() { _curTime = 0f; }
    public void SetTimePass() { _curTime += Time.deltaTime; }

    void Start() { InitState(this, SSInit.Instance); }

    void Update() { FSMUpdate(); }

}// public class SampleStateManager : FSM<SampleStateManager>
//==================================================================;