using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform target;
    public Tilemap thisMap;

    private Vector3 botomLeftLimit;
    private Vector3 topRightLimit;

    private float halfWidth;
    private float halfHeight;

    public int musicId;
    private bool musicStated;

    PlayerLoader ploader;
    void Start()
    {
        target = FindObjectOfType<PlayerController>().transform;

        halfHeight = Camera.main.orthographicSize;
        halfWidth = halfHeight * Camera.main.aspect;

        botomLeftLimit = thisMap.localBounds.min + new Vector3(halfWidth, halfHeight, 0f);
        topRightLimit = thisMap.localBounds.max + new Vector3(-halfWidth, -halfHeight, 0f);
        AudioManager.instance.stopMusic();
        AudioManager.instance.swichSound();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // LaterUpdate is called once per frame after Update

    private void LateUpdate()
    {
        if(target == null)
        {
            target = FindObjectOfType<PlayerController>().transform;
        }

        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, botomLeftLimit.x, topRightLimit.x), 
                                        Mathf.Clamp(transform.position.y, botomLeftLimit.y, topRightLimit.y), 
                                        transform.position.z);


    }
}
