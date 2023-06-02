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
        if (disFromEnemy > autoAimDis)
        {
            rb.AddForce(bulletSpeed * gameObject.transform.up, ForceMode2D.Impulse);
        } else
        {
            if (target)
            {
                gameObject.transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * bulletSpeed * 3);
            }
        }
    }

    private void Update()
    {
        if (target != null)
        {
            disFromEnemy = Vector2.Distance(transform.position, target.transform.position);
        } else
        {
            disFromEnemy = Mathf.Infinity;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyManager enemyScript = collision.gameObject.GetComponent<EnemyManager>();
            enemyScript.ChangeHealth(-bulletDamage);
            Destroy(gameObject);
        }
    }
    public void SetValues(int towerDamage, GameObject _target)
    {
        bulletDamage = towerDamage;
        target = _target;
    }
}
