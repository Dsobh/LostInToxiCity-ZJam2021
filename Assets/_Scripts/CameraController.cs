using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private float cameraSpeed;
    private Vector3 destinationPosition;
    private Vector3 velocity = Vector3.zero;
    public float smoothTime = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        destinationPosition = new Vector3(Mathf.Clamp(player.transform.position.x, -10f, 105f), 
                                            Mathf.Clamp(player.transform.position.y, -50, 88),
                                            this.transform.position.z);
    }

    void LateUpdate()
    {
         this.transform.position = Vector3.Lerp(this.transform.position, destinationPosition, cameraSpeed);
         //this.transform.position = Vector3.SmoothDamp(this.transform.position, destinationPosition, ref velocity, smoothTime);
    }

}
