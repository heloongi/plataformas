using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class PuzzlePiece : MonoBehaviour, IPointerClickHandler
{
    public int correctIndex; // posição correta
    public int currentIndex; // posição atual
    public Image image;
    private Button button;

    private void Awake()
    {
        image = GetComponent<Image>();
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        PuzzleManager.Instance.OnPieceClicked(this);
    }
}




