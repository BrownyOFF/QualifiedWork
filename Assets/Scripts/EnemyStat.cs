using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : MonoBehaviour
{
    public float hp = 20;
    public float pieces = 10f;
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
        GameObject.Find("Player").GetComponent<PlayerChara>().getPieces(pieces);
        Destroy(gameObject);
    }
}
