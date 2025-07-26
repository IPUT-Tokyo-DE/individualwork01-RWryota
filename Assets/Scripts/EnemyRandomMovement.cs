using UnityEngine;

public class EnemyRandomMovement : MonoBehaviour
{
    public float moveSpeed = 2f;               // �ړ����x
    public float changeTargetInterval = 2f;    // �ړ����ς���Ԋu
    public Vector2 moveAreaMin = new Vector2(-5f, -3f); // �ړ��͈́i�����j
    public Vector2 moveAreaMax = new Vector2(5f, 3f);   // �ړ��͈́i�E��j

    private Vector3 targetPosition;
    private float timer;

    void Start()
    {
        SetNewTarget();
    }

    void Update()
    {
        timer += Time.deltaTime;

        // �^�[�Q�b�g�ʒu�ֈړ�
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // ��莞�Ԃ��ƂɐV�����ړ�������߂�
        if (timer >= changeTargetInterval || Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            SetNewTarget();
            timer = 0f;
        }
    }

    void SetNewTarget()
    {
        float x = Random.Range(moveAreaMin.x, moveAreaMax.x);
        float y = Random.Range(moveAreaMin.y, moveAreaMax.y);
        targetPosition = new Vector3(x, y, transform.position.z);
    }
}