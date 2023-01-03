using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPartsControl : MonoBehaviour
{
    public GameObject armorSocket;

    private Dictionary<string, string> useIndices = new Dictionary<string, string>()
    {
        {"hat", "0" },
        {"top", "0" },
        {"pants", "0" },
        {"shoes", "0" }
    };

    [SerializeField] private string hat, top, pants, shoes;

    // Test Function
    private void Update()
    {
        // Inspector Ȯ�ο� ���� �ʱ�ȭ
        UpdateInspector();

        if (Input.GetKeyDown(KeyCode.F1))
        {
            UnEquippedArmor("hat");
            UnEquippedArmor("top");
            UnEquippedArmor("pants");
            UnEquippedArmor("shoes");
        }
        else if(Input.GetKeyDown(KeyCode.F2))
        {
            EquippedArmor("hat", "1");
            EquippedArmor("top", "1");
            EquippedArmor("pants", "1");
            EquippedArmor("shoes", "1");
        }
        else if (Input.GetKeyDown(KeyCode.F3))
        {
            EquippedArmor("hat", "2");
            EquippedArmor("top", "2");
            EquippedArmor("pants", "2");
            EquippedArmor("shoes", "2");
        }
        else if (Input.GetKeyDown(KeyCode.F4))
        {
            EquippedArmor("hat", "3");
            EquippedArmor("top", "3");
            EquippedArmor("pants", "3");
            EquippedArmor("shoes", "3");
        }
    }

    private void UpdateInspector()
    {
        hat = useIndices["hat"];
        top = useIndices["hat"];
        pants = useIndices["hat"];
        shoes = useIndices["hat"];
    }

    /* ��ű� ���� */
    public void EquippedArmor(string part, string index)
    {
        // �������� Part ��Ȱ��ȭ
        armorSocket.transform.Find(part).Find(useIndices[part]).gameObject.SetActive(false);
        
        // �����Ϸ��� Part Ȱ��ȭ
        armorSocket.transform.Find(part).Find(index).gameObject.SetActive(true);
        useIndices[part] = index;
    }

    /* ��ű� �������� */
    public void UnEquippedArmor(string part)
    {
        // �������� Part ��Ȱ��ȭ
        armorSocket.transform.Find(part).Find(useIndices[part]).gameObject.SetActive(false);

        // ���� ������ �𵨸� ����
        armorSocket.transform.Find(part).Find("0").gameObject.SetActive(true);
        useIndices[part] = "0";
    }
}
