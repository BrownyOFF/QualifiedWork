using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombThrow : MonoBehaviour
{
    private float dmg = 40f;
    private float speed = 30f;
    private float time = 3f;
    private Vector2 dir;
    private Collider2D explColl;
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dir = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().FaceDirection();
        explColl = transform.GetChild(0).GetComponent<CircleCollider2D>();
        StartCoroutine(ExplodeTime());
        rb.AddForce(dir * 10 , ForceMode2D.Impulse);
    }

    private IEnumerator ExplodeTime()
    {
        yield return new WaitForSeconds(time);
        Boom();
    }
    void Boom()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(explColl.bounds.center, explColl.bounds.size, 0);
        foreach (var i in colliders)
        {
            if (i.gameObject.CompareTag("Enemy"))
            {
                i.gameObject.GetComponent<EnemySRC>().health -= dmg;
            }
        }
        Destroy(gameObject);
    }

    
    void Update()
    {
        
    }
}
