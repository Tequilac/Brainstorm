using UnityEngine;

public class SC_TrapDoor : MonoBehaviour
{
    public Transform trapDoor;
    public Animator trapDoorAnim;
    public GameObject death;
    private GameObject clonedDeath;
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
            opened = true;
            trapDoorAnim.SetTrigger("open");
            clonedDeath = Instantiate(death, trapDoor.position, trapDoor.rotation);
        }
    }

    public void Reset() {
        if (opened) {
            opened = false;
            trapDoorAnim.SetTrigger("close");
            Destroy(clonedDeath);
        }
    }
}