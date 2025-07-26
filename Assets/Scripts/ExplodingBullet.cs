using UnityEngine;

public class ExplodingBullet : MonoBehaviour
{
    public GameObject childBulletPrefab; // �q�e��Prefab
    public float speed = 7f;
    public float explodeDelay = 3f; // ������܂ł̎���
    public int childBulletCount = 8;
    private Vector2 moveDirection;
    private float timer = 0f;

    public void SetDirection(Vector2 direction)
    {
        moveDirection = direction;
    }

    void Update()
    {
        // �e�e���ړ�
        transform.Translate(moveDirection * speed * Time.deltaTime);

        // ���Ԍv��
        timer += Time.deltaTime;
        if (timer >= explodeDelay)
        {
            Explode();
        }
    }

    void Explode()
    {
        float angleStep = 360f / childBulletCount;
        float angle = 0f;

        for (int i = 0; i < childBulletCount; i++)
        {
            float dirX = Mathf.Cos(angle * Mathf.Deg2Rad);
            float dirY = Mathf.Sin(angle * Mathf.Deg2Rad);
            Vector2 direction = new Vector2(dirX, dirY).normalized;

            GameObject bullet = Instantiate(childBulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<EnemyBullet>().SetDirection(direction, speed); // �q�e�͒ʏ��EnemyBullet��OK

            angle += angleStep;
        }

        Destroy(gameObject); // �e�e������
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}