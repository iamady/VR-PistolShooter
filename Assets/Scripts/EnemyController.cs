using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public SimpleShoot gun;
    public float bulletTime;
    Collider[] AllColliders;
    Animator animator;
    bool calledShoot;
    // Start is called before the first frame update
    void Awake()
    {
        // AllColliders = GetComponentsInChildren<Collider>(true);
        Dead(false);
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        transform.forward = Vector3.ProjectOnPlane((Camera.main.transform.position - transform.position), Vector3.up).normalized;
        if(!calledShoot)
        {
            Invoke("Shoot", 5f);
            calledShoot = true;
        }
        // transform.LookAt(Player.GetComponent<Transform>(), Vector3.up);
        //StartCoroutine(TrackPlayer());
        // Player = GameObject.FindGameObjectWithTag("Player");
    }

    public void Shoot()
    {
        gun.barrelLocation.forward = transform.forward.normalized;
        var dist = Vector3.Distance(gun.barrelLocation.position, GetTarget().normalized);
        var tvx = PlayerManager.instance.movementSpeed * bulletTime * Time.fixedDeltaTime;
        var shotPower = (dist + (tvx)) / Time.fixedDeltaTime;
        gun.shotPower = shotPower;
        animator.SetBool("detected", true);
        gun.Shoot();
    }

    void Dead(bool isDead)
    {
        // foreach(var coll in AllColliders)
        //     coll.enabled = isDead;
        // GetComponent<Rigidbody>().useGravity = !isDead;
        // GetComponent<Animator>().enabled = !isDead;
    }

    Vector3 GetTarget()
    {
        var target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;
        return target;
    }
    
}
