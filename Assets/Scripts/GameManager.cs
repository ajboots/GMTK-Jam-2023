using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private Camera cam;

    [SerializeField]
    private float minCamSize = 0.5f;

    [SerializeField]
    private float maxCamSize = 3;

    public GameObject player;

    private void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        doCameraMovement();
    }

    private void doCameraMovement()
    {
        cam.transform.position = new Vector3(
            player.transform.position.x,
            player.transform.position.y,
            cam.transform.position.z
        );

        if (Input.mouseScrollDelta != Vector2.zero)
        {
            cam.orthographicSize += -.1f * Input.mouseScrollDelta.y;

            if (cam.orthographicSize >= maxCamSize)
                cam.orthographicSize = maxCamSize;

            if (cam.orthographicSize <= minCamSize)
                cam.orthographicSize = minCamSize;
        }
    }
}
