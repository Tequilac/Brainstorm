using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    private SC_TrapDoor[] getTrapdoors()
    {
        return GameObject.FindObjectsOfType<SC_TrapDoor>();
    }

    private SC_SpikeTrap[] getSpikeTraps()
    {
        return GameObject.FindObjectsOfType<SC_SpikeTrap>();
    }

    private Box[] getBoxes()
    {
        return GameObject.FindObjectsOfType<Box>();
    }

    private FinishPoint[] getFinishPoints()
    {
        return GameObject.FindObjectsOfType<FinishPoint>();
    }

    public void OnDeath() {
        fail.Play();
        
        players[0].Reset();
        players[1].Reset();

        SC_TrapDoor[] trapdoors = getTrapdoors();
        foreach (var trapdoor in trapdoors)
        {
            trapdoor.Reset();
        }

        SC_SpikeTrap[] spikeTraps = getSpikeTraps();
        foreach (var spikeTrap in spikeTraps)
        {
            spikeTrap.Reset();
        }

        Box[] boxes = getBoxes();
        foreach (var box in boxes)
        {
            box.Reset();
        }
    }

    public void OnPlayerStand() {
        FinishPoint[] finishPoints = getFinishPoints();
        if (finishPoints[0].isPlayerStanding() && finishPoints[1].isPlayerStanding()) {
            SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings);
        }
    }

    public void RegisterPlayer(int index, Player player) {
        players[index] = player;
    }
}
