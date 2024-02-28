using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class getGridMap : MonoBehaviour
{
    public GameObject sourceGrid; // Reference to the source grid GameObject
    public GameObject destinationGrid; // Reference to the destination grid GameObject
    public string childObjectName = "Walls";
    public TilemapRenderer tilemapRenderer;
    private Vector3 newScale = new Vector3(4f, 4f, 0f);
    public GameObject background;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void getCurrentGridMap()
    {

        background.SetActive(false);
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
            copy.transform.position = new Vector3(topRightCorner.x - 0.75f, topRightCorner.y - 0.75f, transform.position.z);
            copy.transform.localScale = newScale;
            tilemapRenderer = copy.GetComponent<TilemapRenderer>();
            tilemapRenderer.sortingOrder = 2;
        }
        else
        {
            Debug.LogWarning("Child object not found in the source grid.");
        }
    }
}
