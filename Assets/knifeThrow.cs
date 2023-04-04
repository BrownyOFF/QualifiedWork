using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knifeThrow : MonoBehaviour
{
    private float dmg = 25;
    private float speed = 30f;
    private float time = 1f;
    private Vector2 dir;
    void Start()
    {
        dir = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().FaceDirection();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            col.GetComponent<EnemySRC>().TakeDamage(dmg);
            Destroy(gameObject);
        }
        else if (col.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator DestroyTime()
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
    void Update()
    {
        transform.Translate(dir * speed * Time.deltaTime);
    }
}
