using UnityEngine;
using TMPro;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Components;

// Switches the font of a TextMeshProUGUI component based on the selected locale.
[RequireComponent(typeof(TextMeshProUGUI))]
public class FontSwitcher : MonoBehaviour
{
    public TMP_FontAsset allFont; // Default font for all languages.
    public TMP_FontAsset arabicFont; // Font for Arabic locale.
    public TMP_FontAsset hindiFont; // Font for Hindi locale.
    public TMP_FontAsset chineseFont; // Font for Chinese locale.
    public TMP_FontAsset japaneseFont; // Font for Japanese locale.

    private TextMeshProUGUI textComponent; // Reference to the TextMeshProUGUI component.
    private LocalizeStringEvent localizer; // Reference to the LocalizeStringEvent component.

    // Initialize references and subscribe to localization updates.
    void Awake()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
        localizer = GetComponent<LocalizeStringEvent>();

        if (localizer != null)
        {
            // Subscribe so font updates when text updates
            localizer.OnUpdateString.AddListener(OnLocalizedTextChanged);
        }
        else
        {
            Debug.LogWarning($"FontSwitcher: No LocalizeStringEvent found on {gameObject.name}");
        }
    }

    // Unsubscribe from localization updates.
    void OnDestroy()
    {
        if (localizer != null)
        {
            localizer.OnUpdateString.RemoveListener(OnLocalizedTextChanged);
        }
    }

    // Set font once at startup.
    void OnEnable()
    {
        UpdateFont();
    }

    // Called when the localized text changes.
    void OnLocalizedTextChanged(string updatedText)
    {
        UpdateFont();
    }

    // Updates the font based on the current locale.
    void UpdateFont()
    {
        string code = LocalizationSettings.SelectedLocale.Identifier.Code;

        // Exact locale matching
        switch (code)
        {
            case "ar-SA":
                if (arabicFont != null)
                    textComponent.font = arabicFont;
                break;
            case "hi-IN":
                if (hindiFont != null)
                    textComponent.font = hindiFont;
                break;
            case "zh-CN":
            case "zh-TW":
                if (chineseFont != null)
                    textComponent.font = chineseFont;
                break;
            case "ja-JP":
                if (japaneseFont != null)
                    textComponent.font = japaneseFont;
                break;
            default:
                if (allFont != null)
                    textComponent.font = allFont;
                break;
        }
    }
}