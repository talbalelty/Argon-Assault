using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] InputAction movement;
    [SerializeField] InputAction fire;
    [SerializeField] float offsetSpeed = 20f;
    [SerializeField] float xRange = 12f;
    [SerializeField] float yRange = 8f;
    [SerializeField] float pitchFactor = -2f;
    [SerializeField] float dyPitchFactor = -10f;
    [SerializeField] float yawFactor = 3f;
    [SerializeField] float dxRollFactor = -30f;

    private float dx, dy;
    private float xOffset, yOffset;
    private float pitch, yaw, roll;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        movement.Enable();
    }

    private void OnDisable()
    {
        movement.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
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
        dx = movement.ReadValue<Vector2>().x;
        dy = movement.ReadValue<Vector2>().y;

        // calculate new ship position compared to local position
        xOffset = dx * offsetSpeed * Time.deltaTime + transform.localPosition.x;
        yOffset = dy * offsetSpeed * Time.deltaTime + transform.localPosition.y;

        // limit ship position to screen boundaries
        xOffset = Mathf.Clamp(xOffset, -xRange, xRange);
        yOffset = Mathf.Clamp(yOffset, -yRange, yRange);

        transform.localPosition = new Vector3(xOffset, yOffset, transform.localPosition.z);
    }
}
