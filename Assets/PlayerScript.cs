using Assets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Rigidbody sphere;
    public Rigidbody opponent;

    public string playerNumber;

    //Controls
    public string up;
    public string down;
    public string left;
    public string right;

    public float ballSpeed;
    private float xSpeed = 0.5f;
    private float zSpeed = 0.5f;
    private float firstLayerSpeed = 0.50f;
    private float secondLayerSpeed = 0.25f;
    private bool firstLayerSpeedFlag;
    private bool secondLayerSpeedFlag;

    public float collisionBounce;
    public bool adjustBounce;
    public float bounceChange;

    public float opponentCollisionBounce;
    public bool adjustOpponentBounce;
    public float opponentBounceChange;

    public KeyCode powerCode;
    public Powers powerManager;
    public bool usingPower = false;
    public float powerTime = 0f;
    public bool powerReady = true;
    public float timeUntilNextPower = 0.01f;
    public float waitDuration = 10f;
    public float powerDuration;
    private bool powerAlreadyLoaded;
    private bool FirstPowerAlreadyLoaded = true;

    private bool collided;
    private bool collideLastFrame;

    private float offTheLedgeValue = -0.5f;

    private void Start()
    {
        //Fixes glitch where players still exhibit transparency when new game starts.
        Physics.IgnoreLayerCollision(8, 8, false);
        collided = false;

        //Signals that a player fell off and another round is starting. In this situation, the powerLoaded sound shouldn't be played.
        ApplicationState.startOfNewGameScene = true;

        //Make player one load power slightly later. If not, each player accesses the loaded power at the same time 
        //and always get the same power at start of scene for some reason. FIGURE THIS OUT!
        if (powerManager.playerNumber.Equals("1"))
        {
            powerManager.delayedLoadRandomPower();
        }
        else
        {
            powerManager.loadRandomPower();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Adjust the speed of the players depending on if first two layers have already dropped.
        firstLayerSpeedFlag = FindObjectOfType<GameSceneTimer>().firstLayerFastFlag;
        secondLayerSpeedFlag = FindObjectOfType<GameSceneTimer>().secondLayerFastFlag;

        collisionBounce = FindObjectOfType<GameSceneTimer>().collisionBounce;
        if (adjustBounce)
        {
            collisionBounce += bounceChange;
        }
        opponentCollisionBounce = collisionBounce;
        if (adjustOpponentBounce)
        {
            opponentCollisionBounce += opponentBounceChange;
        }

        if (FindObjectOfType<GameSceneManager>().countDownDone)
        {
            checkForMovementInput();
        }
        checkForPowerStatus();
        checkForCollision();

        //If a player falls off the ledge, restart the game scene.
        if (sphere.position.y < offTheLedgeValue)
        {
            FindObjectOfType<GameSceneManager>().EndGameAndRestart();
        }
    }

    //POWER GLITCH!! IF BOTH PLAYERS ARE TRANSPARENT, THEN ONE PLAYER SWITCHES BACK TO NORMAL, THEY STILL COLLIDE!
    private void checkForPowerStatus()
    {
        if ((Input.GetKey(powerCode)) && !usingPower && powerReady)
        {
            powerManager.executeLoadedPower(out powerDuration);
            usingPower = true;
            powerAlreadyLoaded = false;
        }

        if (usingPower)
        {
            if (powerTime < powerDuration)
            {
                powerTime += Time.deltaTime;
            }
            else
            {
                powerManager.stopUsingPower();
                FirstPowerAlreadyLoaded = false;
                usingPower = false;
                powerReady = false;
                powerTime = 0f;
                timeUntilNextPower = 0.01f;
            }
        }
        else
        {
            timeUntilNextPower += Time.deltaTime;

            if (timeUntilNextPower > waitDuration && !powerAlreadyLoaded)
            {
                if (!FirstPowerAlreadyLoaded)
                {
                    ApplicationState.startOfNewGameScene = false;
                    powerManager.loadRandomPower();
                }
                powerReady = true;
                powerAlreadyLoaded = true;
            }
        }
    }

    // If collides, play hit sound. Do not play sound for two consecutive frames.
    private void checkForCollision()
    {
        if (collided)
        {
            if (!collideLastFrame)
            {
                FindObjectOfType<GameSceneSound>().audioHit.Play();
                collideLastFrame = true;
            }
        }
        else
        {
            collideLastFrame = false;
        }
    }

    private void OnCollisionEnter(UnityEngine.Collision collision)
    {
        if (collision.collider.gameObject.layer == 8)
        {
            // ADDED EXTRA AUDIO PLAY FOR TESTING. SEEMS TO WORK!
            FindObjectOfType<GameSceneSound>().audioHit.Play();
            collided = true;

            Vector3 direction = collision.contacts[0].point - transform.position;
            direction = -direction.normalized;
            sphere.AddForce(direction * collisionBounce);

            if (adjustOpponentBounce)
            {
                opponent.AddForce(-direction * opponentCollisionBounce);
            }
        }
    }

    private void OnCollisionExit(UnityEngine.Collision collision)
    {
        if (collision.collider.gameObject.layer == 8)
        {
            collided = false;
        }
    }

    private void checkForMovementInput()
    {
        if (Input.GetKey(left))
        {
            sphere.AddTorque(new Vector3(0, 0, zSpeed) * ballSpeed * Time.deltaTime);
            if (firstLayerSpeedFlag)
            {
                sphere.AddForce(-firstLayerSpeed, 0, 0);
            }
            else if (secondLayerSpeedFlag)
            {
                sphere.AddForce(-secondLayerSpeed, 0, 0);
            }
        }
        if (Input.GetKey(right))
        {
            sphere.AddTorque(new Vector3(0, 0, -zSpeed) * ballSpeed * Time.deltaTime);
            if (firstLayerSpeedFlag)
            {
                sphere.AddForce(firstLayerSpeed, 0, 0);
            }
            else if (secondLayerSpeedFlag)
            {
                sphere.AddForce(secondLayerSpeed, 0, 0);
            }
        }
        if (Input.GetKey(down))
        {
            sphere.AddTorque(new Vector3(-xSpeed, 0, 0) * ballSpeed * Time.deltaTime);
            if (firstLayerSpeedFlag)
            {
                sphere.AddForce(0, 0, -firstLayerSpeed);
            }
            else if (secondLayerSpeedFlag)
            {
                sphere.AddForce(0, 0, -secondLayerSpeed);
            }
        }

        if (Input.GetKey(up))
        {
            sphere.AddTorque(new Vector3(xSpeed, 0, 0) * ballSpeed * Time.deltaTime);
            if (firstLayerSpeedFlag)
            {
                sphere.AddForce(0, 0, firstLayerSpeed);
            }
            else if (secondLayerSpeedFlag)
            {
                sphere.AddForce(0, 0, secondLayerSpeed);
            }
        }
    }
}
