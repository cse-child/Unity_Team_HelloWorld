using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryPool : MonoBehaviour
{
    //������Ʈ�� Destroy �����ʰ� SetActive false�� �����ϴ� ������

    private class PoolItem
    {
        public bool isActive; // "GameObject"�� Ȱ��ȭ ����
        public GameObject gameObject; // ȭ�鿡 ���̴� ���ӿ�����Ʈ
    }

    private int increaseCount = 5; // ������Ʈ�� ������ �� Instantiate()�� �߰� �����Ǵ� ������Ʈ�� ����
    private int maxCount; // ���� ����Ʈ�� ��ϵǾ� �ִ� ������Ʈ ����
    private int activeCount; // ���� ���ӿ� ���ǰ��ִ� Ȱ��ȭ ������Ʈ ����

    private GameObject poolObject; // ������Ʈ Ǯ������ �����ϴ� ���ӿ�����Ʈ ������
    private List<PoolItem> poolItemList; //�����Ǵ� ��� ������Ʈ�� �����ϴ� ����Ʈ

    public int outMaxCount => maxCount; //�ܺο��� ���� ����Ʈ�� ��ϵǾ� �ִ� ������Ʈ ���� Ȯ���� ���� �з�����
    public int outActiveCount => activeCount; //�ܺο��� ���� Ȱ��ȭ �Ǿ��ִ� ������Ʈ ���� Ȯ���� ���� �з�����

    public MemoryPool(GameObject poolObject)
    {
        // ���� �ʱ�ȭ �� ���� ������Ʈ n�� ����
        maxCount = 0;
        activeCount = 0;
        this.poolObject = poolObject;

        poolItemList = new List<PoolItem>();

        InstantiateObjects(); 
    }

    //increaseCount ������ ������Ʈ�� ����

    public void InstantiateObjects()
    {
        //������Ʈ ���� �� active = false�� ����
        maxCount += increaseCount;

        for(int i= 0; i < increaseCount; increaseCount++)
        {
            PoolItem poolItem = new PoolItem();

            poolItem.isActive = false;
            poolItem.gameObject = GameObject.Instantiate(poolObject); 
            poolItem.isActive = true;

            poolItemList.Add(poolItem);
        }
    }

    public void DestroyObjects()
    {
        //Destroy() �޼ҵ带 �̿��� ������Ʈ ���� , ����ȯ & ��������ɶ� �ѹ��� ���� ��� ���� ������Ʈ ����
        if (poolItemList == null) return;

        int count = poolItemList.Count;

        for(int i =0; i < count; i++)
        {
            GameObject.Destroy(poolItemList[i].gameObject);
        }

        poolItemList.Clear();
    }

    public GameObject ActivatePoolItem()
    {
        //poolItemList�� ����Ǿ��ִ� ������Ʈ�� Ȱ��ȭ �ؼ� ���
        if(poolItemList == null) return null; //����Ʈ�� ��������� return

        if(maxCount == activeCount) // ��������Ʈ Ȱ��ȭ ���϶�, �߰� ������Ʈ ����
        {
            InstantiateObjects();
        }

        // ����Ʈ Ž���Ͽ� ��Ȱ��ȭ ������Ʈ Ȱ��ȭ �� ������Ʈ ��ȯ
        int count = poolItemList.Count; 
        for(int i = 0; i < count; i++)
        {
            PoolItem poolItem = poolItemList[i];
            
            if(!poolItem.isActive)
            {
                activeCount++;
                poolItem.isActive = true;
                poolItem.gameObject.SetActive(false);

                return poolItem.gameObject;
            }
        }
        return null;
    }

    public void DeactivatePoolItem(GameObject removeObject)
    {
        //���� Ȱ��ȭ�� ������Ʈ�� ��Ȱ��ȭ ��Ű�� �Լ�
        if(poolItemList == null || removeObject == null) return;

        int count = poolItemList.Count;
        for(int i = 0; i < count; i++)
        {
            PoolItem poolItem = poolItemList[i];

            if(poolItem.gameObject == removeObject)
            {
                activeCount++;

                poolItem.isActive = false;
                poolItem.gameObject.SetActive(false);

                return;
            }
        }
    }

    public void DeactivateAllPooling()
    {
        //���� Ȱ��ȭ�� ��� ������Ʈ�� ��Ȱ��ȭ ��Ű�� �Լ�
        if (poolItemList == null) return;

        int count = poolItemList.Count;
        for (int i = 0; i < count; i++)
        {
            PoolItem poolItem = poolItemList[i];

            if (poolItem.gameObject != null && poolItem.isActive)
            {
                activeCount++;

                poolItem.isActive = false;
                poolItem.gameObject.SetActive(false);
            }
        }

        activeCount = 0;
    }

}
