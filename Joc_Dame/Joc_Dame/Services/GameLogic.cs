using Joc_Dame.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Security.Cryptography.X509Certificates;


namespace Joc_Dame.Services
{
    internal class GameLogic
    {


        public BoardLogic board { get; set;}
        public bool isRedTurn { get; set; } 
        public bool multipleJumps { get; set; }
        public GameLogic()
        {
            board = new BoardLogic();
        }
        
        public void StartGame(bool MultipleJumps)
        {
            isRedTurn = true;
            board = new BoardLogic();
            multipleJumps = MultipleJumps;
        }
       
        public void SwitchTurn()
        {
            isRedTurn = !isRedTurn;
        }
        
        public void MovePiece(int position1, int position2 , int mPosition1, int mPosition2)
        {
           if(mPosition1 >= 8 || mPosition1 < 0 || mPosition2 >= 8 || mPosition2 < 0 )
                return;
           if (position1 >= 8 || position1 < 0 || position2 >= 8 || position2 < 0)
                return;
            if (board.board[position1, position2] == EPiece.Empty)
                return;
            if (isRedTurn && board.board[position1, position2] != EPiece.RedSoldier && board.board[position1, position2] != EPiece.RedKing)
                return;
            if (board.madeMove == false)
            {
                board.MakeMoveNonCapture(position1, position2, mPosition1, mPosition2);
                if (board.madeMove == true )
                {
                    checkWinner();
                    SwitchTurn();
                    board.madeMove = false;
                    board.selectedPiece = Tuple.Create(-1, -1);
                }
                board.MakeMoveWithCapture(position1, position2, mPosition1, mPosition2);
                if (board.madeMove == true &&( multipleJumps == false || (multipleJumps==true && board.MovePossible()==false)))
                {
                    checkWinner();
                    SwitchTurn();
                    board.madeMove = false;
                    board.selectedPiece = Tuple.Create(-1, -1);
                }

            }
            else {
                if (board.selectedPiece.Item1 == position1 && board.selectedPiece.Item2 == position2 && multipleJumps == true)
                {
                    
                    board.MakeMoveWithCapture(position1, position2, mPosition1, mPosition2);
                    checkWinner();
                    if(board.MovePossible()==false)
                    {
                        SwitchTurn();
                        board.madeMove = false;
                        board.selectedPiece = Tuple.Create(-1, -1);
                    }

                }
            }
            
            

        }
        public EPiece checkWinner()
        {
            if (board.redPiecesNumber == 0)
            {
                return EPiece.WhiteSoldier;
            }
            else if (board.whitePiecesNumber == 0)
            {
                return EPiece.RedSoldier;
            }
            else
            return EPiece.Empty;
        }

        public void SaveGame()
        {
            GameData gameData = new GameData(board.board, isRedTurn, multipleJumps);
            gameData.SaveGame();
            
        }

        public void LoadGame()
        {
            GameData gameData = new GameData();
            gameData.LoadGame();
            
            StartGame(multipleJumps);
            
            isRedTurn = gameData.RedTurn;
            multipleJumps = gameData.multipleJumps;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    board.board[i, j] = (EPiece)gameData.board[i * 8 + j];
                }
            }

        }   

    }
}
