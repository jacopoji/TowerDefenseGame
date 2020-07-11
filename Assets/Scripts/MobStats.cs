using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobStats : MonoBehaviour
{
    
    public float startSpeed = 8f;
    //[HideInInspector]
    public float speed;
    public float hp = 100f;
    public int MobWorthGold = 50;
    public int MobDoesDamageAmount;
    // Start is called before the first frame update

    public GameObject DieEffect;
    private BuildManager buildManager;
    void Start()
    {
        speed = startSpeed;
        buildManager = BuildManager.instance;
    }

    public void TakeDamage(float damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Die();
        }
    }

    public void Slow(float pct)
    {
        speed = (1f-pct) * startSpeed;
    }

    void Die()
    {
        GameObject dieEffect = Instantiate(DieEffect, transform.position, Quaternion.identity);
        Destroy(dieEffect, 3f);
        Destroy(gameObject);
        buildManager.AddGold(MobWorthGold);
    }
}
