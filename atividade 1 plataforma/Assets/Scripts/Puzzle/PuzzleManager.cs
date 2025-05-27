using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager Instance;
    public GridLayoutGroup grid;
    public GameObject piecePrefab;
    public Sprite[] slicedSprites;
    public Transform winPanel;
    public Button undoButton, replayButton, cancelReplayButton, restartButton;

    private List<PuzzlePiece> pieces = new List<PuzzlePiece>();
    private PuzzlePiece selectedPiece = null;
    private CommandInvoker commandInvoker;

    private void Awake()
    {
        Instance = this;
        commandInvoker = GetComponent<CommandInvoker>();
    }

    private void Start()
    {
        SetupPuzzle();
        undoButton.onClick.AddListener(() => {
            if (selectedPiece == null)
                commandInvoker.Undo();
        });

        replayButton.onClick.AddListener(() => {
            StartCoroutine(commandInvoker.Replay(OnWin));
        });

        cancelReplayButton.onClick.AddListener(() => {
            commandInvoker.CancelReplayAndFinish(OnWin);
        });

        restartButton.onClick.AddListener(() => {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        });
    }

    private void SetupPuzzle()
    {
        commandInvoker.ClearHistory();
        pieces.Clear();
        List<int> indices = new List<int>();
        for (int i = 0; i < slicedSprites.Length; i++)
        {
            indices.Add(i);
        }

        // Shuffle the list
        for (int i = 0; i < indices.Count; i++)
        {
            int rnd = Random.Range(0, indices.Count);
            int tmp = indices[i];
            indices[i] = indices[rnd];
            indices[rnd] = tmp;
        }

        for (int i = 0; i < indices.Count; i++)
        {
            GameObject obj = Instantiate(piecePrefab, grid.transform);
            PuzzlePiece piece = obj.GetComponent<PuzzlePiece>();
            piece.correctIndex = indices[i];
            piece.currentIndex = i;
            piece.image.sprite = slicedSprites[indices[i]];
            pieces.Add(piece);
        }
    }

    public void OnPieceClicked(PuzzlePiece piece)
    {
        if (commandInvoker.IsReplaying)
            return;

        if (selectedPiece == null)
        {
            selectedPiece = piece;
            HighlightPiece(piece, true);
        }
        else
        {
            if (selectedPiece != piece)
            {
                ICommand command = new SwapCommand(selectedPiece, piece);
                commandInvoker.ExecuteCommand(command);
                CheckWin();
            }

            HighlightPiece(selectedPiece, false);
            selectedPiece = null;
        }
    }

    private void HighlightPiece(PuzzlePiece piece, bool highlight)
    {
        piece.image.color = highlight ? Color.yellow : Color.white;
    }

    public static void SwapPieces(PuzzlePiece a, PuzzlePiece b)
    {
        Sprite tempSprite = a.image.sprite;
        int tempIndex = a.correctIndex;

        a.image.sprite = b.image.sprite;
        a.correctIndex = b.correctIndex;

        b.image.sprite = tempSprite;
        b.correctIndex = tempIndex;
    }

    private void CheckWin()
    {
        foreach (var piece in pieces)
        {
            if (piece.correctIndex != piece.currentIndex)
                return;
        }

        OnWin();
    }

    private void OnWin()
    {
        winPanel.gameObject.SetActive(true);
    }
    

}
