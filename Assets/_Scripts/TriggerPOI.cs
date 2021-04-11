using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPOI : MonoBehaviour
{

    public bool interactable = false;  //Si puedes clickar en el punto de interés o no
    public int nPOIS = 0;
    private Collider2D currentPOI = null; //El POI en el que estás actualmente
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (interactable && Input.GetKeyDown(KeyCode.Mouse0))
        {
            nPOIS++;
            Destroy(currentPOI.gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        interactable = true;
        currentPOI = col;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        interactable = false;
        currentPOI = null;
    }
}
