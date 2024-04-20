using UnityEngine;
using UnityEngine.UI;

public class FishGameManager : MonoBehaviour
{
    public Text scoreText;
    public Text scoreText2;
    public Text scoreText3;
    private int score = 0;

    public GameObject[] fishPrefabs; // 대, 중, 소 물고기 프리팹 배열
    public Transform[] spawnPoints; // 물고기가 스폰될 위치 배열

    private void Start()
    {
        // 1분 30초마다 SpawnFish 함수 호출
        InvokeRepeating("SpawnFish", 0f, 90f);
    }

    private void SpawnFish()
    {
        // 물고기 스폰
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            // 랜덤하게 물고기의 크기를 선택
            int fishIndex = Random.Range(0, fishPrefabs.Length);
            GameObject fishPrefab = fishPrefabs[fishIndex];

            // 물고기 프리팹을 스폰 포인트에 생성
            GameObject newFish = Instantiate(fishPrefab, spawnPoints[i].position, Quaternion.identity);

            // 각 물고기의 크기에 따라 떨어지는 속도 조절
            FishMovement fishMovement = newFish.GetComponent<FishMovement>();
            if (fishMovement != null)
            {
                if (fishIndex == 0) // 대 물고기
                    fishMovement.fallSpeed = 6f;
                else if (fishIndex == 1) // 중 물고기
                    fishMovement.fallSpeed = 4f;
                else // 소 물고기
                    fishMovement.fallSpeed = 3f;
            }
        }
    }

    public void AddScore(int scoreToAdd)
    {
        // 점수 추가 및 스토어 텍스트 업데이트
        score += scoreToAdd;
        scoreText.text = "점수: " + score.ToString();
        scoreText2.text = "점수: " + score.ToString();
        scoreText3.text = "점수: " + score.ToString();

    }
}
