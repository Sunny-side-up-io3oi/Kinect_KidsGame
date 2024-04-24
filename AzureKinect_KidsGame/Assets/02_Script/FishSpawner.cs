using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    public GameObject[] fishPrefabs; // 스폰될 물고기 프리팹 배열
    public float minSpawnInterval = 0.5f; // 최소 스폰 간격
    public float maxSpawnInterval = 2f; // 최대 스폰 간격

    private void Start()
    {
        // 물고기 스폰을 시작
        InvokeRepeating("SpawnFish", 0f, Random.Range(minSpawnInterval, maxSpawnInterval));
    }

    void SpawnFish()
    {
        // 게임 오브젝트의 위치를 생성 위치로 설정
        Vector3 spawnPosition = transform.position;

        // 랜덤한 물고기 프리팹 선택
        GameObject randomFishPrefab = fishPrefabs[Random.Range(0, fishPrefabs.Length)];

        // 물고기 스폰
        GameObject fish = Instantiate(randomFishPrefab, spawnPosition, Quaternion.identity);

        // 물고기에 Rigidbody 컴포넌트가 있으면 중력 활성화
        Rigidbody rb = fish.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = true;
        }
    }
}
