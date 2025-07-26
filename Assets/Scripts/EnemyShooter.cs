using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    public GameObject enemyBulletPrefab;
    public GameObject explodingBulletPrefab; // 追加: さく裂弾用プレハブ

    public float fireInterval = 2f;
    public float minFireInterval = 0.3f; // 最短間隔（限界）
    public float intervalDecreaseRate = 0.05f; // 1回撃つごとに減る量
    private float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= fireInterval)

        {
            
         int pattern = Random.Range(0, 4); // 0, 1, 2.3のいずれかを選ぶ

            switch (pattern)
            {
                case 0:
                    FireRadial();
                    break;
                case 1:
                    FireSpiral();
                    break;
                case 2:
                    FireAimedAtPlayer();
                    break;
                case 3:
                    FireExplodingBullet(); // 新パターン：さく裂弾
                    break;
            }

            timer = 0f;
            // 発射間隔を減らしていく
            fireInterval -= intervalDecreaseRate;

            // 最短間隔より短くならないように制限
            fireInterval = Mathf.Max(fireInterval, minFireInterval);
        }
    }

    // 放射型（全方向に発射）
    void FireRadial()
    {
        int bulletCount = 12;
        float bulletSpeed = 3f;

        float angleStep = 360f / bulletCount;
        float angle = 0f;

        for (int i = 0; i < bulletCount; i++)
        {
            float dirX = Mathf.Cos(angle * Mathf.Deg2Rad);
            float dirY = Mathf.Sin(angle * Mathf.Deg2Rad);
            Vector2 direction = new Vector2(dirX, dirY).normalized;

            GameObject bullet = Instantiate(enemyBulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<EnemyBullet>().SetDirection(direction, bulletSpeed);

            angle += angleStep;
        }
    }

    // 螺旋型（角度を少しずつズラして発射）
    void FireSpiral()
    {
        int bulletCount = 18;
        float bulletSpeed = 2f;
        float startAngle = Time.time * 100f % 360f; // 時間で変化させると回転する

        for (int i = 0; i < bulletCount; i++)
        {
            float angle = startAngle + (360f / bulletCount) * i;
            float dirX = Mathf.Cos(angle * Mathf.Deg2Rad);
            float dirY = Mathf.Sin(angle * Mathf.Deg2Rad);
            Vector2 direction = new Vector2(dirX, dirY).normalized;

            GameObject bullet = Instantiate(enemyBulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<EnemyBullet>().SetDirection(direction, bulletSpeed);
        }
    }

    // 狙い撃ち（プレイヤーの方向に発射）
    void FireAimedAtPlayer()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player == null) return;

        Vector2 direction = (player.transform.position - transform.position).normalized;
        float bulletSpeed = 4f;

        GameObject bullet = Instantiate(enemyBulletPrefab, transform.position, Quaternion.identity);
        bullet.GetComponent<EnemyBullet>().SetDirection(direction, bulletSpeed);
    }
    // 新パターン：さく裂弾
    void FireExplodingBullet()
    {

        int bulletCount = 5;

        float angleStep = 360f / bulletCount;
        float angle = 0f;

        for (int i = 0; i < bulletCount; i++)
        {
            float dirX = Mathf.Cos(angle * Mathf.Deg2Rad);
            float dirY = Mathf.Sin(angle * Mathf.Deg2Rad);
            Vector2 direction = new Vector2(dirX, dirY).normalized;

            GameObject bullet = Instantiate(explodingBulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<ExplodingBullet>().SetDirection(direction); // SetDirectionのみでOK

            angle += angleStep;
        }
    }
}