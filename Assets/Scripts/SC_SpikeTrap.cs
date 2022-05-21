using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_SpikeTrap : MonoBehaviour
{
    public Transform spikeTrap;
    public Animator spikeTrapAnim;
    private bool expanded;

    void Start()
    {
        GetComponent<Collider>().isTrigger = true;
        spikeTrapAnim = spikeTrap.GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !expanded)
        {
            expanded = true;
            spikeTrapAnim.SetTrigger("open");
        }
    }

    public void Reset() {
        if (expanded) {
            expanded = false;
            spikeTrapAnim.SetTrigger("close");
        }
    }
}
