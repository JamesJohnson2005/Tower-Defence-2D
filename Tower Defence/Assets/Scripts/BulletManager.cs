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
    [SerializeField] private bool explode;
    [SerializeField] private float autoAimDis = 7;
    [SerializeField] private float explosionRadius;
    [SerializeField] private int explosionDamage;

    public AudioSource shootSound;
    public AudioSource explosionSound;
    public GameObject explosionParticle;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        shootSound.Play();
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
            if (explode) {

                Collider2D[] hitCollider;
                hitCollider = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
                GameObject spawnedObj = Instantiate(explosionParticle, transform.position, Quaternion.identity);
                spawnedObj.transform.rotation = new Quaternion(90, 0, 0, 0);
                Destroy(spawnedObj, 2);
                Destroy(gameObject);
                for (int i = 0; i < hitCollider.Length; i++)
                {
                    // Check to see if enemy is still valid
                    if (hitCollider[i].gameObject == null) { return; }

                    if (!hitCollider[i].gameObject.GetComponent<EnemyManager>()) { return; }
                    EnemyManager enemyScriptLocal = hitCollider[i].gameObject.GetComponent<EnemyManager>();

                    if (enemyScriptLocal.explosionImmunity) { return; }

                    enemyScriptLocal.ChangeHealth(-explosionDamage);
                }
                Destroy(gameObject);

            }
            Destroy(gameObject);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }

    public void SetValues(int towerDamage, GameObject _target, bool _explode)
    {
        bulletDamage = towerDamage;
        target = _target;
        explode = _explode;
    }
}
