using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Localization.Components;

// Manages the UI for displaying menu items, ingredients, nutrition, tags, and ratings.
public class MenuUIManager : MonoBehaviour
{
    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI titleText; // Dish title text
    [SerializeField] private TextMeshProUGUI subtitleText; // Dish subtitle text
    [SerializeField] private TextMeshProUGUI descriptionText; // Dish description text

    [Header("Localization")]
    [SerializeField] private LocalizeStringEvent titleLocalizer; // Localizer for title
    [SerializeField] private LocalizeStringEvent subtitleLocalizer; // Localizer for subtitle
    [SerializeField] private LocalizeStringEvent descriptionLocalizer; // Localizer for description

    [Header("Containers")]
    [SerializeField] private Transform ingredientsContainer; // Container for ingredient items
    [SerializeField] private Transform nutritionContainer; // Container for nutrition items

    [Header("Templates")]
    [SerializeField] private GameObject ingredientTemplate; // Ingredient item template
    [SerializeField] private GameObject nutritionTemplate; // Nutrition item template

    [Header("Tags")]
    public Transform tagPanel; // Parent for tag icons
    public GameObject tagIconPrefab; // Tag icon template

    [SerializeField] private Transform nutritionScoreContainer; // Container for nutrition scores
    [SerializeField] private GameObject nutritionScoreTemplate; // Nutrition score template

    [SerializeField] private Transform starContainer; // Container for star rating
    [SerializeField] private GameObject starTemplate; // Star rating template

    private List<DishData> dishes; // List of loaded dishes

    // Loads dish data and prepares templates.
    private void Awake()
    {
        dishes = MenuDataLoader.LoadMenuData();

        if (dishes == null || dishes.Count == 0)
        {
            Debug.LogError("No dishes loaded!");
            return;
        }

        ingredientTemplate.SetActive(false);
        nutritionTemplate.SetActive(false);

        Debug.Log("MenuUIManager ready. Waiting for Refresh() call.");
    }

    // Refreshes the UI to show the selected dish.
    public void Refresh()
    {
        if (string.IsNullOrEmpty(DishTapTrigger.SelectedDishId))
        {
            Debug.LogWarning("SelectedDishId is empty.");
            return;
        }

        Debug.Log("Refresh() called - loading dish id: " + DishTapTrigger.SelectedDishId);
        ShowDishById(DishTapTrigger.SelectedDishId);
    }

    // Displays the dish details in the UI.
    public void DisplayDish(DishData dish)
    {
        Debug.Log($"Displaying dish: {dish.name}");

        // Title
        if (titleLocalizer != null)
        {
            titleLocalizer.StringReference.TableReference = "MenuTranslation";
            titleLocalizer.StringReference.TableEntryReference = $"{dish.id}_title";
        }
        else
        {
            titleText.text = dish.name;
        }

        // Subtitle
        if (subtitleLocalizer != null)
        {
            subtitleLocalizer.StringReference.TableReference = "MenuTranslation";
            subtitleLocalizer.StringReference.TableEntryReference = $"{dish.id}_subtitle";
        }
        else
        {
            subtitleText.text = dish.subtitle;
        }

        // Description
        if (descriptionLocalizer != null)
        {
            descriptionLocalizer.StringReference.TableReference = "MenuTranslation";
            descriptionLocalizer.StringReference.TableEntryReference = $"{dish.id}_description";
        }
        else
        {
            descriptionText.text = dish.description;
        }

        // Clear containers
        ClearContainer(ingredientsContainer, ingredientTemplate);
        ClearContainer(nutritionContainer, nutritionTemplate);

        // Ingredients
        foreach (var ingredient in dish.ingredients)
        {
            var go = Instantiate(ingredientTemplate, ingredientsContainer);
            go.SetActive(true);
            PopulateItem(go, ingredient.icon, $"ingredient_{ingredient.icon}", ingredient.name, ingredient.amount);
        }
        PopulateTags(dish.tags);
        PopulateNutritionScore(dish.nutritionValue);
        PopulateStarRating(dish.rating);

        // Nutrition
        foreach (var nutrition in dish.nutrition)
        {
            var go = Instantiate(nutritionTemplate, nutritionContainer);
            go.SetActive(true);
            PopulateItem(go, nutrition.icon, $"nutrition_{nutrition.icon}", nutrition.name, nutrition.amount);
        }
    }

