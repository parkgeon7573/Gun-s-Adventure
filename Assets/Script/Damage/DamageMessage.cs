using UnityEngine;

public struct DamageMessage
{
    public GameObject damager; //���� ������
    public float amount;       //������ �� (���ݷ�)

    public Vector3 hitPoint;   //���� ��ġ
    public Vector3 hitNormal;  //���ݸ��� ǥ��
}
