using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneTimer : MonoBehaviour
{
    public float fallPoint;
    private float elapsed = 0f;
    public Rigidbody layer;
    public Material alarmMaterial;

    public float zoomTarget;
    private float zoomElapsed = 0f;
    private readonly float elapsedEnd = 1f;

    public bool firstLayerFastFlag;
    public bool secondLayerFastFlag;

    public float collisionBounce;

    public AudioSource fallSound;
    private bool fallSoundAlreadyPlayed;
    public AudioSource alarmSound;
    private bool alarmSoundAlreadyPlayed;

    // Update is called once per frame
    void Update()
    {
        if (elapsed < 5)
        {
            firstLayerFastFlag = true;
            secondLayerFastFlag = false;
            collisionBounce = 10f;
        }
        else if (elapsed > 5 && elapsed < 12)
        {
            firstLayerFastFlag = false;
            secondLayerFastFlag = true;
            collisionBounce = 7.5f;
        }
        else if (elapsed > 12 && elapsed < 22)
        {
            collisionBounce = 5f;
        }
        else if (elapsed > 22 && elapsed < 32)
        {
            collisionBounce = 2.5f;
        }
        else
        {
            firstLayerFastFlag = false;
            secondLayerFastFlag = false;
            collisionBounce = 0f;
        }

        if (elapsed >= fallPoint)
        {
            layer.isKinematic = false;
            layer.useGravity = true;
            if (!fallSoundAlreadyPlayed)
            {
                fallSound.Play();
                fallSoundAlreadyPlayed = true;
            }
            cameraZoomAction();
        }
        else if (fallPoint - elapsed < 1.5f)
        {
            layer.gameObject.GetComponent<Renderer>().material = alarmMaterial;
            if (!alarmSoundAlreadyPlayed)
            {
                alarmSound.Play();
                alarmSoundAlreadyPlayed = true;
            }
        }
        elapsed += Time.deltaTime;
    }

    private void cameraZoomAction()
    {
        if (zoomElapsed <= elapsedEnd)
        {
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, zoomTarget, 5 * Time.deltaTime);
        }
        zoomElapsed += Time.deltaTime;
    }
}
