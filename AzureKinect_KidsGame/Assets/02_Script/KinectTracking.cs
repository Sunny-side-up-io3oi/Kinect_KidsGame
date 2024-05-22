using System.Collections.Generic;
using UnityEngine;
using Windows.Kinect;

public class KinectTracking : MonoBehaviour
{
    private KinectSensor kinectSensor;
    private BodyFrameReader bodyFrameReader;
    private Body[] bodies;

    public List<GameObject> playerPrefabs; // 플레이어 프리팹 리스트
    public float moveSpeed = 100f;

    // 플레이어를 추적하는 Dictionary
    private Dictionary<ulong, int> playerIndices = new Dictionary<ulong, int>(); // 플레이어 ID와 인덱스를 매핑하는 Dictionary
    private List<GameObject> players = new List<GameObject>(); // 플레이어 오브젝트 리스트
    private List<ulong> trackedIds = new List<ulong>(); // 추적된 플레이어 ID 리스트 (재사용)
    private List<ulong> lostIds = new List<ulong>(); // 추적되지 않는 플레이어 ID 리스트 (재사용)

    void Start()
    {
        // Kinect 센서를 초기화
        kinectSensor = KinectSensor.GetDefault();
        if (kinectSensor != null)
        {
            // BodyFrameReader를 엽니다
            bodyFrameReader = kinectSensor.BodyFrameSource.OpenReader();
            if (!kinectSensor.IsOpen)
            {
                kinectSensor.Open(); // 센서가 열려 있지 않으면 엽니다
            }
        }
        else
        {
            UnityEngine.Debug.LogError("No Kinect sensor found.");
        }
    }

    void Update()
    {
        if (bodyFrameReader == null) return; // bodyFrameReader가 초기화되지 않은 경우 리턴

        var frame = bodyFrameReader.AcquireLatestFrame();
        if (frame != null)
        {
            if (bodies == null)
            {
                bodies = new Body[kinectSensor.BodyFrameSource.BodyCount]; // bodies 배열을 초기화
            }

            frame.GetAndRefreshBodyData(bodies);
            UpdateTrackedPlayers(); // 추적된 플레이어 업데이트
            RemoveLostPlayers(); // 추적되지 않는 플레이어 제거
            frame.Dispose(); // 사용 후 프레임을 해제
        }
    }

    private void UpdateTrackedPlayers()
    {
        trackedIds.Clear(); // 추적된 ID 리스트를 초기화

        foreach (var body in bodies)
        {
            if (body.IsTracked)
            {
                trackedIds.Add(body.TrackingId); // 추적된 ID 리스트에 추가
                if (!playerIndices.ContainsKey(body.TrackingId))
                {
                    // 새로운 플레이어를 생성
                    int prefabIndex = Random.Range(0, playerPrefabs.Count); // 랜덤하게 플레이어 프리팹을 선택
                    GameObject player = Instantiate(playerPrefabs[prefabIndex]);
                    playerIndices.Add(body.TrackingId, players.Count);
                    players.Add(player);
                }
                else
                {
                    // 플레이어가 이미 존재하면 활성화
                    players[playerIndices[body.TrackingId]].SetActive(true);
                }
                UpdatePlayerPosition(body); // 플레이어 위치 업데이트
            }
        }
    }

    private void RemoveLostPlayers()
    {
        lostIds.Clear(); // 추적되지 않는 ID 리스트를 초기화

        foreach (ulong id in playerIndices.Keys)
        {
            if (!trackedIds.Contains(id))
            {
                lostIds.Add(id); // 추적되지 않는 ID 리스트에 추가
            }
        }

        foreach (ulong id in lostIds)
        {
            // 추적되지 않는 플레이어 오브젝트 비활성화
            players[playerIndices[id]].SetActive(false);
        }
    }

    private void UpdatePlayerPosition(Body body)
    {
        var spineBasePosition = body.Joints[JointType.SpineBase].Position;
        var unitySpineBasePosition = new Vector3(spineBasePosition.X, spineBasePosition.Y, -spineBasePosition.Z);

        // Kinect 데이터를 기반으로 새로운 위치 계산
        int playerIndex = playerIndices[body.TrackingId];
        GameObject player = players[playerIndex];
        Vector3 initialPosition = player.transform.position; // 플레이어의 초기 위치 가져오기

        // 좌표를 초기 위치에서 좌우로만 이동하도록 업데이트
        var newPosition = new Vector3(unitySpineBasePosition.x * moveSpeed, initialPosition.y, initialPosition.z);

        // 플레이어 위치 업데이트
        player.transform.position = newPosition;
    }

    void OnDestroy()
    {
        // bodyFrameReader가 null이 아닌 경우 Dispose
        bodyFrameReader?.Dispose();
        bodyFrameReader = null;

        // Kinect 센서가 열려 있는 경우 닫기
        if (kinectSensor != null && kinectSensor.IsOpen)
        {
            kinectSensor.Close();
        }
        kinectSensor = null;
    }
}
