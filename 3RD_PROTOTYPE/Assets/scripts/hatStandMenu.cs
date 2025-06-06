using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class hatStandMenu : MonoBehaviour
{
    public GameObject hatStandPanel;
    
    public GameObject grassyHatInfo;
    public GameObject grassyHatEquip;
    public bool isWearingGrassyHat = false;
    public GameObject grassyHatOnStand;
    public GameObject grassyHatPlayer;
   
    public GameObject beanieInfo;
    public GameObject beanieEquip;
    public bool isWearingBeanie = false;
    public GameObject beanieOnStand;
    public GameObject beaniePlayer;
    
    public GameObject hardHatInfo;
    public GameObject hardHatEquip;
    public bool isWearingHardHat = false;
    public GameObject hardHatOnStand;
    public GameObject hardHatPlayer;

    public GameObject noHatsPanel;
    public GameObject hatEquipped;
    public GameObject alreadyWearing;

    public GameObject unequipButton;

    public bool buttonPressed = false;
    public void boughtGrassyHat()
    {
        grassyHatInfo.SetActive(true);
        noHatsPanel.SetActive(false);
        isWearingGrassyHat = true;
        grassyHatOnStand.SetActive(true);

        isWearingBeanie = false;
        isWearingHardHat = false;

        unequipButton.SetActive(true);
    }

    public void boughtBeanie()
    {
        beanieInfo.SetActive(true);
        noHatsPanel.SetActive(false);
        isWearingBeanie = true;
        beanieOnStand.SetActive(true);

        isWearingGrassyHat = false;
        isWearingHardHat = false;

        unequipButton.SetActive(true);
    }

    public void boughtHardHat()
    {
        hardHatInfo.SetActive(true);
        noHatsPanel.SetActive(false);
        isWearingHardHat = true;
        hardHatOnStand.SetActive(true);

        isWearingBeanie = false;
        isWearingGrassyHat = false;

        unequipButton.SetActive(true);
    }

    public void GrassyHatEquip()
    {
        if(isWearingGrassyHat == false && buttonPressed == false) 
        { 
            grassyHatPlayer.SetActive(true);
            hardHatPlayer.SetActive(false);
            beaniePlayer.SetActive(false);

            isWearingBeanie = false;
            isWearingGrassyHat = true;
            isWearingHardHat = false;

            StartCoroutine(HatEquipped());

            buttonPressed = true;
            StartCoroutine(ButtonPressed());


        }

        if(isWearingGrassyHat == true && buttonPressed == false)
        {
            StartCoroutine(AlreadyWearing());
            buttonPressed = true;
            StartCoroutine(ButtonPressed());
        }


    }

    public void BeanieEquip()
    {
        if (isWearingBeanie == false && buttonPressed == false)
        {
            grassyHatPlayer.SetActive(false);
            hardHatPlayer.SetActive(false);
            beaniePlayer.SetActive(true);

            isWearingBeanie = true;
            isWearingGrassyHat = false;
            isWearingHardHat = false;

            StartCoroutine(HatEquipped());

            buttonPressed = true;
            StartCoroutine(ButtonPressed());

        }

        if (isWearingBeanie == true && buttonPressed == false)
        {
            StartCoroutine(AlreadyWearing());

            buttonPressed = true;
            StartCoroutine(ButtonPressed());

        }
    }

    public void HardHatEquip()
    {
        if (isWearingHardHat == false && buttonPressed == false)
        {
            grassyHatPlayer.SetActive(false);
            hardHatPlayer.SetActive(true);
            beaniePlayer.SetActive(false);

            isWearingBeanie = false;
            isWearingGrassyHat = false;
            isWearingHardHat = true;

            StartCoroutine(HatEquipped());

            buttonPressed = true;
            StartCoroutine(ButtonPressed());
        }

        if (isWearingHardHat == true && buttonPressed == false)
        {
            StartCoroutine(AlreadyWearing());

            buttonPressed = true;
            StartCoroutine(ButtonPressed());
        }
    }

    public void Unequip()
    {
        grassyHatPlayer.SetActive(false);
        beaniePlayer.SetActive(false);
        hardHatPlayer.SetActive(false);

        isWearingBeanie = false;
        isWearingGrassyHat = false;
        isWearingHardHat = false;
    }

    public IEnumerator AlreadyWearing()
    {
        yield return new WaitForSeconds(0f);
        hatEquipped.SetActive(false);
        alreadyWearing.SetActive(true);
        yield return new WaitForSeconds(2f);
        alreadyWearing.SetActive(false) ;
    }

    public IEnumerator HatEquipped()
    {
        yield return new WaitForSeconds(0f);
        alreadyWearing.SetActive(false);
        hatEquipped.SetActive(true);
        yield return new WaitForSeconds(2f);
        hatEquipped.SetActive(false);
    }

    public IEnumerator ButtonPressed()
    {
        yield return new WaitForSeconds(0.1f);
        buttonPressed = false;
    }

}
