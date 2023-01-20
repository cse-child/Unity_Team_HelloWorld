using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreathCannon : MonoBehaviour
{
    public float damage = 5.0f;
    private SphereCollider myCollider;
    private float range = 0.5f;
    RaycastHit hitInfo;
    public LayerMask layerMask;
    private PlayerControl playerControl;
    private PlayerState playerState;

    private GameObject target;
    private float moveSpeed = 10.0f;
    // Start is called before the first frame update
    private void Awake()
    {
        myCollider = GetComponent<SphereCollider>();
        playerControl = FindObjectOfType<PlayerControl>();
        playerState = FindObjectOfType<PlayerState>();
        target = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnEnable()
    {
        Vector3 direction = target.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(direction);
    }
    // Update is called once per frame
    private void Update()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime, Space.Self);
        Debug.DrawRay(myCollider.transform.position, transform.forward * range, Color.blue, 0.3f);
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, range, layerMask))
        {
            gameObject.SetActive(false);
            playerControl.TakeDamage(damage);
            print(playerState.curHp);
        }

        float time = 0.0f;
        time += Time.deltaTime;

        if (time >= 1)
        {
            gameObject.SetActive(false);
        }
    }
}