    // Populates an ingredient or nutrition item UI.
    private void PopulateItem(GameObject go, string iconName, string keyBase, string fallbackName, string fallbackAmount)
    {
        var refs = go.GetComponent<UIItemRefs>();
        if (refs == null)
        {
            Debug.LogError("Prefab missing UIItemRefs!");
            return;
        }

        // Icon
        Sprite iconSprite = Resources.Load<Sprite>($"Icons/{iconName}");
        refs.iconImage.sprite = iconSprite;
        refs.iconImage.enabled = iconSprite != null;

        // Name localization
        if (refs.nameLocalizer != null)
        {
            refs.nameLocalizer.StringReference.TableReference = "MenuTranslation";
            refs.nameLocalizer.StringReference.TableEntryReference = keyBase;
        }
        else
        {
            refs.nameText.text = fallbackName;
            refs.nameText.enabled = true;
        }

        // Quantity plain text only
        refs.quantityText.text = fallbackAmount;
        refs.quantityText.enabled = true;
    }

    // Clears all children except the template from a container.
    private void ClearContainer(Transform container, GameObject template)
    {
        foreach (Transform child in container)
        {
            if (child.gameObject != template)
                Destroy(child.gameObject);
        }
    }

    // Populates tag icons for the dish.
    private void PopulateTags(List<string> tags)
    {
        if (tagPanel == null || tagIconPrefab == null)
        {
            Debug.LogWarning("Tag panel or prefab not assigned.");
            return;
        }

        // Clear previous clones
        ClearContainer(tagPanel, tagIconPrefab);

        // Make sure the original template is hidden
        tagIconPrefab.SetActive(false);

        if (tags == null || tags.Count == 0)
            return;

        foreach (var tag in tags)
        {
            var tagGO = Instantiate(tagIconPrefab, tagPanel);
            tagGO.SetActive(true);

            // Load sprite
            Sprite tagSprite = Resources.Load<Sprite>($"Icons/{tag}");
            var image = tagGO.GetComponent<Image>();

            if (image != null)
            {
                if (tagSprite != null)
                {
                    image.sprite = tagSprite;
                    image.enabled = true;
                }
                else
                {
                    Debug.LogWarning($"Tag icon not found: {tag}");
                    image.enabled = false;
                }
            }
            else
            {
                Debug.LogWarning("Tag prefab missing Image component.");
            }
        }
    }

    // Populates nutrition score icons for the dish.
    private void PopulateNutritionScore(List<string> scores)
    {
        if (nutritionScoreContainer == null || nutritionScoreTemplate == null)
        {
            Debug.LogWarning("Nutrition score container or template not assigned.");
            return;
        }

        // Clear previous
        foreach (Transform child in nutritionScoreContainer)
        {
            if (child.gameObject != nutritionScoreTemplate)
                Destroy(child.gameObject);
        }

        nutritionScoreTemplate.SetActive(false);

        if (scores == null || scores.Count == 0)
            return;

        foreach (var score in scores)
        {
            var go = Instantiate(nutritionScoreTemplate, nutritionScoreContainer);
            go.SetActive(true);

            var image = go.GetComponent<Image>();
            if (image != null)
            {
                Sprite scoreSprite = Resources.Load<Sprite>($"Icons/{score}");
                if (scoreSprite != null)
                {
                    image.sprite = scoreSprite;
                    image.enabled = true;
                }
                else
                {
                    Debug.LogWarning($"Nutrition score sprite not found: {score}");
                    image.enabled = false;
                }
            }
            else
            {
                Debug.LogWarning("Nutrition score prefab missing Image component.");
            }
        }
    }

    // Populates the star rating for the dish.
    private void PopulateStarRating(float rating)
    {
        if (starContainer == null || starTemplate == null)
        {
            Debug.LogWarning("Star container/template not assigned.");
            return;
        }

        // Clean previous stars
        foreach (Transform child in starContainer)
        {
            if (child.gameObject != starTemplate)
                Destroy(child.gameObject);
        }

        // Load sprite based on rating
        string spriteName = $"{rating.ToString("0.0")}";
        Sprite ratingSprite = Resources.Load<Sprite>($"Icons/{spriteName}");

        if (ratingSprite == null)
        {
            Debug.LogWarning($"Rating sprite not found: {spriteName}");
            return;
        }

        // Instantiate star image
        GameObject starGO = Instantiate(starTemplate, starContainer);
        starGO.SetActive(true);
        starTemplate.SetActive(false); // Hide the template
        var img = starGO.GetComponent<Image>();
        if (img != null)
        {
            img.sprite = ratingSprite;
            img.enabled = true;
        }
    }

    // Finds and displays a dish by its id.
    public void ShowDishById(string id)
    {
        if (dishes == null)
        {
            Debug.LogError("Dishes not loaded!");
            return;
        }

        DishData dish = dishes.Find(d => d.id == id);
        if (dish != null)
        {
            DisplayDish(dish);
        }
        else
        {
            Debug.LogWarning($"Dish with id '{id}' not found.");
        }
    }
}