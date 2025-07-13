using UnityEngine;
using UnityEngine.EventSystems;

// Handles tap interactions on icons to spawn dish models and show info panels.
public class IconTapHandler : MonoBehaviour
{
    [Header("Model Setup")]
    public GameObject modelPrefab;     // Assign your 3D model prefab
    public GameObject infoPanel;       // UI panel to show info

    private GameObject spawnedModel;   // Reference to the spawned model

    // Handles touch and mouse input for tapping the icon.
    void Update()
    {
        // Touch Support (Android/iOS builds)
#if UNITY_ANDROID || UNITY_IOS
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                return;

            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform == this.transform)
                {
                    TriggerModel();
                }
            }
        }
#endif

        // Mouse Support (Unity Editor)
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform == this.transform)
                {
                    TriggerModel();
                }
            }
        }
#endif
    }

    // Triggers the spawning of the dish model via the DishSpawnManager.
    void TriggerModel()
    {
        DishSpawnManager.instance.SpawnDishModel();
    }
}