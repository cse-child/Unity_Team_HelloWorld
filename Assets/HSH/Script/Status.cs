using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    [Header("Walk, Run Speed")]
    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    private float runSpeed;
    //�ȴ� �ӵ�, �ٴ� �ӵ� ���� ����
    public float WalkSpeed => walkSpeed;
    public float RunSpeed => runSpeed;
    //�ܺο��� ���� Ȯ���ϴ� �뵵�� GetProfert?
}
