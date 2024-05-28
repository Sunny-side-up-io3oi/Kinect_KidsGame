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
    private Dictionary<ulong, GameObject> trackedPlayers = new Dictionary<ulong, GameObject>(); // 플레이어 ID와 오브젝트를 매핑하는 Dictionary
    private List<ulong> lostIds = new List<ulong>(); // 추적되지 않는 플레이어 ID 리스트 (재사용)
    private int prefabIndex = 0; // 현재 사용할 플레이어 프리팹 인덱스

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
        lostIds.Clear(); // 추적되지 않는 ID 리스트를 초기화

        foreach (var body in bodies)
        {
            if (body.IsTracked)
            {
                if (!trackedPlayers.ContainsKey(body.TrackingId))
                {
                    // 새로운 플레이어를 생성
                    GameObject player = Instantiate(playerPrefabs[prefabIndex], GetInitialPosition(prefabIndex), Quaternion.identity); // 초기 위치와 회전값을 지정하여 생성
                    trackedPlayers.Add(body.TrackingId, player);

                    prefabIndex = (prefabIndex + 1) % playerPrefabs.Count; // 다음 플레이어 프리팹 인덱스 계산
                }
                else
                {
                    // 플레이어가 이미 존재하면 활성화
                    trackedPlayers[body.TrackingId].SetActive(true);
                }
                UpdatePlayerPosition(body, trackedPlayers[body.TrackingId]); // 플레이어 위치 업데이트
            }
        }
    }

    private void RemoveLostPlayers()
    {
        foreach (var id in trackedPlayers.Keys)
        {
            if (!IsTrackedId(id))
            {
                // 추적되지 않는 플레이어 오브젝트 비활성화
                trackedPlayers[id].SetActive(false);
                lostIds.Add(id);
            }
        }

        foreach (var id in lostIds)
        {
            trackedPlayers.Remove(id); // 추적되지 않는 플레이어 제거
        }
    }

    private bool IsTrackedId(ulong id)
    {
        foreach (var body in bodies)
        {
            if (body.IsTracked && body.TrackingId == id)
            {
                return true;
            }
        }
        return false;
    }

    private Vector3 GetInitialPosition(int prefabIndex)
    {
        // 각 플레이어 프리팹별로 초기 위치를 지정
        switch (prefabIndex)
        {
            case 0: // 첫 번째 플레이어 프리팹
                return new Vector3(-36.8f, -2.46f, 0f);
            case 1: // 두 번째 플레이어 프리팹
                return new Vector3(30.8f, -2.46f, 0f);
            // 필요에 따라 추가적인 플레이어 프리팹에 대한 초기 위치 지정
            default:
                return Vector3.zero;
        }
    }

    private void UpdatePlayerPosition(Body body, GameObject player)
    {
        var spineBasePosition = body.Joints[JointType.SpineBase].Position;
        var unitySpineBasePosition = new Vector3(spineBasePosition.X, spineBasePosition.Y, -spineBasePosition.Z);

        // Kinect 데이터를 기반으로 새로운 위치 계산
        var newPosition = new Vector3(unitySpineBasePosition.x * moveSpeed, player.transform.position.y, player.transform.position.z);

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
