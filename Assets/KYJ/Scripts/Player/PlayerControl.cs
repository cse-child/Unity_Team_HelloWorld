using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.UI;
using static PlayerManager;

public class PlayerControl : MonoBehaviour
{
    const int MAX_WEAPON_COUNT = 1;
    const float BLOOD_SCREEN_HP = 30.0f;

    public GameObject weaponSocket;
    public GameObject attackCollision;
    public TrailRenderer trailEffect;
    public Text warningText;

    private Animator animator;

    private PlayerState playerState;
    private StarterAssetsInputs starterAssetsInputs;
    private FadeEffect fadeEffect;
    private PlayerManager playerManager;

    private int curWeaponState;
    private int curSkillState;
    private bool isAttacking = false;

    public bool isDead = false;
    

    private readonly int hashDeath = Animator.StringToHash("Death");
    private readonly int hashLooting = Animator.StringToHash("Looting");
    private readonly int hashAttack = Animator.StringToHash("Attack");
    private readonly int hashDamage = Animator.StringToHash("Damage");
    private readonly int hashSpeed = Animator.StringToHash("Speed");


    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerState = GetComponent<PlayerState>();
        starterAssetsInputs = FindObjectOfType<StarterAssetsInputs>();
        fadeEffect = FindObjectOfType<FadeEffect>();
        playerManager = FindObjectOfType<PlayerManager>();
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
        CheckBloodScreen();

        //if (Input.GetKeyDown(KeyCode.F1))
        //    TakeDamage(20);
        //if(Input.GetKeyDown(KeyCode.F2))
        //    PlayerResponse(1);
        //playerState.IncreaseExp(30);
    }

    // Player ������ -> HP ����
    public void TakeDamage(float value)
    {
        if (isDead) return; // �׾����� �ȸ°�

        animator.SetTrigger(hashDamage);
        playerState.DecreaseHp(value);
        PlaySfxSound((int)PlayerManager.Sfx.DAMAGE);

        // �׾���?
        if (playerState.curHp <= 0)
        {
            playerState.curHp = 0;
            animator.SetBool(hashDeath, true);
            isDead = true;
            PlaySfxSound((int)PlayerManager.Sfx.DEATH);
        }
    }

    // ���콺 ���� ��ư - ����
    private void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (curWeaponState == 0) return; // ���⸦ �ȵ�������� ����X
            if (!starterAssetsInputs.cursorLocked) return; // ���콺 Ȱ��ȭ ���¸� ���� �Ұ�

            animator.SetTrigger(hashAttack);
            isAttacking = true;
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
                StartCoroutine(SetWarningText("������ ���Ⱑ ���� ���⸦ �� �� �����ϴ�"));
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

    // �÷��̾� �ǰ� ������ ��ũ�� �ͺ� ȿ��
    private void CheckBloodScreen()
    {
        if(playerState.curHp <= BLOOD_SCREEN_HP)
        {
            fadeEffect.OnFade(FadeState.FadeLoop);
        }
    }

    // �˱� ȿ�� �ѱ�
    private void StartTrailEffect()
    {
        trailEffect.enabled = true;
    }

    // �˱� ȿ�� ����
    private void FinishTrailEffect()
    {
        trailEffect.enabled = false;
    }

    // Animation Event - ȿ���� ���
    private void PlaySfxSound(int type)
    {
        playerManager.SfxPlay((PlayerManager.Sfx)type);
    }

    // ��� �����ð����ȸ� ����
    public IEnumerator SetWarningText(string text)
    {
        warningText.text = text;

        yield return new WaitForSeconds(3.0f);

        warningText.text = "";
    }

    // Animation Event
    private void EndAttack()
    {
        animator.ResetTrigger(hashAttack);
        isAttacking = false;
    }

    // �÷��̾ �����ִ����� ��ȯ�ϴ� �Լ�
    public bool IsPlayerStop()
    {
        if (animator.GetFloat(hashSpeed) <= 0.5f)
            return true; // �����ִ�.
        return false; // �����̰� �ִ�.
    }

    // �÷��̾ (�⺻)������������ ��ȯ�ϴ� �Լ�
    public bool IsPlayerAttacking()
    {
        return isAttacking;
    }

    // �÷��̾� ��� �� ������ (1: ��ú�Ȱ/2: �������� ��Ȱ)
    public void PlayerResponse(int menu)
    {
        if(menu == 2)
        {
            Vector3 town = new Vector3(144.346207f, 36.0278397f, -127.950394f);
            this.transform.position = town;
        }
        
        isDead = false;
        animator.SetBool(hashDeath, isDead);
        playerState.ResetState();
        fadeEffect.StopFade();
    }

}
