using UnityEngine;

public class ExplodingBullet : MonoBehaviour
{
    public GameObject childBulletPrefab; // éqíeÇÃPrefab
    public float speed = 7f;
    public float explodeDelay = 3f; // Ç≥Ç≠óÙÇ‹Ç≈ÇÃéûä‘
    public int childBulletCount = 8;
    private Vector2 moveDirection;
    private float timer = 0f;

    public void SetDirection(Vector2 direction)
    {
        moveDirection = direction;
    }

    void Update()
    {
        // êeíeÇà⁄ìÆ
        transform.Translate(moveDirection * speed * Time.deltaTime);

        // éûä‘åvë™
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
            bullet.GetComponent<EnemyBullet>().SetDirection(direction, speed); // éqíeÇÕí èÌÇÃEnemyBulletÇ≈OK

            angle += angleStep;
        }

        Destroy(gameObject); // êeíeÇè¡Ç∑
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}