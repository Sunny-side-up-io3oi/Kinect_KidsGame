using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    public GameObject[] fishPrefabs;
    public float minSpawnInterval = 0.5f; 
    public float maxSpawnInterval = 2f; 

    private void Start()
    {
       
        InvokeRepeating("SpawnFish", 0f, Random.Range(minSpawnInterval, maxSpawnInterval));
        Debug.Log("물고기 나왔다!");
    }

    void SpawnFish()
    {
        
        Vector3 spawnPosition = transform.position;

        
        GameObject randomFishPrefab = fishPrefabs[Random.Range(0, fishPrefabs.Length)];

        
        GameObject fish = Instantiate(randomFishPrefab, spawnPosition, Quaternion.identity);

       
        Rigidbody rb = fish.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = true;
        }
    }
}
