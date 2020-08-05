using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;
    public float speed = 50f;
    public GameObject bulletImpactEffect;
    public int bulletDamage = 50;
    private float damageAmplifier = 1;
    public float explosionRadius;
    public void SetTarget(Transform _target)
    {
        target = _target;
    }
    

    // Update is called once per frame
    void Update()
    {
        if(target != null)
        {
            Vector3 dir =  target.position - transform.position;
            float travelDistance = speed * Time.deltaTime;
            if(travelDistance >= dir.magnitude)
            {
                BulletHit();
                Destroy(gameObject);
                return;
            }
            transform.Translate(dir.normalized * travelDistance, Space.World);
            transform.LookAt(target);
        }
        if(target == null)
        {
            Destroy(gameObject);
        }
    }

    void BulletHit()
    {
        GameObject effect = Instantiate(bulletImpactEffect, transform.position, transform.rotation);
        Destroy(effect, 5f);
        if (explosionRadius > 0f)
        {
            explosion();
        }
        else
        {
            damage(target);
        }        
        //Debug.Log("Bullet hit!");
    }

    void explosion()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach ( Collider collider in colliders)
        {
            if(collider.tag == "Enemy")
            {
                damage(collider.transform);
            }
        }

    }
    void damage(Transform enemy)
    {
        MobStats mob = enemy.GetComponent<MobStats>();
        if (mob != null)
            mob.TakeDamage(bulletDamage * damageAmplifier);
    }

    public void SetDamageAmplifier(float value)
    {
        damageAmplifier = value;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
