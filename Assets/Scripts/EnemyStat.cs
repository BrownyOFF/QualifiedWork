using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : MonoBehaviour
{
    public float hp = 20;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void takingDamage(float damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            death();    
        }
    }

    private void death()
    {
        Destroy(gameObject);
    }
}
