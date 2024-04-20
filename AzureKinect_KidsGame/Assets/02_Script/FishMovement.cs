using UnityEngine;

public class FishMovement : MonoBehaviour
{
    public float fallSpeed = 5f; // 물고기의 떨어지는 속도

    private void Update()
    {
        // 아래 방향으로 물고기를 이동
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);

        // 물고기가 화면 아래로 벗어나면 삭제
        if (transform.position.y < -5f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 플레이어와 충돌하면 게임 매니저에게 점수 추가 요청
        if (collision.CompareTag("Player"))
        {
            FishGameManager gameManager = FindObjectOfType<FishGameManager>();
            if (gameManager != null)
            {
                int scoreToAdd = 0;
                if (gameObject.CompareTag("LargeFish"))
                    scoreToAdd = 20;
                else if (gameObject.CompareTag("MediumFish"))
                    scoreToAdd = 50;
                else if (gameObject.CompareTag("SmallFish"))
                    scoreToAdd = 100;

                gameManager.AddScore(scoreToAdd);
            }

            // 충돌한 물고기 삭제
            Destroy(gameObject);
        }
    }
}
