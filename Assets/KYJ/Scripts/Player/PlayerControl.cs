using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    const int MAX_WEAPON_COUNT = 1;

    public GameObject weaponSocket;
    public GameObject attackCollision;

    private Animator animator;

    private PlayerState playerState;
    private StarterAssetsInputs starterAssetsInputs;

    private int curWeaponState;

    private readonly int hashDeath = Animator.StringToHash("Death");
    private readonly int hashLooting = Animator.StringToHash("Looting");
    private readonly int hashAttack = Animator.StringToHash("Attack");
    private readonly int hashDamage = Animator.StringToHash("Damage");


    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerState = GetComponent<PlayerState>();
        starterAssetsInputs = FindObjectOfType<StarterAssetsInputs>();
    }

    private void Start()
    {
        if(animator.GetInteger("WeaponState") == 0)
        {
            if(weaponSocket)
                weaponSocket.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
            Looting();
        Attack();
        EquippedWeapon();
    }

    // Player 데미지 -> HP 감소
    public void TakeDamage(float value)
    {
        animator.SetTrigger(hashDamage);
        playerState.DecreaseHp(value);

        // 죽었니?
        if(playerState.curHp <= 0)
        {
            animator.SetTrigger(hashDeath);
        }
    }

    // 마우스 왼쪽 버튼 - 공격
    private void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!starterAssetsInputs.cursorLocked) return; // 마우스 활성화 상태면 공격 불가
            animator.SetTrigger(hashAttack);
        }
    }

    // 아이템 줍기
    public void Looting()
    {
        animator.SetTrigger(hashLooting);
    }

    // 무기 장착/해제
    private void EquippedWeapon()
    {
        curWeaponState = animator.GetInteger("WeaponState");

        // 무기 장착/해제/변경
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (curWeaponState == MAX_WEAPON_COUNT)
                animator.SetInteger("WeaponState", 0); // 맨손
            else
                animator.SetInteger("WeaponState", ++curWeaponState); // 무기장착

            // 무기를 바꾸면 이전에 입력된 값들(Trigger, Integer) 초기화
            animator.ResetTrigger(hashAttack);
            animator.ResetTrigger(hashDamage);
            ResetSkillState();
        }
    }

    // 애니메이션 Event - 무기 Prefab Active On/Off
    private void ChangeWeaponActive()
    {
        if (weaponSocket)
        {
            if(weaponSocket.activeSelf)
                weaponSocket.SetActive(false);
            else
                weaponSocket.SetActive(true);
        }
    }

    public void PlaySkill(int skillNum)
    {
        animator.SetInteger("SkillState", skillNum);
    }

    public int WeaponState()
    {
        return curWeaponState;
    }

    // 애니메이션 Event - 스킬 상태 초기화
    private void ResetSkillState()
    {
        animator.SetInteger("SkillState", 0);
    }

    // 애니메이션 Event - 양손검 무기 Collider 활성화
    private void OnAttackCollision()
    {
        //print("OnAttackCollision");
        attackCollision.SetActive(true);
    }

    // 애니메이션 Event - Skill Effect Play 함수
    private void PlaySkillEffect(string key)
    {
        float offsetZ = 7;
        float offsetY = 1;
        Vector3 effectStartPos = transform.position;

        if (key == "Skill_1" || key == "Skill_2")
            effectStartPos += transform.up * offsetY;
        else if (key == "Skill_3")
            effectStartPos += transform.forward * offsetZ;

        ParticleManager.instance.Play(key, effectStartPos, transform.rotation);
    }
}
