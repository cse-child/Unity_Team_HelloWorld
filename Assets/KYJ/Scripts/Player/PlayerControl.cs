using StarterAssets;
using System.Collections;
using System.Collections.Generic;
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

    public bool isDead = false;

    private readonly int hashDeath = Animator.StringToHash("Death");
    private readonly int hashLooting = Animator.StringToHash("Looting");
    private readonly int hashAttack = Animator.StringToHash("Attack");
    private readonly int hashDamage = Animator.StringToHash("Damage");


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
    }

    // Player 데미지 -> HP 감소
    public void TakeDamage(float value)
    {
        if (isDead) return; // 죽었으면 안맞게

        animator.SetTrigger(hashDamage);
        playerState.DecreaseHp(value);
        PlaySfxSound((int)PlayerManager.Sfx.DAMAGE);

        // 죽었니?
        if (playerState.curHp <= 0)
        {
            playerState.curHp = 0;
            animator.SetBool(hashDeath, true);
            isDead = true;
            PlaySfxSound((int)PlayerManager.Sfx.DEATH);
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
            if (!CheckWeaponExist())
            {
                StartCoroutine(SetWarningText("장착된 무기가 없어 무기를 들 수 없습니다"));
                return;
            }

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

    // 스킬 애니메이션 플레이
    public void PlaySkill(int skillNum)
    {
        curSkillState = skillNum;
        animator.SetInteger("SkillState", skillNum);
    }

    // 무기 상태 반환 (0: 맨손, 1: 양손검)
    public int WeaponState()
    {
        return curWeaponState;
    }

    public void SetWeaponState(int state)
    {
        curWeaponState = state;
        animator.SetInteger("WeaponState", state);
    }

    // 스킬 상태 반환 (0: 실행X, 1~4: 스킬 실행중)
    public int SkillState()
    {
        return curSkillState;
    }

    // 애니메이션 Event - 스킬 상태 초기화
    private void ResetSkillState()
    {
        curSkillState = 0;
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

    // 무기 장착 여부 확인 함수
    private bool CheckWeaponExist()
    {
        // Weapon Socket의 0번 GameObject가 켜져있으면 -> 무기 없음
        // 0번이 꺼져있으면 -> 무기 장착중
        return !weaponSocket.transform.Find("0").gameObject.activeSelf;
    }

    // 플레이어 피가 적을때 스크린 핏빛 효과
    private void CheckBloodScreen()
    {
        if(playerState.curHp <= BLOOD_SCREEN_HP)
        {
            fadeEffect.OnFade(FadeState.FadeLoop);
        }
    }

    // 검기 효과 켜기
    private void StartTrailEffect()
    {
        trailEffect.enabled = true;
    }

    // 검기 효과 끄기
    private void FinishTrailEffect()
    {
        trailEffect.enabled = false;
    }

    // Animation Event - 효과음 재생
    private void PlaySfxSound(int type)
    {
        playerManager.SfxPlay((PlayerManager.Sfx)type);
    }

    // 경고문 일정시간동안만 띄우기
    public IEnumerator SetWarningText(string text)
    {
        warningText.text = text;

        yield return new WaitForSeconds(3.0f);

        warningText.text = "";
    }
}
