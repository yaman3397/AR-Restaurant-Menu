using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Localization.Components;

// Holds references to UI components for ingredient and nutrition items.
public class UIItemRefs : MonoBehaviour
{
    public Image iconImage; // Icon image for the item.
    public TextMeshProUGUI nameText; // Name text for the item.
    public TextMeshProUGUI quantityText; // Quantity text for the item.

    public LocalizeStringEvent nameLocalizer; // Localizer for the name text.
    public LocalizeStringEvent quantityLocalizer; // Localizer for the quantity text.
}