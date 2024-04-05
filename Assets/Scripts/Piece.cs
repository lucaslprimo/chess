using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Piece
{
    public enum Team
    {
        BLACK, WHITE
    }

    public enum Type
    {
       PAWN, ROOK, BISHOP, KING, QUEEN, KNIGHT
    }

    public Skin skin;

    [SerializeField]
    private Team team;
    [SerializeField]
    private Type type;
    
    public Vector2 startPosition;

    [SerializeField]
    private int xMove = 1;
    [SerializeField]
    private int yMove = 1;

    [SerializeField]
    private Boolean doesDiagonalMove = true;
    [SerializeField]
    private Boolean doesParalelMove = false;

    public Vector2 position;

    public List<Vector2> possibleMoves;
    public List<Vector2> possibleAttacks;

    private const int C_ATTACK = 1;
    private const int Y_ATTACK = 1;
    

    private Boolean isFirstMove = true;

    private GameObject pieceInstance;

    public void SpawnPiece(Vector3 boardPosition)
    {
        position = startPosition;
        Debug.Log($"Start Position = {position.x}, {position.y}");
        pieceInstance = GameObject.Instantiate(skin.GetSkinedPiece(type, team), boardPosition, Quaternion.identity);
        possibleMoves = new List<Vector2>();
        possibleAttacks = new List<Vector2>();
        CalculatePossibleMoves();

    }

    private void CalculatePossibleMoves()
    {
        possibleMoves.Clear();
        if (doesParalelMove)
        {           
            for (int i = 1; i <= xMove; i++)
            {
                int newX = (int)position.x + i;
                if (newX < 8)
                    possibleMoves.Add(new Vector2(newX, position.y));
            }

            //Handle the fist move of the Pawn
            int altYMove = yMove;
            if (isFirstMove && type == Type.PAWN) altYMove = yMove + 1;

            for (int i = 1; i <= altYMove; i++)
            {
                int newY = (int)position.y + i;
                if (newY < 8)
                    possibleMoves.Add(new Vector2(position.x, newY));
            }
            
            if(type != Type.PAWN)
            {
                for (int i = 1; i <= xMove; i++)
                {
                    int newX = (int)position.x - i;
                    if (newX >= 0)
                        possibleMoves.Add(new Vector2(newX, position.y));
                }

                for (int i = 1; i <= yMove; i++)
                {
                    int newY = (int)position.y - i;
                    if (newY >= 0)
                        possibleMoves.Add(new Vector2(position.x, newY));
                }
            }  
        }

        if (doesDiagonalMove)
        {
            for (int i = 1; i <= xMove; i++)
            {
                int newY = (int)position.y + i;
                int newX = (int)position.x + i;
                if (newY < 8 && newY < 8)
                    possibleMoves.Add(new Vector2(newX, newY));
            }

            for (int i = 1; i <= xMove; i++)
            {
                int newY = (int)position.y - i;
                int newX = (int)position.x - i;
                if (newY >= 0 && newX >= 0)
                    possibleMoves.Add(new Vector2(newX, newY));
            }
        }

        Debug.Log("Possible Moves");
        foreach (Vector2 move in possibleMoves)
        {
            Debug.Log($"{move.x}, {move.y}");
        }
        Debug.Log("--------------");

        if (possibleMoves.Count == 0)
        {
            Debug.Log("Can't Move");
        }
    }

    public void Move(Vector2 newPosition)
    {
        if (isFirstMove) isFirstMove = false;
        position = newPosition;
        Debug.Log($"Moved To = {position.x}, {position.y}");
    }

    
}
