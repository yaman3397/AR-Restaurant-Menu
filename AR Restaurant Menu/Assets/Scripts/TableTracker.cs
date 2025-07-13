using UnityEngine;
using Vuforia;

// Handles table marker tracking and assigns marker for dish placement.
public class TableTracker : MonoBehaviour
{
    public HelpOverlayController helpOverlay; // Reference to help overlay controller.

    void Start()
    {
        Debug.Log("TableTracker Start called.");

        var observer = GetComponent<ObserverBehaviour>();
        if (observer == null)
        {
            Debug.LogError("ObserverBehaviour not found on Table_Marker!");
            return;
        }

        // Subscribe to target status changes.
        observer.OnTargetStatusChanged += (o, status) =>
        {
            Debug.Log($"TableTracker Status: {status.Status}");

            if (status.Status == Status.TRACKED || status.Status == Status.EXTENDED_TRACKED)
            {
                DishTapTrigger.tableMarker = this.transform;
                Debug.Log("Table marker detected and assigned.");

                helpOverlay.ShowOverlay(HelpOverlayController.HelpOverlayType.TapIcon);
            }
            else if (DishTapTrigger.tableMarker == this.gameObject)
            {
                DishTapTrigger.tableMarker = null;
                Debug.Log("Table marker lost.");
            }

        };
    }
}