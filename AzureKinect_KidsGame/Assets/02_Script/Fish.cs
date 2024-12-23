using UnityEngine;

public class Fish : MonoBehaviour
{
    public int score;
    public float speed;
    public GameObject scoreParticlePrefab;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        SetSpeed();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager gameManager = GameManager.Instance;
            if (gameManager != null)
            {
                GameObject playerObject = collision.gameObject;
                string playerName = playerObject.name.Replace("(Clone)", "").Trim(); // 플레이어 오브젝트의 이름을 가져옴

                int scoreToAdd = 0;
                if (gameObject.CompareTag("FishA"))
                    scoreToAdd = 10;
                else if (gameObject.CompareTag("FishB"))
                    scoreToAdd = 20;
                else if (gameObject.CompareTag("FishC"))
                    scoreToAdd = 30;
                else if (gameObject.CompareTag("FishD"))
                    scoreToAdd = 40;
                else if (gameObject.CompareTag("FishE"))
                    scoreToAdd = 50;

                gameManager.AddScore(playerName, scoreToAdd);
                gameManager.PlayScoreSound();

                if (scoreParticlePrefab != null)
                {
                    Instantiate(scoreParticlePrefab, transform.position, Quaternion.identity);
                }
            }

            Destroy(gameObject);
        }
    }

    private void SetSpeed()
    {
        rb.velocity = new Vector3(0f, -speed, 0f);
    }
}
