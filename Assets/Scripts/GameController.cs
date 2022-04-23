using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    Player[] players = new Player[2];

    [SerializeField]
    AudioSource fail;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnDeath() {
        fail.Play();
        
        players[0].Reset();
        players[1].Reset();
    }

    public void RegisterPlayer(int index, Player player) {
        players[index] = player;
    }
}
