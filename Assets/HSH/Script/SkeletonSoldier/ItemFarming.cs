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
                ItemLootManager.instance.AddLootItem(1, 3);
                ItemLootManager.instance.OpenLootingUI();
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
