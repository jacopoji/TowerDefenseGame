using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public Transform target;
    private float shootCountdown = 0f;
    private LineRenderer laserClone;
    private ParticleSystem laserImpactEffectClone;
    public float range = 18f;
    public float selfValue;
    [Header("Use Bullet(default)")]
    public GameObject bulletPrefab;
    public float aimSpeed = 10f;
    public float fireRate = 1f;

    [Header("Use Laser")]
    public bool useLaser = false;
    public LineRenderer laser;
    public ParticleSystem LaserImpactEffect;
    public float damageOverTime;
    public float slowPercentage =.5f;

    [Header("Setup")]
    public Transform partToRotate;
    public Transform shootPoint;
    public float depreciation = 0.6f;

    private MobStats enemy;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget()
    {
        if (target != null && TargetInRange(target, transform, range))
            return;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float dist;
        float minDist = Mathf.Infinity;
        Transform foundTarget = null;
        foreach (GameObject enemy in enemies)
        {
            dist = Vector3.Distance(transform.position, enemy.transform.position);
            if(dist < minDist)
            {
                minDist = dist;
                foundTarget = enemy.transform;
            }
        }
        if(minDist <= range && foundTarget != null)
        {
            target = foundTarget;
            enemy = target.GetComponent<MobStats>();
        }
        else
        {
            target = null;

        }
}

    // Update is called once per frame
    void Update()
    {
        
        if (target == null)
        {
            if(laserClone!=null)
            {
                if(laserClone.enabled)
                    laserClone.enabled = false;
                LaserImpactEffect.Stop();
            }
            return;
        }
        LockOnTarget();
        if (useLaser)
        {
            Laser();
        }
        else
        {
            if (shootCountdown <= 0)
            {
                Shoot();
                shootCountdown = 1 / fireRate;
            }


            shootCountdown -= Time.deltaTime;
        }
        
    }

    void LockOnTarget()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * aimSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    bool TargetInRange(Transform target,Transform self, float range)
    {
        return Vector3.Distance(target.position, self.position) < range;
    }

    void Laser()
    {
        //if (!laser.enabled)
        //    laser.enabled = true;
        if(laserClone == null)
        {
            laserClone = Instantiate(laser, transform.position, Quaternion.identity);
            LaserImpactEffect = laserClone.GetComponentInChildren<ParticleSystem>();
        }

        if (!laserClone.enabled)
        {
            laserClone.enabled = true;
            LaserImpactEffect.Play();
        }
        enemy.TakeDamage(damageOverTime * Time.deltaTime);
        enemy.Slow(slowPercentage);
        Vector3 dir = shootPoint.position - target.position;

        LaserImpactEffect.transform.rotation = Quaternion.LookRotation(dir);
        var shape = LaserImpactEffect.shape;
        shape.rotation = dir;
        LaserImpactEffect.transform.position = target.position + dir.normalized * target.transform.localScale.magnitude/2;


        laserClone.SetPosition(0, shootPoint.position);
        laserClone.SetPosition(1, target.position);
    }

    void Shoot()
    {
        GameObject BulletGO = (GameObject)Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
        Bullet bullet = BulletGO.GetComponent<Bullet>();
        bullet.SetTarget(target);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    public void UpdateSelfValue(int amount)
    {
        selfValue += amount * depreciation;
        //Debug.Log("Self Value updated to " + selfValue);
    }
}
