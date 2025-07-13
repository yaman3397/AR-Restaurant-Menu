using UnityEngine;
using UnityEngine.EventSystems;

// Handles tap interactions for spawning dish models and managing menu UI.
public class DishTapTrigger : MonoBehaviour
{
    [Header("References")]
    public GameObject modelPrefab; // Prefab for the dish model to spawn.
    public static Transform tableMarker; // Reference to the table marker for model placement.
    public GameObject menuMarkers; // UI markers for menu selection.
    public GameObject mainMenuButton; // Button to open the main menu.
    public GameObject mainMenuCanvas; // Canvas for the main menu UI.
    public HelpOverlayController helpOverlay; // Reference to help overlay controller.
    public MenuUIManager menuUIManager; // Reference to menu UI manager.

    [Header("Spawn Settings")]
    public Vector3 modelOffset = new Vector3(0, 0.05f, 0); // Offset for model placement.

    [Header("Dish Data")]
    public string dishId; // Unique identifier for the dish.

    // Handles touch and mouse input for tapping the dish icon.
    void Update()
    {
#if UNITY_ANDROID || UNITY_IOS
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                if (EventSystem.current.IsPointerOverGameObject(touch.fingerId)) return;
                HandleTap(touch.position);
            }
        }
#endif

#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;
            HandleTap(Input.mousePosition);
        }
#endif
    }

    // Handles tap logic and spawns the dish model if tapped.
    void HandleTap(Vector2 screenPos)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPos);
        if (Physics.Raycast(ray, out RaycastHit hit) && hit.transform == this.transform)
        {
            Debug.Log("Dish icon tapped!");

            // Store selected dish id
            DishTapTrigger.SelectedDishId = dishId;
            Debug.Log("SelectedDishId set to: " + SelectedDishId);

            if (menuMarkers != null)
                menuMarkers.SetActive(false);

            SpawnModel();

            if (mainMenuButton != null)
            {
                mainMenuButton.SetActive(true);
                helpOverlay.ShowOverlay(HelpOverlayController.HelpOverlayType.RotateModel);
                helpOverlay.ShowOverlay(HelpOverlayController.HelpOverlayType.ZoomModel);
            }
        }
    }

    // Spawns the dish model at the table marker.
    void SpawnModel()
    {
        if (tableMarker == null)
        {
            Debug.LogError("Table Marker not assigned!");
            return;
        }

        foreach (Transform child in tableMarker)
            GameObject.Destroy(child.gameObject);

        ModelManager.currentModel = Instantiate(modelPrefab, tableMarker);
        ModelManager.currentModel.transform.localPosition = modelOffset;
        ModelManager.currentModel.transform.localRotation = Quaternion.identity;

        Debug.Log("3D model spawned on table marker: " + ModelManager.currentModel.name);
    }

    // Opens the main menu and refreshes the UI.
    public void OpenMainMenu()
    {
        // Remove the 3D model
        ModelManager.RemoveModel();

        // Show the menu canvas
        if (mainMenuCanvas != null)
            mainMenuCanvas.SetActive(true);
        else
            Debug.LogError("Main Menu Canvas is missing!");

        // Call Refresh() to load the dish dynamically
        if (menuUIManager != null)
            menuUIManager.Refresh();
        else
            Debug.LogError("MenuUIManager reference is missing!");

        // Hide the menu markers
        if (menuMarkers != null)
            menuMarkers.SetActive(false);

        // Hide the main menu button
        if (mainMenuButton != null)
            mainMenuButton.SetActive(false);

        Debug.Log("Main menu opened.");
    }

    // Closes the main menu and re-enables menu markers.
    public void CloseMainMenu()
    {
        if (mainMenuCanvas != null)
            mainMenuCanvas.SetActive(false);

        if (menuMarkers != null)
            menuMarkers.SetActive(true);

        if (mainMenuButton != null)
            mainMenuButton.SetActive(false);

        if (ModelManager.currentModel != null)
            ModelManager.RemoveModel();

        Debug.Log("Main menu closed and menu markers re-enabled.");
    }

    // Static field to hold the selected dish id.
    public static string SelectedDishId;
}