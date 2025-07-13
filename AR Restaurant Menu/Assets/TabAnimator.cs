using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TabAnimator : MonoBehaviour
{
    public Toggle toggle;
    public Vector3 activeScale = Vector3.one * 1.1f;
    public Vector3 inactiveScale = Vector3.one;
    public float animationSpeed = 5f;

    void Awake()
    {
        toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(OnToggleChanged);

        // Initialize scale
        transform.localScale = toggle.isOn ? activeScale : inactiveScale;
    }

    void OnToggleChanged(bool isOn)
    {
        if (isOn)
        {
            // Animate this toggle to active
            StopAllCoroutines();
            StartCoroutine(AnimateScale(activeScale));

            // Deactivate others in group
            foreach (var sibling in transform.parent.GetComponentsInChildren<TabAnimator>())
            {
                if (sibling != this)
                {
                    sibling.Deactivate();
                }
            }
        }
    }

    public void Deactivate()
    {
        StopAllCoroutines();
        StartCoroutine(AnimateScale(inactiveScale));
    }

    IEnumerator AnimateScale(Vector3 target)
    {
        while (Vector3.Distance(transform.localScale, target) > 0.01f)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, target, Time.deltaTime * animationSpeed);
            yield return null;
        }
        transform.localScale = target;
    }
}