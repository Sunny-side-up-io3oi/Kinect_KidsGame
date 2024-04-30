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
            GameManager gameManager = FindObjectOfType<GameManager>();
            if (gameManager != null)
            {
                int scoreToAdd = 0;
                if (gameObject.CompareTag("FishA"))
                    scoreToAdd = 20;
                else if (gameObject.CompareTag("FishB"))
                    scoreToAdd = 50;
                else if (gameObject.CompareTag("FishC"))
                    scoreToAdd = 100;

                gameManager.AddScore(scoreToAdd);
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
