using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    private Rigidbody2D rb;
    public float bulletSpeed;
    public int bulletDamage;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.AddForce(bulletSpeed * gameObject.transform.up, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyManager enemyScript = collision.gameObject.GetComponent<EnemyManager>();
           // enemyScript.ChangeHealth(-bulletDamage);
        }
    }
    private void SetDamage(int towerDamage)
    {
        bulletDamage = towerDamage;
    }
}
