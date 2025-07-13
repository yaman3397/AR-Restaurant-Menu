using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.EventSystems;
using System.Collections.Generic;

// Handles tap interactions for placing and moving dish models in AR.
public class DishTapHandler : MonoBehaviour
{
    // Prefab for the dish model to spawn.
    public GameObject modelPrefab;

    // UI panel displaying dish information.
    public GameObject infoPanel;

    // AR Raycast Manager for detecting planes.
    public ARRaycastManager raycastManager;

    // Reference to the currently spawned dish model.
    private GameObject spawnedModel;

    // Indicates if placement mode is active.
    private bool isPlacementMode = false;

    // Static list to store raycast hits.
    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    // Handles touch and mouse input for placing and moving the dish model.
    void Update()
    {
        if (isPlacementMode && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Vector2 touchPosition = Input.GetTouch(0).position;

            // Check if we tapped a UI element (ignore if so)
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                return;

            if (raycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
            {
                Pose hitPose = hits[0].pose;

                if (spawnedModel == null)
                {
                    spawnedModel = Instantiate(modelPrefab, hitPose.position, hitPose.rotation);
                }
                else
                {
                    spawnedModel.transform.position = hitPose.position;
                }

                infoPanel?.SetActive(true);
                isPlacementMode = false; // Disable placement after one tap
            }
        }
        else if (!isPlacementMode && Input.GetMouseButtonDown(0))
        {
            // Mouse click support (Editor)
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform == this.transform)
                {
                    Debug.Log("Icon clicked – now enter placement mode.");
                    isPlacementMode = true;
                }
            }
        }
    }
}

