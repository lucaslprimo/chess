using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skin", menuName = "ScriptableObjects/SkinConfig", order = 1)]
public class Skin: ScriptableObject
{
    public string SkinName = "Default";

    public GameObject whitePawn;
    public GameObject whiteBishop;
    public GameObject whiteKing;
    public GameObject whiteKnight;
    public GameObject whiteQueen;
    public GameObject whiteRook;

    public GameObject blackPawn;
    public GameObject blackBishop;
    public GameObject blackKing;
    public GameObject blackKnight;
    public GameObject blackQueen;
    public GameObject blackRook;

    public GameObject GetSkinedPiece(Piece.Type type, Piece.Team team)
    {
        if (team == Piece.Team.WHITE) {
            switch (type)
            {
                case Piece.Type.PAWN: 
                    return whitePawn;
                case Piece.Type.ROOK:
                    return whiteRook;
                case Piece.Type.BISHOP:
                    return whiteBishop;
                case Piece.Type.KING:
                    return whiteKing;
                case Piece.Type.QUEEN:
                    return whiteQueen;
                case Piece.Type.KNIGHT:
                    return whiteKnight;
            }
        }
        else
        {
            switch (type)
            {
                case Piece.Type.PAWN:
                    return blackPawn;
                case Piece.Type.ROOK:
                    return blackRook;
                case Piece.Type.BISHOP:
                    return blackBishop;
                case Piece.Type.KING:
                    return blackKing;
                case Piece.Type.QUEEN:
                    return blackQueen;
                case Piece.Type.KNIGHT:
                    return blackKnight;
            }
        }

        return null;
    }
}
