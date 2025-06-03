using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class gemPieceMeter : MonoBehaviour
{
    public float maxGemPiece = 5f;
    public float currentGemPiece;
    public Image gemPieceBar;

    public gemManager gemManager;

    public GameObject gotGemText;

    public ParticleSystem gotGemParticle;

    public AudioSource sfx;
    public AudioClip gotGem;


    public void Start()
    {
        currentGemPiece = 0f;
        updateGemPieceBar();
    }

    public void Update()
    {
        if(currentGemPiece > 4.99f)
        {
            GemFull();
            StartCoroutine(GotGem());
            StartCoroutine(GemParticle());
            sfx.clip = gotGem;
            sfx.Play();
        }
    }

    public void updateGemPiece(float amount)
    {
        currentGemPiece += amount;
        updateGemPieceBar();

    }

    public void updateGemPieceBar()
    {
        StartCoroutine(UpdateGemBar());
    }

    [ContextMenu("GemFull")]
    public void GemFull()
    {
        currentGemPiece = currentGemPiece - 5f;
        updateGemPieceBar();
        gemManager.addGem();

    }

    [ContextMenu("GotGemPiece")]
    public void GotGemPiece()
    {
        currentGemPiece = currentGemPiece + 1f;
        updateGemPieceBar();
        

    }

    public IEnumerator GotGem()
    {
        yield return new WaitForSeconds(0f);
        gotGemText.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        gotGemText.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        gotGemText.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        gotGemText.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        gotGemText.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        gotGemText.SetActive(false);
    }

    public IEnumerator UpdateGemBar()
    {
        yield return new WaitForSeconds(0.5f);
        float targetFillAmount = currentGemPiece / maxGemPiece;
        gemPieceBar.fillAmount = targetFillAmount;
    }

    public IEnumerator GemParticle()
    {
        yield return new WaitForSeconds(0.5f);
        gotGemParticle.Play();
    }

    public IEnumerator GemPieceTransfer()
    {
        yield return new WaitForSeconds(0.5f);
        
    }
}
