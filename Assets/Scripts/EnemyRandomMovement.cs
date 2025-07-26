using UnityEngine;

public class EnemyRandomMovement : MonoBehaviour
{
    public float moveSpeed = 2f;               // 移動速度
    public float changeTargetInterval = 2f;    // 移動先を変える間隔
    public Vector2 moveAreaMin = new Vector2(-5f, -3f); // 移動範囲（左下）
    public Vector2 moveAreaMax = new Vector2(5f, 3f);   // 移動範囲（右上）

    private Vector3 targetPosition;
    private float timer;

    void Start()
    {
        SetNewTarget();
    }

    void Update()
    {
        timer += Time.deltaTime;

        // ターゲット位置へ移動
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // 一定時間ごとに新しい移動先を決める
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