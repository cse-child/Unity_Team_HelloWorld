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
    //걷는 속도, 뛰는 속도 변수 선언
    public float WalkSpeed => walkSpeed;
    public float RunSpeed => runSpeed;
    //외부에서 값을 확인하는 용도의 GetProfert?
}
