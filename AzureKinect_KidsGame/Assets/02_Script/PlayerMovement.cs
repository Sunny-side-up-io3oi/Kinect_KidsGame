using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; 

    private Rigidbody rb; 

    private void Start()
    {
       
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        
        float horizontalInput = Input.GetAxis("Horizontal");

        
        if (Mathf.Abs(horizontalInput) > 0.1f)
        {
            
            Vector3 movement = new Vector3(horizontalInput, 0f, 0f) * moveSpeed * Time.deltaTime;

            
            RaycastHit hit;
            if (!Physics.Raycast(transform.position, movement.normalized, out hit, movement.magnitude))
            {
                
                rb.MovePosition(transform.position + movement);
            }
        }
    }
}
