using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ItemDataManager;
using static UnityEditor.Progress;

public class SkillDataManager : MonoBehaviour
{
    private static SkillDataManager _instance;
    public static SkillDataManager instance
    {
        get
        {
            if(_instance == null )
            {
                GameObject obj = new GameObject("SkillDataManager");
                _instance = obj.AddComponent<SkillDataManager>();
            }
            return _instance;
        }
    }

    public struct SkillData
    {
        public int skillNum;
        public string skillName;
        public int decreaseMP;
        public int coolTime;
        public int damage;
        public int buff;
        public bool learning;
        public Sprite image;
        public string explanation;
    }

    List<SkillData> skills = new List<SkillData>();

    public void LoadItemData()
    {
        string temp = System.IO.File.ReadAllText("Assets/KYJ/Resources/TextData/SkillDataTable.csv");
        temp = temp.Replace("\r\n", "\n");
        string[] row = temp.Split("\n");
        for (int i = 1; i < row.Length; i++)
        {
            string[] col = row[i].Split(",");

            SkillData tempData;
            tempData.skillNum = int.Parse(col[0]);
            tempData.skillName = col[1];
            tempData.decreaseMP = int.Parse(col[2]);
            tempData.coolTime = int.Parse(col[3]);
            tempData.damage = int.Parse(col[4]);
            tempData.buff = int.Parse(col[5]);
            tempData.learning = int.Parse(col[6]) == 1 ? true : false;

            // ���ߴ� �ҽ� �� �ܾ���� ��
            //����Ʈ ������ �̹����� ��������Ʈ�� ��ȯ  byte -> texture -> splite
            byte[] byteTexture = System.IO.File.ReadAllBytes(col[7]);
            Texture2D texture = new Texture2D(0, 0);
            texture.LoadImage(byteTexture);
            Rect rect = new Rect(0, 0, texture.width, texture.height);
            tempData.image = Sprite.Create(texture, rect, new Vector2(0.5f, 0.5f));

            tempData.explanation = col[8];

            skills.Add(tempData);
        }
    }

    public SkillData GetSkillData(int skillNum)
    {
        return skills[skillNum - 1];
    }
}
