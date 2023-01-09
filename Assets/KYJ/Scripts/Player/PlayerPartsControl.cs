using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerPartsControl : MonoBehaviour
{
    public GameObject armorSocket;
    public GameObject weaponSocket;

    private PlayerControl playerControl;

    private Dictionary<string, string> useIndices = new Dictionary<string, string>()
    {
        {"hat", "0" },
        {"top", "0" },
        {"pants", "0" },
        {"shoes", "0" },
        {"weapon", "1" }
    };

    [SerializeField] private string hat, top, pants, shoes, weapon;

    private void Awake()
    {
        playerControl = GetComponent<PlayerControl>();
    }

    // Test Function
    private void Update()
    {
        // Inspector 확인용 변수 초기화
        UpdateInspector();

        //if (Input.GetKeyDown(KeyCode.F1))
        //{
        //    UnEquippedArmor("hat");
        //    UnEquippedArmor("top");
        //    UnEquippedArmor("pants");
        //    UnEquippedArmor("shoes");
        //    UnEquippedWeapon();
        //}
        //else if(Input.GetKeyDown(KeyCode.F2))
        //{
        //    EquippedArmor("hat", "1");
        //    EquippedArmor("top", "1");
        //    EquippedArmor("pants", "1");
        //    EquippedArmor("shoes", "1");
        //    EquippedWeapon("1");
        //}
        //else if (Input.GetKeyDown(KeyCode.F3))
        //{
        //    EquippedArmor("hat", "2");
        //    EquippedArmor("top", "2");
        //    EquippedArmor("pants", "2");
        //    EquippedArmor("shoes", "2");
        //    EquippedWeapon("2");
        //}
        //else if (Input.GetKeyDown(KeyCode.F4))
        //{
        //    EquippedArmor("hat", "3");
        //    EquippedArmor("top", "3");
        //    EquippedArmor("pants", "3");
        //    EquippedArmor("shoes", "3");
        //    EquippedWeapon("3");
        //}
    }

    private void UpdateInspector()
    {
        hat = useIndices["hat"];
        top = useIndices["top"];
        pants = useIndices["pants"];
        shoes = useIndices["shoes"];
        weapon = useIndices["weapon"];
    }

    /* 방어구 장착 */
    public void EquippedArmor(string part, string index)
    {
        // 장착중인 Part 비활성화
        armorSocket.transform.Find(part).Find(useIndices[part]).gameObject.SetActive(false);
        
        // 장착하려는 Part 활성화
        armorSocket.transform.Find(part).Find(index).gameObject.SetActive(true);
        useIndices[part] = index;
    }

    /* 방어구 장착해제 */
    public void UnEquippedArmor(string part)
    {
        // 장착중인 Part 비활성화
        armorSocket.transform.Find(part).Find(useIndices[part]).gameObject.SetActive(false);

        // 장착 해제한 모델링 적용
        armorSocket.transform.Find(part).Find("0").gameObject.SetActive(true);
        useIndices[part] = "0";
    }

    /* 무기 장착 */
    public void EquippedWeapon(string index)
    {
        // 장착중인 무기 비활성화
        weaponSocket.transform.Find(useIndices["weapon"]).gameObject.SetActive(false);

        // 장착하려는 무기 활성화
        weaponSocket.transform.Find(index).gameObject.SetActive(true);
        useIndices["weapon"] = index;
    }

    /* 무기 장착해제 */
    public void UnEquippedWeapon()
    {
        // 장착중인 무기 비활성화
        weaponSocket.transform.Find(useIndices["weapon"]).gameObject.SetActive(false);

        // 장착 해제 인덱스 적용
        weaponSocket.transform.Find("0").gameObject.SetActive(true);
        useIndices["weapon"] = "0";
        playerControl.SetWeaponState(0);
    }
}
