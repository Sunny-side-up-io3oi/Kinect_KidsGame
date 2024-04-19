using System.Diagnostics;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 100f; 

    void Update()
    {
        
        float horizontalInput = Input.GetAxis("Horizontal");

        
        Vector3 movement = new Vector3(horizontalInput, 0f, 0f) * moveSpeed * Time.deltaTime;
        transform.Translate(movement);
        
    }
}
