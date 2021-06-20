using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject gun;
    public Animator animator;

    public Transform firePoint;
    public GameObject bulletPrefab;

    public float fireRate = 0.5f;
    public float bulletForce = 20f;
    private float nextShootTimer;
    private bool isShooting;

    public int maxTotalBullet;
    public int currentTotalBullet;
    public int magazineSize = 10;
    public int currentMagazineCount;
    public string bulletText = "10/100";

    void Start()
    {
        maxTotalBullet = 50;
        currentTotalBullet = maxTotalBullet;
        magazineSize = 10;
        currentMagazineCount = magazineSize;
        UpdateText();
    }

    void Update()
    {
        if (!PauseMenu.isPaused && !Player.isDead)
        {
            if ((Input.GetButtonDown("Fire1") || Input.GetButton("Fire1")) && Time.time > nextShootTimer)
            {
                if (currentMagazineCount > 0)
                {
                    isShooting = true;
                    animator.Play("Weapon1_firing");
                    Shoot();
                    Invoke("ResetShoot", fireRate);
                    nextShootTimer = Time.time + fireRate;
                }
                else
                {
                    Reload();
                    nextShootTimer = Time.time + fireRate;
                }
            }
        }
        
    }

    private void FixedUpdate()
    {
        if (!PauseMenu.isPaused && !Player.isDead)
        {
            Aim();
        }
    }

    void Shoot() 
    {
        currentMagazineCount -= 1;
        currentTotalBullet -= 1;
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse);
        UpdateText();
    }

    void ResetShoot()
    {
        isShooting = false;
        animator.Play("Weapon1_idle");
    }

    void Reload()
    {
        int missingFromMag = magazineSize - currentMagazineCount;
        if (missingFromMag < currentTotalBullet)
        {
            currentMagazineCount = magazineSize;
        }
        else
        {
            currentMagazineCount = currentTotalBullet;
        }
        UpdateText();
    }

    public void Refill()
    {
        currentTotalBullet += 25;
        if (currentTotalBullet > 50)
        {
            currentTotalBullet = 50;
        }
        Reload();
        UpdateText();
    }

    void UpdateText()
    {
        bulletText = currentTotalBullet.ToString() + "/" + maxTotalBullet.ToString();
    }

    void Aim()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        difference.Normalize();

        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);

        if (rotationZ < -90 || rotationZ > 90)
        {
            if (gun.transform.eulerAngles.y == 0)
            {
                transform.localRotation = Quaternion.Euler(180, 0, -rotationZ);
            }
            else if (gun.transform.eulerAngles.y == 180)
            {
                transform.localRotation = Quaternion.Euler(180, 180, -rotationZ);
            }
        }
    }

}
