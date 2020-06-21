using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public SimpleShoot gun;
    Collider[] AllColliders;
    // Start is called before the first frame update
    void Awake()
    {
        // AllColliders = GetComponentsInChildren<Collider>(true);
        Dead(false);
    }

    // Update is called once per frame
    void Update()
    {

        transform.forward = Vector3.ProjectOnPlane((Camera.main.transform.position - transform.position), Vector3.up).normalized;
        // transform.LookAt(Player.GetComponent<Transform>(), Vector3.up);
        //StartCoroutine(TrackPlayer());
        // Player = GameObject.FindGameObjectWithTag("Player");
    }

    public void Shoot()
    {
        gun.Shoot();
    }

    void Dead(bool isDead)
    {
        // foreach(var coll in AllColliders)
        //     coll.enabled = isDead;
        // GetComponent<Rigidbody>().useGravity = !isDead;
        // GetComponent<Animator>().enabled = !isDead;
    }
    
}
