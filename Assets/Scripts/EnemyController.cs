using UnityEngine;

public class EnemyController: MonoBehaviour
{
    public GameObject enemyBulletPrefab;
    public float fireInterval = 1f;
    private float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= fireInterval)
        {
            Fire();
            timer = 0f;
        }
    }

    void Fire()
    {
        if (enemyBulletPrefab != null)
        {
            Instantiate(enemyBulletPrefab, transform.position, Quaternion.identity);
        }
    }
}
