using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

// Manages language selection using a dropdown and sets the locale for localization.
public class LanguageSelector : MonoBehaviour
{
    public TMP_Dropdown dropdown; // Reference to the dropdown UI component.

    // Fixed list of 17 locales and their display names in desired order.
    private static readonly List<(string code, string displayName)> locales = new List<(string, string)>
    {
        ("ar-SA", "Arabic"),
        ("bg-BG", "Bulgarian"),
        ("zh-CN", "Chinese Simplified"),
        ("zh-TW", "Chinese Traditional"),
        ("cs-CZ", "Czech"),
        ("nl-BE", "Dutch (Belgium)"),
        ("nl-NL", "Dutch"),
        ("en-GB", "English"),
        ("fr-FR", "French"),
        ("de-DE", "German"),
        ("hi-IN", "Hindi"),
        ("it-IT", "Italian"),
        ("ja-JP", "Japanese"),
        ("pl-PL", "Polish"),
        ("ru-RU", "Russian"),
        ("es-ES", "Spanish"),
        ("sv-SE", "Swedish")
    };

    // Initializes the dropdown and sets the saved or default locale.
    void Start()
    {
        dropdown.ClearOptions();
        var options = new List<string>();
        foreach (var entry in locales)
        {
            options.Add(entry.displayName);
        }
        dropdown.AddOptions(options);

        // Load saved index
        int savedIndex = PlayerPrefs.GetInt("SelectedLanguage", 0);

        // Clamp to valid range
        if (savedIndex < 0 || savedIndex >= locales.Count)
            savedIndex = 0;

        // Find the Locale object with the corresponding code
        string selectedCode = locales[savedIndex].code;
        Locale selectedLocale = null;
        foreach (var loc in LocalizationSettings.AvailableLocales.Locales)
        {
            if (loc.Identifier.Code == selectedCode)
            {
                selectedLocale = loc;
                break;
            }
        }
        if (selectedLocale != null)
            LocalizationSettings.SelectedLocale = selectedLocale;
        dropdown.value = savedIndex;

        dropdown.onValueChanged.AddListener(SetLanguage);
    }

    // Sets the locale based on the selected dropdown index and saves the selection.
    void SetLanguage(int index)
    {
        // Clamp to valid range
        if (index < 0 || index >= locales.Count)
            index = 0;
        string code = locales[index].code;
        Locale localeToSet = null;
        foreach (var loc in LocalizationSettings.AvailableLocales.Locales)
        {
            if (loc.Identifier.Code == code)
            {
                localeToSet = loc;
                break;
            }
        }
        if (localeToSet != null)
            LocalizationSettings.SelectedLocale = localeToSet;
        PlayerPrefs.SetInt("SelectedLanguage", index);
    }
}