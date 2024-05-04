using UnityEngine;

public class IconIndicator : MonoBehaviour
{
    public Transform target; // The actual world space position of the icon
    public Camera xrCamera; // The main XR camera
    public RectTransform iconRectTransform; // The RectTransform of the icon (this should be a separate UI element from the main icon)
    public float minIconSize = 50f; // Minimum size of the icon when off-screen
    public float maxIconSize = 100f; // Normal size when in view

    private Vector2 onScreenSize; // The RectTransform size when the icon is on screen

    void Start()
    {
        onScreenSize = iconRectTransform.sizeDelta; // Store the original size
    }

    void Update()
    {
        Vector3 viewportPosition = xrCamera.WorldToViewportPoint(target.position);
        bool isOnScreen = viewportPosition.z > 0 && viewportPosition.x >= 0 && viewportPosition.x <= 1 && viewportPosition.y >= 0 && viewportPosition.y <= 1;

        // Adjust these values based on the parent Canvas scale
        float adjustedMaxSize = maxIconSize / 0.01f; // Adjusting for Canvas scale
        float adjustedMinSize = minIconSize / 0.01f; // Adjusting for Canvas scale

        if (isOnScreen)
        {
            iconRectTransform.position = xrCamera.WorldToScreenPoint(target.position);
            iconRectTransform.sizeDelta = new Vector2(adjustedMaxSize, adjustedMaxSize);
        }
        else
        {
            // Ensure icon is repositioned within viewable boundaries when off-screen
            viewportPosition.x = Mathf.Clamp(viewportPosition.x, 0.05f, 0.95f);
            viewportPosition.y = Mathf.Clamp(viewportPosition.y, 0.05f, 0.95f);
            Vector3 edgePosition = xrCamera.ViewportToScreenPoint(viewportPosition);
            iconRectTransform.position = edgePosition;
            iconRectTransform.sizeDelta = new Vector2(adjustedMinSize, adjustedMinSize);
        }
    }

}
