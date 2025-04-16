using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public InputAction MoveAction;
    
    public float turnSpeed = 20f;

    Animator m_Animator;
    Rigidbody m_Rigidbody;
    AudioSource m_AudioSource;
    Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity;

    public float detectionAngle = 45f; // Angle of detection cone
    public GameObject[] ghosts; // Assign in Inspector

    public AudioSource whisperSound; // Assign in Inspector
    public float whisperDistance = 4f; // Distance to trigger sound


    void Start ()
    {
        m_Animator = GetComponent<Animator> ();
        m_Rigidbody = GetComponent<Rigidbody> ();
        m_AudioSource = GetComponent<AudioSource> ();
        
        MoveAction.Enable();
    }

    void FixedUpdate ()
    {
        var pos = MoveAction.ReadValue<Vector2>();
        
        float horizontal = pos.x;
        float vertical = pos.y;
        
        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement.Normalize ();

        bool hasHorizontalInput = !Mathf.Approximately (horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately (vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;
        m_Animator.SetBool ("IsWalking", isWalking);
        
        if (isWalking)
        {
            if (!m_AudioSource.isPlaying)
            {
                m_AudioSource.Play();
            }
        }
        else
        {
            m_AudioSource.Stop ();
        }

        Vector3 desiredForward = Vector3.RotateTowards (transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
        m_Rotation = Quaternion.LookRotation (desiredForward);
    }

    void OnAnimatorMove ()
    {
        m_Rigidbody.MovePosition (m_Rigidbody.position + m_Movement * m_Animator.deltaPosition.magnitude);
        m_Rigidbody.MoveRotation (m_Rotation);
    }

    void Update() // Changed to Update for real-time detection
    {
        CheckGhostDetection();
        CheckGhostProximity();
    }

    void CheckGhostDetection()
    {
        Vector3 playerForward = transform.forward;
        foreach (GameObject ghost in ghosts)
        {
            if (ghost != null && ghost.activeSelf)
            {
                Vector3 directionToGhost = (ghost.transform.position - transform.position).normalized;
                float dotProduct = Vector3.Dot(playerForward, directionToGhost);
                float angle = Mathf.Acos(dotProduct) * Mathf.Rad2Deg;

                if (angle <= detectionAngle)
                {
                    Debug.Log($"Detected {ghost.name} at angle {angle:F2}°");
                }
            }
        }
    }

    void CheckGhostProximity()
    {
        bool shouldPlay = false;
        foreach (GameObject ghost in ghosts)
        {
            if (ghost != null && ghost.activeSelf)
            {
                float distance = Vector3.Distance(transform.position, ghost.transform.position);
                if (distance <= whisperDistance)
                {
                    shouldPlay = true;
                    break; // Play if any ghost is close
                }
            }
        }

        if (shouldPlay && !whisperSound.isPlaying)
        {
            whisperSound.Play();
            Debug.Log("Creepy whisper playing");
        }
        else if (!shouldPlay && whisperSound.isPlaying)
        {
            whisperSound.Stop();
            Debug.Log("Creepy whisper stopped");
        }
    }
}
