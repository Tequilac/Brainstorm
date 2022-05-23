using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishPoint : MonoBehaviour
{
    public Material[] materials;
    public Renderer rend;
    private bool playerStanding = false;
    private GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = materials[0];
        GetComponent<Collider>().isTrigger = true;
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            rend.sharedMaterial = materials[1];
            playerStanding = true;
            gameController.OnPlayerStand();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            rend.sharedMaterial = materials[0];
            playerStanding = false;
        }
    }

    public bool isPlayerStanding()
    {
        return playerStanding;
    }
}
