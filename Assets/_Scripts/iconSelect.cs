using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.AI;

public class iconSelect : MonoBehaviour, IPointerClickHandler
{
    public LineRenderer lineRenderer;
    public Transform player; // Reference to the player object

    void Start()
    {
        // Initialize the line renderer
        lineRenderer = GetComponent<LineRenderer>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Print a message to the console indicating the icon has been selected
        Debug.Log("Icon selected: " + gameObject.name);

        // Get the destination position (position of the clicked icon)
        Vector3 destination = transform.position;

        // Calculate the path using NavMesh
        NavMeshPath path = new NavMeshPath();
        NavMesh.CalculatePath(player.position, destination, NavMesh.AllAreas, path);

        // Set the positions for the line renderer
        lineRenderer.positionCount = path.corners.Length;
        lineRenderer.SetPositions(path.corners);
    }
}
