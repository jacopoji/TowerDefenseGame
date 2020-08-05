using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float panSpeed = 30f;
    public float panBoundary = 10f;
    public float scrollSpeed = 10f;
    public float scrollBoundaryMin = 10f;
    public float scrollBoundaryMax = 80f;
    public bool disablePan = false;

    GameMaster gameMaster;
    public static Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
        gameMaster = GameMaster.instance;
    }
    // Update is called once per frame
    void Update()
    {
        if (disablePan)
            return;

        if (gameMaster.isGameOver)
            return;


        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height-panBoundary)
        {
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("s") || Input.mousePosition.y <= panBoundary)
        {
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("a") || Input.mousePosition.x <= panBoundary)
        {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBoundary)
        {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey(KeyCode.CapsLock))
        {
            disablePan = !disablePan;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            transform.position = startPos;
        }
        
        float scroll = -Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0)
        {
            Vector3 pos = transform.position;
            pos.y += scroll * 1000 * scrollSpeed * Time.deltaTime;
            pos.y = Mathf.Clamp(pos.y, scrollBoundaryMin, scrollBoundaryMax);
            transform.position = pos;
        }

    }
}
