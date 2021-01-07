using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayUsernames : MonoBehaviour
{
    public TextMeshProUGUI P1Username;
    public TextMeshProUGUI P2Username;

    // Start is called before the first frame update
    void Start()
    {
        P1Username.text = ApplicationState.playerOneUserName;
        P2Username.text = ApplicationState.playerTwoUserName;
    }
}
