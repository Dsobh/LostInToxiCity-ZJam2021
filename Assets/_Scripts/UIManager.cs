using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    

    public GameObject[] mapParts;
    public GameObject mapPanel;
    private bool mapActive = false;
    private int partsObtained = 0;
    public TMP_Text numberOfPartsText;
    public GameObject avisoMapas;
    public GameObject abanico;
    public GameObject X_Image;
    private bool xReached = false;
    public GameObject padres;
    public GameObject menu;
    public GameObject controles;


    void OnEnable()
    {
        PlayerInteractions.OnMapChanged += HandleMapChanged;
    }

    void OnDisable()
    {
        PlayerInteractions.OnMapChanged -= HandleMapChanged;
    }

    void Start()
    {
        numberOfPartsText.text = "x0";
    }

    void Update() {
        if(controles.activeInHierarchy && Input.GetKeyDown(KeyCode.Space))
        {
            controles.SetActive(false);
        }

        if(Input.GetKeyDown(KeyCode.M))
        {
            if(!mapActive)
            {
                if(partsObtained == 0 && !avisoMapas.activeInHierarchy)
                {
                    avisoMapas.SetActive(true);
                }

                mapPanel.SetActive(true);
                mapActive = true;
                if(xReached)
                {
                    X_Image.SetActive(true);
                }
            }else{

                if(partsObtained == 0)
                {
                    avisoMapas.SetActive(false);
                }

                mapPanel.SetActive(false);
                mapActive = false;
                if(xReached)
                {
                    X_Image.SetActive(false);
                }
            } 
        }

        if(partsObtained > 0)
        {
            avisoMapas.SetActive(false);
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!menu.activeInHierarchy){
                menu.SetActive(true);
            }else{
                menu.SetActive(false);
            }
        }
    }

    void HandleMapChanged(int index) 
    {
        if(partsObtained < 4)
        {
            partsObtained++;
            numberOfPartsText.text = "x"+partsObtained.ToString();
            mapParts[index].gameObject.GetComponent<Image>().enabled = true;
        }
        if(partsObtained == 4){
            xReached = true;
            padres.SetActive(true);
            //Activar padres
        }
    }

    public void ToggleAbanico()
    {
        Debug.Log(!abanico.activeInHierarchy);
        abanico.SetActive(!abanico.activeInHierarchy);
    }
}
