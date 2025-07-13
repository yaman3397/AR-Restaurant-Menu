using UnityEngine;
using System.Collections.Generic;

// Loads menu data from a JSON file in the Resources folder.
public static class MenuDataLoader
{
    // Loads and parses the menu data JSON file, returning a list of DishData.
    public static List<DishData> LoadMenuData()
    {
        TextAsset jsonData = Resources.Load<TextAsset>("MenuData");
        if (jsonData == null)
        {
            Debug.LogError("MenuData.json not found in Resources!");
            return null;
        }

        DishDataList dataList = JsonUtility.FromJson<DishDataList>(jsonData.text);
        return dataList.dishes;
    }
}