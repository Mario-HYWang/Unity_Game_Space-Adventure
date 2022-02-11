using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [Header("Basic Control Settings")]
    [Tooltip("The flying speed of your ship")]
    [SerializeField] float controlSpeed = 10f;
    [SerializeField] float xRange = 11f;
    [SerializeField] float yRange = 7f;

    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField] float controlPitchFactor = -15f;

    [SerializeField] float positionRollFactor = -1.5f;
    [SerializeField] float controlRollFactor = -20f;

    [SerializeField] GameObject[] lasers;

    float xThrow, yThrow;

    void Update()
    {
        ProcessTraslation();
        ProcessRotation();
        ProcessFiring();
        ProcessQuit();
    }

    void ProcessQuit()
    {
        if (Input.GetKey(KeyCode.Escape))
        Application.Quit();
    }

    void ProcessRotation()
    {
        // ©Ô¿Y
        float pitchDueToPos = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;
        float pitchRotate = pitchDueToPos + pitchDueToControlThrow;

        // ±€¬‡
        float rollDueToPos = transform.localPosition.x * positionRollFactor;
        float rollDueToControlThrow = xThrow * controlRollFactor;
        float rollRotate = rollDueToPos + rollDueToControlThrow;
  
        transform.localRotation = Quaternion.Euler(pitchRotate, 0f , rollRotate);
    }

    void ProcessTraslation()
    {
        xThrow = Input.GetAxis("Horizontal");
        yThrow = Input.GetAxis("Vertical");

        float xOffset = xThrow * Time.deltaTime * controlSpeed;
        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

        float yOffset = yThrow * Time.deltaTime * controlSpeed;
        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }

    void ProcessFiring()
    {
        if (Input.GetButton("Fire1"))
        { 
            SetLaserActive(true); 
        }

        else
        {
            SetLaserActive(false);
        }
    }

    void SetLaserActive(bool isActive)
    {
        foreach (GameObject laser in lasers)
        {
            var emissionModule = laser.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;
        }
    }
}

