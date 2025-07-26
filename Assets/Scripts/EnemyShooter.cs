using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    public GameObject enemyBulletPrefab;
    public GameObject explodingBulletPrefab; // �ǉ�: �������e�p�v���n�u

    public float fireInterval = 2f;
    public float minFireInterval = 0.3f; // �ŒZ�Ԋu�i���E�j
    public float intervalDecreaseRate = 0.05f; // 1�񌂂��ƂɌ����
    private float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= fireInterval)

        {
            
         int pattern = Random.Range(0, 4); // 0, 1, 2.3�̂����ꂩ��I��

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
                    FireExplodingBullet(); // �V�p�^�[���F�������e
                    break;
            }

            timer = 0f;
            // ���ˊԊu�����炵�Ă���
            fireInterval -= intervalDecreaseRate;

            // �ŒZ�Ԋu���Z���Ȃ�Ȃ��悤�ɐ���
            fireInterval = Mathf.Max(fireInterval, minFireInterval);
        }
    }

    // ���ˌ^�i�S�����ɔ��ˁj
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

    // �����^�i�p�x���������Y�����Ĕ��ˁj
    void FireSpiral()
    {
        int bulletCount = 18;
        float bulletSpeed = 2f;
        float startAngle = Time.time * 100f % 360f; // ���Ԃŕω�������Ɖ�]����

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

    // �_�������i�v���C���[�̕����ɔ��ˁj
    void FireAimedAtPlayer()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player == null) return;

        Vector2 direction = (player.transform.position - transform.position).normalized;
        float bulletSpeed = 4f;

        GameObject bullet = Instantiate(enemyBulletPrefab, transform.position, Quaternion.identity);
        bullet.GetComponent<EnemyBullet>().SetDirection(direction, bulletSpeed);
    }
    // �V�p�^�[���F�������e
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
            bullet.GetComponent<ExplodingBullet>().SetDirection(direction); // SetDirection�݂̂�OK

            angle += angleStep;
        }
    }
}