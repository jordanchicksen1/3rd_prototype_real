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
        }
    }

    public void updateGemPiece(float amount)
    {
        currentGemPiece += amount;
        updateGemPieceBar();

    }

    public void updateGemPieceBar()
    {
        float targetFillAmount = currentGemPiece / maxGemPiece;
        gemPieceBar.fillAmount = targetFillAmount;
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
}
