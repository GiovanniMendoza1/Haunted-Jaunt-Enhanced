using UnityEngine;

public class HauntedHotspot : MonoBehaviour
{
    public GameObject auraPrefab; // Assign MysticalAura.prefab
    private ParticleSystem auraInstance;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (auraInstance == null)
            {
                // Instantiate aura at player's position
                GameObject auraObject = Instantiate(auraPrefab, other.transform);
                auraObject.transform.localPosition = Vector3.zero; // Center on player
                auraInstance = auraObject.GetComponent<ParticleSystem>();
            }
            if (!auraInstance.isPlaying)
            {
                auraInstance.Play();
                Debug.Log("Mystical aura activated");
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (auraInstance != null && auraInstance.isPlaying)
            {
                auraInstance.Stop();
                Debug.Log("Mystical aura stopped");
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && auraInstance != null)
        {
            // Keep aura centered on player
            auraInstance.transform.position = other.transform.position;
        }
    }
}