using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getGridMap : MonoBehaviour
{
    public GameObject sourceGrid; // Reference to the source grid GameObject
    public GameObject destinationGrid; // Reference to the destination grid GameObject
    public string childObjectName = "Walls";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void getCurrentGridMap()
    {
        Camera mainCamera = Camera.main;
        Transform originalChild = sourceGrid.transform.Find("Walls");

        // If the child object is found, replace the child in the destination grid
        if (originalChild != null)
        {
            // Find the existing child in the destination grid
            Transform existingChild = destinationGrid.transform.Find(childObjectName);

            // If an existing child is found, destroy it
            if (existingChild != null)
            {
                Destroy(existingChild.gameObject);
            }

            // Instantiate a copy of the child object
            GameObject copy = Instantiate(originalChild.gameObject);

            // Set the parent of the copy to the destination grid
            copy.transform.SetParent(destinationGrid.transform, false);
            

            // Calculate the position of the top right corner of the screen in world space
            Vector3 topRightCorner = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane));

            // Set the position of the GameObject to the calculated position
            copy.transform.position = new Vector3(topRightCorner.x - 2f, topRightCorner.y - 2f, transform.position.z);
        }
        else
        {
            Debug.LogWarning("Child object not found in the source grid.");
        }
    }
}
