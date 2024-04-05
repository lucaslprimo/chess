using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField]
    private Transform firstSquarePosition;
    [SerializeField]
    private float squareSize;
    [SerializeField]
    private GameObject prefabHighlight;

    private SquareInfo[][] boardMatrix = new SquareInfo[8][];

    [SerializeField]
    private Piece[] pieces;

    private class SquareInfo
    {
        public GameObject highlight;
        public Piece piece;
        public bool isOccupied = false;
    }

    void Start()
    {
        for (var i = 0; i < boardMatrix.Length; i++)
        {
            boardMatrix[i] = new SquareInfo[8];
        }

        for (int i = 0; i < boardMatrix.Length; i++)
        {
            for (int j = 0; j < boardMatrix.Length; j++)
            {
                SquareInfo squareInfo = new SquareInfo();
                Vector3 squarePosition = new Vector3(firstSquarePosition.position.x + i * squareSize, 0.01f, firstSquarePosition.position.z + j * squareSize);
                squareInfo.highlight = Instantiate(prefabHighlight, squarePosition, Quaternion.identity);
                squareInfo.highlight.SetActive(false);
                boardMatrix[i][j] = squareInfo;
            }
        }

        foreach(Piece piece in pieces)
        {
            SquareInfo square = GetBoardSquareByPosition(piece.startPosition);
            square.piece = piece;
            square.isOccupied = true;
            piece.SpawnPiece(square.highlight.transform.position);
        }

        foreach(Vector2 position in pieces[0].possibleMoves)
        {
            SquareInfo square = GetBoardSquareByPosition(position);
            if (!square.isOccupied) square.highlight.SetActive(true);
            else break;
        }
    }

    private SquareInfo GetBoardSquareByPosition(Vector2 position)
    {
        return boardMatrix[(int)position.x][(int)position.y];
    }

    void Update()
    {
        
    }
}
