using UnityEngine;

public class SC_TrapDoor : MonoBehaviour
{
    public Transform trapDoor;
    public Animator trapDoorAnim;
    public GameObject death;
    private bool opened;

    void Start()
    {
        GetComponent<Collider>().isTrigger = true;
        trapDoorAnim = trapDoor.GetComponent<Animator>();
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !opened)
        {
            Debug.Log("exit");
            opened = true;
            trapDoorAnim.SetTrigger("open");
            Instantiate(death, trapDoor.position, trapDoor.rotation);
        }
    }
}