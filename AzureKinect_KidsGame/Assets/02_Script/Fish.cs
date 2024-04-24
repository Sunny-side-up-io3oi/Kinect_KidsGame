using UnityEngine;

public class Fish : MonoBehaviour
{
    public int score = 1; // 물고기의 점수

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // 물고기가 플레이어와 충돌했을 때 점수 증가 및 물고기 제거
            GameManager.Instance.AddScore(score);
            Destroy(gameObject);
        }
    }
}