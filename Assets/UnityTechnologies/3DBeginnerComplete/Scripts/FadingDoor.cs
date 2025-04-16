using UnityEngine;

public class FadingDoor : MonoBehaviour
{
    public float fadeSpeed = 2f; // Speed of fading
    private Material doorMaterial;
    private bool isFading = false;
    private float targetAlpha = 1f;
    private float currentAlpha = 1f;

    void Start()
    {
        doorMaterial = GetComponent<Renderer>().material;
        doorMaterial.color = new Color(doorMaterial.color.r, doorMaterial.color.g, doorMaterial.color.b, 1f); // Start opaque
    }

    void Update()
    {
        if (isFading)
        {
            currentAlpha = Mathf.Lerp(currentAlpha, targetAlpha, Time.deltaTime * fadeSpeed);
            doorMaterial.color = new Color(doorMaterial.color.r, doorMaterial.color.g, doorMaterial.color.b, currentAlpha);
            if (Mathf.Abs(currentAlpha - targetAlpha) < 0.01f)
            {
                isFading = false;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isFading = true;
            targetAlpha = 0f; // Fade to transparent
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isFading = true;
            targetAlpha = 1f; // Fade to opaque
        }
    }
}