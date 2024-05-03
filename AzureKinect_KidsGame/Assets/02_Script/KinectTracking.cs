using System.Diagnostics;
using UnityEngine;
using Windows.Kinect;

public class KinectSpineBaseTracking : MonoBehaviour
{ 
    private KinectSensor kinectSensor;
    private BodyFrameReader bodyFrameReader;
    private Body[] bodies;

    public GameObject objectToMove;
    public float moveSpeed = 100f;

    private Vector3 initialPosition; 

    void Start()
    {
        kinectSensor = KinectSensor.GetDefault();
        if (kinectSensor != null)
        {
            bodyFrameReader = kinectSensor.BodyFrameSource.OpenReader();
            if (!kinectSensor.IsOpen)
            {
                kinectSensor.Open();
            }
        }
        else
        {
            UnityEngine.Debug.LogError("No Kinect sensor found.");
        }

        
        initialPosition = objectToMove.transform.position;
    }

    void Update()
    {
        if (bodyFrameReader != null)
        {
            var frame = bodyFrameReader.AcquireLatestFrame();
            if (frame != null)
            {
                if (bodies == null)
                {
                    bodies = new Body[kinectSensor.BodyFrameSource.BodyCount];
                }

                frame.GetAndRefreshBodyData(bodies);

                foreach (var body in bodies)
                {
                    if (body.IsTracked)
                    {
                        var spineBasePosition = body.Joints[JointType.SpineBase].Position;

                        
                        var unitySpineBasePosition = new Vector3(spineBasePosition.X, spineBasePosition.Y, -spineBasePosition.Z);

                        
                        var newPosition = initialPosition + new Vector3(unitySpineBasePosition.x, 0f, 0f) * moveSpeed;

                        
                        objectToMove.transform.position = newPosition;
                    }
                }

                frame.Dispose();
            }
        }
    }

    void OnDestroy()
    {
        if (bodyFrameReader != null)
        {
            bodyFrameReader.Dispose();
            bodyFrameReader = null;
        }

        if (kinectSensor != null)
        {
            if (kinectSensor.IsOpen)
            {
                kinectSensor.Close();
            }
            kinectSensor = null;
        }
    }
}
