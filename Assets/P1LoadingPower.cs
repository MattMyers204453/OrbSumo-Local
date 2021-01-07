using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Responsible for showing the loading power bar.
/// </summary>
public class P1LoadingPower : MonoBehaviour
{
    public string playerTag;
    private GameObject playerObject;

    public RectTransform bar;
    private RectTransform parent;

    private float loadingProgress;
    private float loadingTotal;
    private float loadingPercentage;

    private float powerProgress;
    private float powerTotal;
    private float powerPercentage;
    private bool firstTimeGettingPowerTotal = true;

    private void Awake()
    {
        parent = (RectTransform)bar.parent;
        playerObject = GameObject.FindGameObjectWithTag(playerTag);
        loadingTotal = playerObject.GetComponent<PlayerScript>().waitDuration;
    }
    // Start is called before the first frame update
    void Start()
    {
        bar.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, parent.rect.width);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerObject.GetComponent<PlayerScript>().powerReady)
        {
            if (playerObject.GetComponent<PlayerScript>().usingPower)
            {
                // The usageTotal only needs to be accessed once, as soon as the power up is determined.
                if (firstTimeGettingPowerTotal)
                {
                    powerTotal = playerObject.GetComponent<PlayerScript>().powerDuration;
                    firstTimeGettingPowerTotal = false;
                }
                powerProgress = playerObject.GetComponent<PlayerScript>().powerTime;
                powerPercentage = 1 - (powerProgress / powerTotal);
                bar.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, powerPercentage * parent.rect.width);
            }
            else
            {
                bar.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, parent.rect.width);
            }
        }
        else
        {
            firstTimeGettingPowerTotal = true;
            loadingProgress = playerObject.GetComponent<PlayerScript>().timeUntilNextPower;
            loadingPercentage = loadingProgress / loadingTotal;
            if (loadingPercentage <= 1.0f)
            {
                bar.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, loadingPercentage * parent.rect.width);
            }
        }
    }
}
