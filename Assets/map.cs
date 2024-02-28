using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class map : MonoBehaviour
{

    void Start()
    {
        // Get the camera component attached to the main camera
        Camera mainCamera = Camera.main;

        // Calculate the position of the top right corner of the screen in world space
        Vector3 topRightCorner = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane));

        // Set the position of the GameObject to the calculated position
        transform.position = new Vector3(topRightCorner.x-2f, topRightCorner.y-2f, transform.position.z);
    }

}
