using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDataManager : MonoBehaviour
{
    private static ItemDataManager _instance;
    public static ItemDataManager instance
    {
        get
        {
            if(_instance == null)
            {
                GameObject obj = new GameObject("ItemDataManager");
                _instance = obj.AddComponent<ItemDataManager>();
            }
            return _instance;
        }
    }

    public struct ItemData
    {
        public int itemNum;
        public string name;
        public string property;
        public float power;
        public Sprite image;
        public string explanation;
    }

    /*----------------------------����------------------------*/

    List<ItemData> items = new List<ItemData>();

    /*---------------------------------------------------------*/

    public void Awake()
    {
        LoadItemData();
    }

    private void LoadItemData()
    {
        string temp = System.IO.File.ReadAllText("Assets/LHJ/Resource/TextData/ItemTable.csv");
        temp = temp.Replace("\r\n", "\n");
        string[] row = temp.Split("\n");
        for(int i = 1; i<row.Length;i++)
        {
            string[] col = row[i].Split(",");

            ItemData tempData;
            tempData.itemNum = int.Parse(col[0]);
            tempData.name = col[1];
            tempData.property = col[2];
            tempData.power = float.Parse(col[3]);

            //����Ʈ ������ �̹����� ��������Ʈ�� ��ȯ  byte -> texture -> splite
            byte[] byteTexture = System.IO.File.ReadAllBytes(col[4]);
            Texture2D texture = new Texture2D(0, 0);
            texture.LoadImage(byteTexture);
            Rect rect = new Rect(0,0, texture.width, texture.height);
            tempData.image = Sprite.Create(texture, rect, new Vector2(0.5f, 0.5f));

            tempData.explanation = col[5];

            items.Add(tempData);
        }
    }

    public ItemData GetItemData(int itemNum)
    {
        return items[itemNum - 1];
    }

    public ItemData GetItemData(string name)
    {
        ItemData nullData;
        nullData.name = "����";
        nullData.itemNum = -1;
        nullData.property = "����";
        nullData.power = -1;
        nullData.image = null;
        nullData.explanation = "����";

        foreach(ItemData temp in items)
        {
            if(temp.name==name)
            {
                return temp;
            }
        }

        Debug.Log("������ �̸��� �߸���");
        return nullData;
    }

}
