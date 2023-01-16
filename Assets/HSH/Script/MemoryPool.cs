using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryPool : MonoBehaviour
{
    //오브젝트를 Destroy 하지않고 SetActive false로 관리하는 ㅎ마수

    private class PoolItem
    {
        public bool isActive; // "GameObject"의 활성화 정보
        public GameObject gameObject; // 화면에 보이는 게임오브젝트
    }

    private int increaseCount = 5; // 오브젝트가 부족할 때 Instantiate()로 추가 생성되는 오브젝트의 개수
    private int maxCount; // 현재 리스트에 등록되어 있는 오브젝트 개수
    private int activeCount; // 현재 게임에 사용되고있는 활성화 오브젝트 개수
    public int regenCount;

    private GameObject poolObject; // 오브젝트 풀링에서 관리하는 게임오브젝트 프리펩
    private List<PoolItem> poolItemList; //관리되는 모든 오브젝트를 저장하는 리스트

    public int outMaxCount => maxCount; //외부에서 현재 리스트에 등록되어 있는 오브젝트 개수 확인을 위한 패러미터
    public int outActiveCount => activeCount; //외부에서 현재 활성화 되어있는 오브젝트 개수 확인을 위한 패러미터

    public MemoryPool(GameObject poolObject)
    {
        // 변수 초기화 및 최초 오브젝트 n개 생성
        maxCount = 0;
        activeCount = 0;
        this.poolObject = poolObject;

        poolItemList = new List<PoolItem>();

        InstantiateObjects();
    }

    //increaseCount 단위로 오브젝트를 생성

    public void InstantiateObjects()
    {
        //오브젝트 생성 시 active = false로 설정
        maxCount += increaseCount;

        for(int i= 0; i < increaseCount; ++i)
        {
            PoolItem poolItem = new PoolItem();

            poolItem.isActive = false;
            poolItem.gameObject = GameObject.Instantiate(poolObject); 
            poolItem.gameObject.SetActive(false);

            poolItemList.Add(poolItem);
        }
    }
    public void DestroyObjects()
    {
        //Destroy() 메소드를 이용해 오브젝트 삭제 , 씬전환 & 게임종료될때 한번만 수행 모든 게임 오브젝트 삭제
        if (poolItemList == null) return;

        int count = poolItemList.Count;

        for(int i =0; i < count; ++i)
        {
            GameObject.Destroy(poolItemList[i].gameObject);
        }

        poolItemList.Clear();
    }

    public GameObject ActivatePoolItem()
    {
        //poolItemList에 저장되어있는 오브젝트를 활성화 해서 사용
        if(poolItemList == null) return null; //리스트가 비어있으면 return

        if(maxCount == activeCount) // 모든오브젝트 활성화 중일때, 추가 오브젝트 생성
        {
            InstantiateObjects();
        }

        // 리스트 탐색하여 비활성화 오브젝트 활성화 후 오브젝트 반환
        int count = poolItemList.Count; 
        for(int i = 0; i < count; ++i)
        {
            PoolItem poolItem = poolItemList[i];
            
            if(!poolItem.isActive)
            {
                activeCount++;
                poolItem.isActive = true;
                poolItem.gameObject.SetActive(true);

                return poolItem.gameObject;
            }
        }
        return null;
    }
    public GameObject ActivateLimitePoolItem()
    {
        //poolItemList에 저장되어있는 오브젝트를 활성화 해서 사용
        if (poolItemList == null) return null; //리스트가 비어있으면 return

        // 리스트 탐색하여 비활성화 오브젝트 활성화 후 오브젝트 반환
        int count = poolItemList.Count;
        for (int i = 0; i < count; ++i)
        {
            PoolItem poolItem = poolItemList[i];

            if (!poolItem.isActive)
            {
                activeCount++;
                poolItem.isActive = true;
                poolItem.gameObject.SetActive(true);

                return poolItem.gameObject;
            }
        }
        return null;
    }

    public void DeactivatePoolItem(GameObject removeObject)
    {
        //현재 활성화된 오브젝트를 비활성화 시키는 함수
        if(poolItemList == null || removeObject == null) return;

        int count = poolItemList.Count;
        for(int i = 0; i < count; ++i)
        {
            PoolItem poolItem = poolItemList[i];

            if(poolItem.gameObject == removeObject)
            {
                activeCount--;

                poolItem.isActive = false;
                poolItem.gameObject.SetActive(false);

                return;
            }
        }
    }

    public void DeactivateAllPoolItem()
    {
        //현재 활성화된 모든 오브젝트를 비활성화 시키는 함수
        if (poolItemList == null) return;

        int count = poolItemList.Count;
        for (int i = 0; i < count; ++i)
        {
            PoolItem poolItem = poolItemList[i];

            if (poolItem.gameObject != null && poolItem.isActive)
            {
                poolItem.isActive = false;
                poolItem.gameObject.SetActive(false);
            }
        }

        activeCount = 0;
    }

    public void AllActivateFalse()
    {
        if (poolItemList == null) return;

        // 리스트 탐색하여 비활성화 오브젝트 활성화 후 오브젝트 반환
        int count = poolItemList.Count;
        for (int i = 0; i < count; ++i)
        {
            PoolItem poolItem = poolItemList[i];

            if (!poolItem.isActive)
            {
                activeCount++;
            }
        }
    }
    public float FalseCheck()
    {
        if (poolItemList == null) return 0;

        int count = poolItemList.Count;
        for (int i = 0; i < count; ++i)
        {
            PoolItem poolItem = poolItemList[i];

            if (!poolItem.isActive)
            {
                regenCount++;
            }
        }
        return regenCount;
    }
}
