using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    private Rigidbody2D rb;
    public float bulletSpeed;
    public int bulletDamage;
    public GameObject target;
    private float disFromEnemy;
    [SerializeField] private float autoAimDis = 7;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.AddForce(bulletSpeed * gameObject.transform.up, ForceMode2D.Impulse);

        if (disFromEnemy < autoAimDis)
        {
            transform.up = target.transform.position - transform.position;
        } else
        {
            //gameObject.transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * bulletSpeed * 3);
        }
    }

    private void Update()
    {
        disFromEnemy = Vector2.Distance(transform.position, target.transform.position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyManager enemyScript = collision.gameObject.GetComponent<EnemyManager>();
           // enemyScript.ChangeHealth(-bulletDamage);
        }
    }
    public void SetValues(int towerDamage, GameObject _target)
    {
        bulletDamage = towerDamage;
        target = _target;
    }
}
