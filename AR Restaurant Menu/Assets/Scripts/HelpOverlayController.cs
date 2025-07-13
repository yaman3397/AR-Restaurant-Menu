using UnityEngine;

// Manages help overlays for scanning, zooming, rotating, and tapping icons.
public class HelpOverlayController : MonoBehaviour
{
    [Header("Overlay Panels")]
    public GameObject scanMenuPanel; // Panel for scan menu help.
    public GameObject zoomModelPanel; // Panel for zoom model help.
    public GameObject tapIconPanel; // Panel for tap icon help.

    [Header("Overlay Animators")]
    public Animator scanMenuAnimator; // Animator for scan menu panel.
    public Animator zoomModelAnimator; // Animator for zoom model panel.
    public Animator tapIconAnimator; // Animator for tap icon panel.

    [Header("Settings")]
    public float autoHideSeconds = 3f; // Time before overlay auto-hides.

    private GameObject currentPanel = null; // Currently active panel.
    private Animator currentAnimator = null; // Currently active animator.

    // Types of help overlays.
    public enum HelpOverlayType
    {
        ScanMenu,
        ZoomModel,
        RotateModel,
        TapIcon
    }

    // Show scan menu overlay on start.
    void Start()
    {
        ShowOverlay(HelpOverlayType.ScanMenu);
    }

    // Shows the specified overlay and auto-hides after a delay.
    public void ShowOverlay(HelpOverlayType overlayType)
    {
        HideOverlay();

        switch (overlayType)
        {
            case HelpOverlayType.ScanMenu:
                currentPanel = scanMenuPanel;
                currentAnimator = scanMenuAnimator;
                break;
            case HelpOverlayType.ZoomModel:
                currentPanel = zoomModelPanel;
                currentAnimator = zoomModelAnimator;
                break;
            case HelpOverlayType.TapIcon:
                currentPanel = tapIconPanel;
                currentAnimator = tapIconAnimator;
                break;
        }

        if (currentPanel != null)
        {
            currentPanel.SetActive(true);
            if (currentAnimator != null)
                currentAnimator.Play(0, -1, 0f);

            CancelInvoke(nameof(HideOverlay));
            Invoke(nameof(HideOverlay), autoHideSeconds);
        }
    }

    // Hides the currently active overlay.
    public void HideOverlay()
    {
        if (currentPanel != null)
        {
            currentPanel.SetActive(false);
            currentPanel = null;
            currentAnimator = null;
        }
    }

    // Toggles the specified overlay on or off.
    public void ToggleOverlay(HelpOverlayType overlayType)
    {
        if (currentPanel != null)
        {
            HideOverlay();
        }
        else
        {
            ShowOverlay(overlayType);
        }
    }
}