using UnityEngine;

// Manages free tap spawning of models in front of the camera.
public class FreeTapSpawner : MonoBehaviour
{
    public static FreeTapSpawner instance; // Singleton instance.

    private GameObject spawnedModel; // Reference to the currently spawned model.

    // Set the singleton instance on Awake.
    void Awake()
    {
        instance = this;
    }

    // Spawns the model prefab in front of the camera if not already spawned.
    public void SpawnModel(GameObject prefab)
    {
        if (spawnedModel != null || prefab == null)
            return;

        Vector3 pos = Camera.main.transform.position + Camera.main.transform.forward * 0.3f;
        spawnedModel = Instantiate(prefab, pos, Quaternion.LookRotation(Camera.main.transform.forward));
        //spawnedModel.transform.localScale = Vector3.one * 0.5f;
        spawnedModel.transform.SetParent(null);
    }
}
