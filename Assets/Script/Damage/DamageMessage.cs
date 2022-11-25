using UnityEngine;

public struct DamageMessage
{
    public GameObject damager; //공격 가한측
    public float amount;       //데미지 양 (공격력)

    public Vector3 hitPoint;   //공격 위치
    public Vector3 hitNormal;  //공격맞은 표면
}
