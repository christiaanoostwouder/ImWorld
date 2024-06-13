using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public AudioSource weaponSound;
    public Camera playerCamera;

    public bool isShooting, readyToShoot;
    bool allowReset = true;
    public float shootingDelay = 2f;

    // Burst.
    public int bulletsPerBurst = 3;
    public int burstBulletsLeft;

    // Spread.
    public float spreadIntensity;

    // Bullet.
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletVelocity;
    public float bulletPrefabLifeTime;

    public enum ShootingMode
    {
        Single,
        Burst,
        Auto
    }

    public ShootingMode currentShootingMode;

    private void Awake()
    {
        readyToShoot = true;
        burstBulletsLeft = bulletsPerBurst;
    }


    void Update()
    {
        if(currentShootingMode == ShootingMode.Auto)
        {
            // Hold down.
            isShooting = Input.GetKey(KeyCode.Mouse0);
        }
        else if(currentShootingMode == ShootingMode.Single || currentShootingMode == ShootingMode.Burst)
        {
            // Press once.
            isShooting = Input.GetKeyDown(KeyCode.Mouse0);
        }
        if(readyToShoot && isShooting)
        {
            burstBulletsLeft = bulletsPerBurst;
            FireWeapon();
        }
    }

    private void FireWeapon()
    {
        readyToShoot = false;
        
        Vector3 shootingDirection = CalculateDirectionAndSpread().normalized;

        // Instantiate bullet.
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);

        // Shooting direction.
        bullet.transform.forward = shootingDirection;

        // Shoot bullet.
        bullet.GetComponent<Rigidbody>().AddForce(shootingDirection * bulletVelocity, ForceMode.Impulse);
        // Destroy bullet.
        StartCoroutine(DestroyBulletAfterTime(bullet, bulletPrefabLifeTime));
        weaponSound.Play();

        // Check finnish shooting.
        if (allowReset)
        {
            Invoke("ResetShot", shootingDelay);
            allowReset = false;
        }

        // Burst mode.
        if(currentShootingMode == ShootingMode.Burst && burstBulletsLeft > 1)// Shoot once before check;
        {
            burstBulletsLeft--;
            Invoke("FireWeapon", shootingDelay);
        }
    }

    private void ResetShot()
    {
        readyToShoot = true;
        allowReset = true;
    }

    public Vector3 CalculateDirectionAndSpread()
    {
        // Raycast from middle of the screen.
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        Vector3 targetPoint;
        if(Physics.Raycast(ray, out hit))
        {
            // Hitting something.
            targetPoint = hit.point;
        }
        else
        {
            // Shooting in air.
            targetPoint = ray.GetPoint(100);
        }

        Vector3 direction = targetPoint - bulletSpawn.position;

        float x = UnityEngine.Random.Range(-spreadIntensity, spreadIntensity);
        float y = UnityEngine.Random.Range(-spreadIntensity, spreadIntensity);

        return direction + new Vector3(x, y, 0);
    }

    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);
    }
}
