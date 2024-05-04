using UnityEngine;

public class iconResize : MonoBehaviour
{
    public GameObject icon;
    public Transform playerTransform;
    public float activationRange = 5f;
    public float minScale = 0.5f;
    public float maxScale = 2.0f;
    public float minYOffset = 0f; // Minimum Y offset when the player is within range
    public float maxYOffset = 2f; // Maximum Y offset when the player is within range
    public float shrinkSpeed = 2.0f; // Speed at which the icon shrinks
    public float growSpeed = 2.0f;

    private Vector3 initialScale;
    private Vector3 initialPosition;

    void Start()
    {
        initialScale = icon.transform.localScale;
        initialPosition = transform.position;
    }

    void Update()
    {
        float distance = Vector3.Distance(playerTransform.position, transform.position);

        // Calculate scale multiplier based on distance and activation range
        float scaleMultiplier = Mathf.Clamp01((distance - activationRange) / activationRange);
        float targetScale = Mathf.Lerp(minScale, maxScale, scaleMultiplier);

        // Calculate speed based on whether the player is within or outside the activation range
        float speed = (distance < activationRange) ? shrinkSpeed : growSpeed;

        // Smoothly adjust icon scale towards the target scale
        icon.transform.localScale = Vector3.Lerp(icon.transform.localScale, initialScale * targetScale, Time.deltaTime * speed);

        // Calculate the Y offset based on the distance from the player and the activation range
        float yOffset = Mathf.Lerp(minYOffset, maxYOffset, Mathf.Clamp01(distance / activationRange));

        // Shift the icon's position upwards when shrinking
        if (distance < activationRange)
        {
            transform.position += new Vector3(0f, Time.deltaTime * speed * yOffset, 0f);
        }
        else
        {
            // Bring the icon back down when outside the range
            transform.position = Vector3.Lerp(transform.position, initialPosition, Time.deltaTime * growSpeed);
        }
    }
}
