using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerInteractions : MonoBehaviour
{

    public delegate void _OnMapChanged(int index);
    public static event _OnMapChanged OnMapChanged;
    public UnityEvent OnChange;

    private Animator _animator;
    public GameObject canvas;
    public GameObject interactionAdviseText;
    public float offset = 5f;
    public bool[] pointsOfInterest;
    private bool isOnPOITrigger = false;
    private bool isOnGasTrigger = false;
    [SerializeField]
    public bool hasAbanico = false;
    private int poiNumber;
    private GameObject auxPOI;
    private GameObject gas;
    public UIManager _uimanager;
    public float gasLifeTime = 0.1f;
    public float gasTimeCount = 0f;
    private bool gasDisperse = false;

    void Start()
    {
        _animator = GetComponent<Animator>();
        for (int i = 0; i < pointsOfInterest.Length; i++)
        {
            pointsOfInterest[i] = false;
        }
    }

    void Update()
    {
        if(isOnPOITrigger && Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("Has pulsado la f" + poiNumber);
            auxPOI.GetComponent<BoxCollider2D>().enabled = false;
            SFXManager.sfxInstance.Audio.PlayOneShot(SFXManager.sfxInstance.pickupMapa);

            MapChanged(poiNumber);
        }

        if(isOnGasTrigger && Input.GetKeyDown(KeyCode.F) && hasAbanico)
        {
            _animator.SetTrigger("Abanico");
            SFXManager.sfxInstance.Audio.PlayOneShot(SFXManager.sfxInstance.abanicar);
            SFXManager.sfxInstance.Audio.PlayOneShot(SFXManager.sfxInstance.disolverGas);
            gasDisperse = true;
            
        }

        if (gasDisperse)
        {
            if (gasLifeTime > gasTimeCount)
            {
                gas.gameObject.transform.localScale = new Vector3(gas.gameObject.transform.localScale.x,
                                                                    gas.gameObject.transform.localScale.y - 0.001f,
                                                                    gas.gameObject.transform.localScale.z);
                gasTimeCount += Time.deltaTime;
            }else{
                Destroy(gas.gameObject);
                gasDisperse = false;
                gasTimeCount = 0;
            }
            
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Gas") && hasAbanico)
        {
            isOnGasTrigger = true;
            ShowMesage();
            gas = other.gameObject;
            Debug.Log("Has entrado en el trigger del gas. Mostrar mensaje por pantalla.");
        }
        if(other.CompareTag("POI"))
        {
            isOnPOITrigger = true;
            ShowMesage();
            poiNumber = other.gameObject.GetComponent<POIInfo>().pointNumber;
            auxPOI = other.gameObject;
        }
        if(other.CompareTag("Abanico") && !hasAbanico)
        {
            hasAbanico = true;
            SFXManager.sfxInstance.Audio.PlayOneShot(SFXManager.sfxInstance.pickupAbanico);
            _uimanager.ToggleAbanico();
            Destroy(other.gameObject);
        }
        if(other.CompareTag("MusicaPadres"))
        {
            Debug.Log("musiquita turbia");
            AmbienteScript.AmbienteInstance.Audio.clip = AmbienteScript.AmbienteInstance.tenseAmbient;
            AmbienteScript.AmbienteInstance.Audio.Play();
            BgScript.BgInstance.Audio.clip = null;
            //TODO:musica creepy
        }
        if(other.CompareTag("Padres"))
        {
            SceneManager.LoadScene("TheEnd");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Gas"))
        {
            isOnGasTrigger = false;
            canvas.transform.SetParent(null);
            canvas.gameObject.SetActive(false);
            Debug.Log("Has salido del trigger del gas. Quitar mensaje en pantalla.");
        }

        if(other.CompareTag("POI"))
        {
            isOnPOITrigger = false;
            auxPOI = null;
            canvas.transform.SetParent(null);
            canvas.gameObject.SetActive(false);
        }
    }

    void ShowMesage()
    {
        interactionAdviseText.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + offset);
        canvas.transform.SetParent(this.transform);
        canvas.SetActive(true);
    }

    void MapChanged(int index) 
    {
        Debug.Log("Estas en la función que cambia el mapa");
        OnChange.Invoke();

        if(OnMapChanged != null)
        {
            OnMapChanged(index);
        }
    }
}
