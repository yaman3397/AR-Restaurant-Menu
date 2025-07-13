using System;
using System.Collections.Generic;

// Represents the data for a single dish, including its ingredients, nutrition, tags, and rating.
[Serializable]
public class DishData
{
    // Unique identifier for the dish.
    public string id;

    // Name of the dish.
    public string name;

    // Subtitle or short description for the dish.
    public string subtitle;

    // Detailed description of the dish.
    public string description;

    // List of ingredients used in the dish.
    public List<IngredientData> ingredients;

    // List of nutrition facts for the dish.
    public List<NutritionData> nutrition;

    // List of tags associated with the dish (e.g., vegan, spicy).
    public List<string> tags;

    // User rating for the dish.
    public float rating;

    // List of nutrition values for the dish.
    public List<string> nutritionValue;
}

// Represents a single ingredient in a dish.
[Serializable]
public class IngredientData
{
    // Name of the ingredient.
    public string name;

    // Amount of the ingredient.
    public string amount;

    // Icon representing the ingredient.
    public string icon;
}

// Represents a single nutrition fact for a dish.
[Serializable]
public class NutritionData
{
    // Name of the nutrition fact.
    public string name;

    // Amount of the nutrition fact.
    public string amount;

    // Icon representing the nutrition fact.
    public string icon;
}

// Represents a list of dishes.
[Serializable]
public class DishDataList
{
    // Collection of dishes.
    public List<DishData> dishes;
}