using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FishGameManager : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text scoreText2;
    public TMP_Text scoreText3;
    private int score = 0;

    public GameObject[] fishPrefabs; 
    public Transform[] spawnPoints; 

    private void Start()
    {
      
        InvokeRepeating("SpawnFish", 0f, 90f);
    }

    private void SpawnFish()
    {
        
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            
            int fishIndex = Random.Range(0, fishPrefabs.Length);
            GameObject fishPrefab = fishPrefabs[fishIndex];

            
            GameObject newFish = Instantiate(fishPrefab, spawnPoints[i].position, Quaternion.identity);

            
            FishMovement fishMovement = newFish.GetComponent<FishMovement>();
            if (fishMovement != null)
            {
                if (fishIndex == 0) 
                    fishMovement.fallSpeed = 6f;
                else if (fishIndex == 1) 
                    fishMovement.fallSpeed = 4f;
                else 
                    fishMovement.fallSpeed = 3f;
            }
        }
    }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "점수: " + score.ToString();
        scoreText2.text = "점수: " + score.ToString();
        scoreText3.text = "점수: " + score.ToString();
    }
}
