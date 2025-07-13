using UnityEngine;

// Manages the spawning of dish models and info panels in the scene.
public class DishSpawnManager : MonoBehaviour
{
    // Singleton instance of the DishSpawnManager.
    public static DishSpawnManager instance;

    // Prefab for the dish model to spawn.
    public GameObject modelPrefab;

    // UI panel displaying dish information.
    public GameObject infoPanel;

    // Reference to the currently spawned dish model.
    private GameObject spawnedModel;

    // Set the singleton instance on Awake.
    void Awake()
    {
        instance = this;
    }

    // Spawns the dish model in front of the camera if not already spawned.
    public void SpawnDishModel()
    {
        if (spawnedModel != null) return;

        Vector3 spawnPos = Camera.main.transform.position + Camera.main.transform.forward * 0.3f;
        spawnedModel = Instantiate(modelPrefab, spawnPos, Quaternion.LookRotation(Camera.main.transform.forward));
        //spawnedModel.transform.localScale = Vector3.one * 0.5f;
        spawnedModel.transform.SetParent(null); // extra safety

        if (infoPanel != null)
            infoPanel.SetActive(true);

        Debug.Log("Model spawned by Scene Manager");
    }
}