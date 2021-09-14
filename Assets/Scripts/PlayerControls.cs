using UnityEngine;
using UnityEngine.InputSystem;

// This Script uses Unity's new Input System
public class PlayerControls : MonoBehaviour
{
    [Header("General Setup Settings")]
    [Tooltip("How fast the ship moves across the screen")]
    [SerializeField] float offsetSpeed = 20f;
    [Tooltip("Right and left borders of moveable area")]
    [SerializeField] float xRange = 12f;
    [Tooltip("Top and bottum borders of moveable area")]
    [SerializeField] float yRange = 8f;
    [Tooltip("Ship's weapons ParticleSystem")]
    [SerializeField] GameObject[] lasers;

    [Header("Screen Position Based Settings")]
    [Tooltip("Amount of pitch the ship will have relative to local position")]
    [SerializeField] float pitchFactor = -2f;
    [Tooltip("Amount of yaw the ship will have relative to local position")]
    [SerializeField] float yawFactor = 3f;

    [Header("Player Input Based Settings")]
    [Tooltip("Amount of temporary roll the ship will have relative to input")]
    [SerializeField] float dxRollFactor = -30f;
    [Tooltip("Amount of temporary pitch the ship will have relative to input")]
    [SerializeField] float dyPitchFactor = -10f;

    Vector2 movement;
    bool isFiring;
    float dx, dy;
    float xOffset, yOffset;
    float pitch, yaw, roll;

    void OnMovement(InputValue value)
    {
        movement = value.Get<Vector2>();
    }

    void OnFire(InputValue value)
    {
        isFiring = value.isPressed;
    }

    // Update is called once per frame
    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        ProcessFiring();
    }

    private void ProcessFiring()
    {
        foreach (GameObject laser in lasers)
        {
            var emissionModule = laser.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isFiring; // playOnAwake and Loop are both enabled
            laser.GetComponent<AudioSource>().enabled = isFiring; // playOnAwake and Loop are both enabled
        }
    }

    private void ProcessRotation()
    {
        // position pitch relative to location on screen + extra temporary pitch in relation to pressed input
        pitch = transform.localPosition.y * pitchFactor + dy * dyPitchFactor;
        // position yaw relative to location on screen
        yaw = transform.localPosition.x * yawFactor;
        // temporary roll in relation to pressed input
        roll = dx * dxRollFactor;
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void ProcessTranslation()
    {
        // read press value [-1,1]
        dx = movement.x;
        dy = movement.y;

        // calculate new ship position compared to local position
        xOffset = dx * offsetSpeed * Time.deltaTime + transform.localPosition.x;
        yOffset = dy * offsetSpeed * Time.deltaTime + transform.localPosition.y;

        // limit ship position to screen boundaries
        xOffset = Mathf.Clamp(xOffset, -xRange, xRange);
        yOffset = Mathf.Clamp(yOffset, -yRange, yRange);

        // Move to new position
        transform.localPosition = new Vector3(xOffset, yOffset, transform.localPosition.z);
    }
}
