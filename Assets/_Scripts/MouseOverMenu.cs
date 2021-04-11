using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseOverMenu : MonoBehaviour, IPointerEnterHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        //do stuff
        SFXManager.sfxInstance.Audio.PlayOneShot(SFXManager.sfxInstance.menuChangeOption);
    }

}
