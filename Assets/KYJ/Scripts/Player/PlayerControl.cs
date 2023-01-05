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
    private int curSkillState;

    private bool isDead = false;

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
        
        //Looting();
        Attack();
        EquippedWeapon();
    }

    // Player ������ -> HP ����
    public void TakeDamage(float value)
    {
        animator.SetTrigger(hashDamage);
        playerState.DecreaseHp(value);

        // �׾���?
        if(playerState.curHp <= 0)
        {
            playerState.curHp = 0;
            animator.SetBool(hashDeath, true);
            isDead = true;
        }
    }

    // ���콺 ���� ��ư - ����
    private void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!starterAssetsInputs.cursorLocked) return; // ���콺 Ȱ��ȭ ���¸� ���� �Ұ�

            animator.SetTrigger(hashAttack);
        }
    }

    // ������ �ݱ�
    public void Looting()
    {
        animator.SetTrigger(hashLooting);
    }

    // ���� ����/����
    private void EquippedWeapon()
    {
        curWeaponState = animator.GetInteger("WeaponState");

        // ���� ����/����/����
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!CheckWeaponExist())
            {
                print("# ������ ���Ⱑ ���� ���⸦ �� �� �����ϴ� !");
                return;
            }

            if (curWeaponState == MAX_WEAPON_COUNT)
                animator.SetInteger("WeaponState", 0); // �Ǽ�
            else
                animator.SetInteger("WeaponState", ++curWeaponState); // ��������

            // ���⸦ �ٲٸ� ������ �Էµ� ����(Trigger, Integer) �ʱ�ȭ
            animator.ResetTrigger(hashAttack);
            animator.ResetTrigger(hashDamage);
            ResetSkillState();
        }
    }

    // �ִϸ��̼� Event - ���� Prefab Active On/Off
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

    // ��ų �ִϸ��̼� �÷���
    public void PlaySkill(int skillNum)
    {
        curSkillState = skillNum;
        animator.SetInteger("SkillState", skillNum);
    }

    // ���� ���� ��ȯ (0: �Ǽ�, 1: ��հ�)
    public int WeaponState()
    {
        return curWeaponState;
    }

    public void SetWeaponState(int state)
    {
        curWeaponState = state;
        animator.SetInteger("WeaponState", state);
    }

    // ��ų ���� ��ȯ (0: ����X, 1~4: ��ų ������)
    public int SkillState()
    {
        return curSkillState;
    }

    // �ִϸ��̼� Event - ��ų ���� �ʱ�ȭ
    private void ResetSkillState()
    {
        curSkillState = 0;
        animator.SetInteger("SkillState", 0);
    }

    // �ִϸ��̼� Event - ��հ� ���� Collider Ȱ��ȭ
    private void OnAttackCollision()
    {
        //print("OnAttackCollision");
        attackCollision.SetActive(true);
    }

    // �ִϸ��̼� Event - Skill Effect Play �Լ�
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

    // ���� ���� ���� Ȯ�� �Լ�
    private bool CheckWeaponExist()
    {
        // Weapon Socket�� 0�� GameObject�� ���������� -> ���� ����
        // 0���� ���������� -> ���� ������
        return !weaponSocket.transform.Find("0").gameObject.activeSelf;
    }
}
