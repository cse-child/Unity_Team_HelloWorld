using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFarming : MonoBehaviour
{
    private GameObject target;
    private float farmRange = 1.0f;
    private bool isFarming = false;


    private void OnEnable()
    {
        TimeOverFarm();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, farmRange);
    }
    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        Farming();
    }

    private void Farming()
    {
        float distance = Vector3.Distance(transform.position, target.transform.position);
        if(distance < farmRange)
        {
            isFarming = true;
        }
        else
        {
            isFarming = false;
        }
        if (isFarming)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                int randPotion = Random.Range(1, 4);
                int randMaterial = Random.Range(1, 4);
                ItemLootManager.instance.AddLootItem(1, randPotion);

                int randItemDrob = Random.Range(0, 9);
                int randEquipment = Random.Range(7, 22);
                int randPotionDrop = Random.Range(1, 7);
                switch (randItemDrob)
                {
                    case 0:
                    case 1:
                    case 2:
                        ItemLootManager.instance.AddLootItem(22, randMaterial);
                        break;
                    case 3:
                        ItemLootManager.instance.AddLootItem(randEquipment, 1);
                        break;
                    case 4:
                    case 5:
                    case 6:
                    case 7:
                        ItemLootManager.instance.AddLootItem(randPotionDrop, randPotion);
                        break;
                    case 8:
                    case 9:
                        break;
                }
                ItemLootManager.instance.OpenLootingUI();
                //PlayerControl.Looting();
                Destroy(gameObject);
            }
        }
    }

    private IEnumerator TimeOverFarm()
    {
        yield return new WaitForSeconds(3.0f);
        Destroy(gameObject);
    }
}
