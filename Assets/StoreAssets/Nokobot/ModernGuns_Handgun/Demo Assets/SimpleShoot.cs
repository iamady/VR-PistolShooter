using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SimpleShoot : MonoBehaviour
{

    public GameObject bulletPrefab;
    public GameObject casingPrefab;
    public GameObject muzzleFlashPrefab;
    public Transform barrelLocation;
    public Transform casingExitLocation;


    public float shotPower = 100f;

    // new variables
    public int maxAmmo = 12;
    private int currentAmmo;

    public GameObject bulletTrail;

    public AudioSource source;
    public AudioClip shotFired;
    public AudioClip gunReloaded;
    public AudioClip noAmmo;
    public TextMeshProUGUI bulletCount;
    public bool isEnemy;


    void Start()
    {
        if (barrelLocation == null)
            barrelLocation = transform;
    }

    void Update()
    {
        if (!isEnemy)
        {

            if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch) || Input.GetButtonDown("Fire1"))
            {
                if (currentAmmo > 0)
                    GetComponent<Animator>().SetTrigger("Fire");
                // else
                //     source.PlayOneShot(noAmmo);
            }

            if (Vector3.Angle(transform.up, Vector3.up) > 100 && currentAmmo < maxAmmo)
                Reload();

            bulletCount.text = currentAmmo.ToString();
        }
    }

    void Reload()
    {
        currentAmmo = maxAmmo;
        // source.Play(sho tFired);
    }

    public void Shoot()
    {
        //  GameObject bullet;
        //  bullet = Instantiate(bulletPrefab, barrelLocation.position, barrelLocation.rotation);
        // bullet.GetComponent<Rigidbody>().AddForce(barrelLocation.forward * shotPower);

        currentAmmo--;
        // source.PlayOneShot(shotFired);

        GameObject tempFlash;
        var bullet = Instantiate(bulletPrefab, barrelLocation.position, barrelLocation.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(barrelLocation.forward * shotPower);
        tempFlash = Instantiate(muzzleFlashPrefab, barrelLocation.position, barrelLocation.rotation);

        // Detect direction of shooting
        RaycastHit hitInfo;
        bool hasHit = Physics.Raycast(barrelLocation.position, barrelLocation.forward, out hitInfo, 100);

        if (bulletTrail)
        {
            GameObject line = Instantiate(bulletTrail);
            line.GetComponent<LineRenderer>().SetPositions(new Vector3[]{
                barrelLocation.position, hasHit ? hitInfo.point : barrelLocation.position + barrelLocation.forward * 100
                 });
        }

        //Scene object clean up
        Destroy(bullet, 5f);
        Destroy(tempFlash, 1f);
        //  Instantiate(casingPrefab, casingExitLocation.position, casingExitLocation.rotation).GetComponent<Rigidbody>().AddForce(casingExitLocation.right * 100f);

    }

    void CasingRelease()
    {
        GameObject casing;
        casing = Instantiate(casingPrefab, casingExitLocation.position, casingExitLocation.rotation) as GameObject;
        casing.GetComponent<Rigidbody>().AddExplosionForce(550f, (casingExitLocation.position - casingExitLocation.right * 0.3f - casingExitLocation.up * 0.6f), 1f);
        casing.GetComponent<Rigidbody>().AddTorque(new Vector3(0, Random.Range(100f, 500f), Random.Range(10f, 1000f)), ForceMode.Impulse);
        Destroy(casing, 5.0f);
    }


}
