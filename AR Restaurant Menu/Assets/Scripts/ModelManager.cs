using UnityEngine;

// Manages the current 3D model instance in the scene.
public static class ModelManager
{
    public static GameObject currentModel; // Reference to the current model.

    // Removes and destroys the current model from the scene.
    public static void RemoveModel()
    {
        if (currentModel != null)
        {
            GameObject.Destroy(currentModel);
            currentModel = null;
            Debug.Log("3D model destroyed by ModelManager.");
        }
        else
        {
            Debug.Log("No model to destroy.");
        }
    }
}