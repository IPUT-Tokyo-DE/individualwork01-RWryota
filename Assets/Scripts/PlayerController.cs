using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 45f;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Vector2 moveDirection;

    void Update()
    {

        // ÉJÉÅÉâì‡Ç…êßå¿
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp01(pos.x);
        pos.y = Mathf.Clamp01(pos.y);
        Vector3 newPos = Camera.main.ViewportToWorldPoint(pos);
        newPos.z = transform.position.z; // zé≤Çå≥ÇÃÇ‹Ç‹Ç…
        transform.position = newPos;
        Move();

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Shoot();

        }
    }

    void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 dir = new Vector3(h, v, 0).normalized;
        transform.position += dir * moveSpeed * Time.deltaTime;
    }

    void Shoot()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        }
    }
}
