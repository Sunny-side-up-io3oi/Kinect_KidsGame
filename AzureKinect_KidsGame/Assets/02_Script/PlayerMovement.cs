using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // 플레이어 이동 속도

    private Rigidbody rb; // Rigidbody 컴포넌트 참조

    private void Start()
    {
        // Rigidbody 컴포넌트 가져오기
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // 수평 입력 감지
        float horizontalInput = Input.GetAxis("Horizontal");

        // 키보드 입력값이 있는 경우에만 움직이기
        if (Mathf.Abs(horizontalInput) > 0.1f)
        {
            // 플레이어 이동 방향 벡터 계산
            Vector3 movement = new Vector3(horizontalInput, 0f, 0f) * moveSpeed * Time.deltaTime;

            // Raycast를 사용하여 이동할 위치에 벽이 있는지 확인
            RaycastHit hit;
            if (!Physics.Raycast(transform.position, movement.normalized, out hit, movement.magnitude))
            {
                // 벽이 없는 경우에만 Rigidbody를 이용하여 플레이어 이동
                rb.MovePosition(transform.position + movement);
            }
        }
    }
}
