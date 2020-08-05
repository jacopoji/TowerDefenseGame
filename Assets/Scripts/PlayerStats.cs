using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static float gold;
    public int startingGold = 500;

    public static int lives;
    public int startingLives = 1;

    public static PlayerStats instance;
    void Awake()
    {
        gold = startingGold;
        lives = startingLives;
        instance = this;
    }

    public void TakeDamage(int damage)
    {
        lives -= damage;
        GameMaster.instance.CheckGameOver();
    }
    
}
