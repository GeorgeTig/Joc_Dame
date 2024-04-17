using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace Joc_Dame.Model
{
    internal class BoardLogic
    {
        public EPiece[,] board { get; set; }
        public int redPiecesNumber { get; set; }
        public int whitePiecesNumber { get; set; }
        public bool madeMove { get; set; }
        public Tuple<int, int> selectedPiece { get; set; }
        public BoardLogic()
        {
            madeMove = false;
            selectedPiece = new Tuple<int, int>(-1, -1);
            redPiecesNumber = 12;
            whitePiecesNumber = 12;
            board = new EPiece[8, 8];
            for(int i=0; i<8; i++)
            {
                for(int j=0; j<8; j++)
                {
                    if(i % 2 == 0)
                    {
                        if(j % 2 == 0)
                        {
                            board[i, j] = EPiece.Empty;
                        }
                        else
                        {
                            if(i < 3)
                            {
                                board[i, j] = EPiece.RedSoldier;
                            }
                            else if(i > 4)
                            {
                                board[i, j] = EPiece.WhiteSoldier;
                            }
                            else
                            {
                                board[i, j] = EPiece.Empty;
                            }
                        }
                    }
                    else
                    {
                        if(j % 2 == 0)
                        {
                            if(i < 3)
                            {
                                board[i, j] = EPiece.RedSoldier;
                            }
                            else if(i > 4)
                            {
                                board[i, j] = EPiece.WhiteSoldier;
                            }
                            else
                            {
                                board[i, j] = EPiece.Empty;
                            }
                        }
                        else
                        {
                            board[i, j] = EPiece.Empty;
                        }
                    }
                }
            }
        }
        

        public void MakeMoveNonCapture(int position1, int position2, int mPosition1, int mPosition2)
        {
            if (board[mPosition1, mPosition2] != EPiece.Empty)
                return;

            if (mPosition2 != position2 - 1 && mPosition2 != position2 + 1)
                return;

                if (board[position1, position2] != EPiece.WhiteSoldier)
                {
                    if(mPosition1 == position1+1 )
                    {
                        EPiece piece = board[position1, position2];
                        board[mPosition1, mPosition2] = piece;
                        board[position1, position2] = EPiece.Empty;
                        madeMove = true;
                        selectedPiece = Tuple.Create(mPosition1, mPosition2);
                        if((mPosition1 == 0) || (mPosition1 == 7))
                        promotePiece(mPosition1, mPosition2);
                    }
                }

            if(board[position1, position2] != EPiece.RedSoldier)
            {
                if (mPosition1 == position1 - 1 )
                {
                    EPiece piece = board[position1, position2];
                    board[mPosition1, mPosition2] = piece;
                    board[position1, position2] = EPiece.Empty;
                    madeMove = true;
                    selectedPiece = Tuple.Create(mPosition1, mPosition2);
                    if ((mPosition1 == 0) || (mPosition1 == 7))
                        promotePiece(mPosition1, mPosition2);
                }
            }
        }

        public void MakeMoveWithCapture(int position1, int position2, int mPosition1, int mPosition2)
        {
            if (mPosition2 != position2 + 2 && mPosition2 != position2 - 2)
                return;

            if (board[position1, position2]!= EPiece.WhiteSoldier)
            {
                if ( mPosition1 == position1 + 2 )
                {
                    if(board[position1, position2] != EPiece.WhiteKing)
                        if (board[(mPosition1 + position1) / 2, (mPosition2 + position2) / 2] == EPiece.WhiteSoldier || board[(mPosition1 + position1) / 2, (mPosition2 + position2) / 2] == EPiece.WhiteKing)
                        {
                            EPiece piece = board[position1, position2];
                            board[(mPosition1 + position1) / 2, (mPosition2 + position2) / 2] = EPiece.Empty;
                            board[mPosition1, mPosition2] = piece;
                            board[position1, position2] = EPiece.Empty;
                            whitePiecesNumber--;
                            madeMove = true;
                            selectedPiece = Tuple.Create(mPosition1, mPosition2);
                            if ((mPosition1 == 0) || (mPosition1 == 7))
                                promotePiece(mPosition1, mPosition2);
                        }

                    if (board[position1, position2] == EPiece.WhiteKing)
                        if (board[(mPosition1 + position1) / 2, (mPosition2 + position2) / 2] == EPiece.RedSoldier || board[(mPosition1 + position1) / 2, (mPosition2 + position2) / 2] == EPiece.RedKing)
                        {
                            EPiece piece = board[position1, position2];
                            board[(mPosition1 + position1) / 2, (mPosition2 + position2) / 2] = EPiece.Empty;
                            board[mPosition1, mPosition2] = piece;
                            board[position1, position2] = EPiece.Empty;
                            redPiecesNumber--;
                            madeMove = true;
                            selectedPiece = Tuple.Create(mPosition1, mPosition2);
                            if ((mPosition1 == 0) || (mPosition1 == 7))
                                promotePiece(mPosition1, mPosition2);
                        }
                }
                
            }

            if (board[position1, position2] != EPiece.RedSoldier)
            {
                if (mPosition1 == position1 - 2 )
                {
                    if (board[position1, position2] == EPiece.RedKing)
                        if (board[(mPosition1 + position1) / 2, (mPosition2 + position2) / 2] == EPiece.WhiteSoldier || board[(mPosition1 + position1) / 2, (mPosition2 + position2) / 2] == EPiece.WhiteKing)
                        {
                            EPiece piece = board[position1, position2];
                            board[(mPosition1 + position1) / 2, (mPosition2 + position2) / 2] = EPiece.Empty;
                            board[mPosition1, mPosition2] = piece;
                            board[position1, position2] = EPiece.Empty;
                            whitePiecesNumber--;
                            selectedPiece = Tuple.Create(mPosition1, mPosition2);
                            madeMove = true;
                            if ((mPosition1 == 0 ) || (mPosition1 == 7 ))
                                promotePiece(mPosition1, mPosition2);
                        }

                    if (board[position1, position2] != EPiece.RedKing)
                        if (board[(mPosition1 + position1) / 2, (mPosition2 + position2) / 2] == EPiece.RedSoldier || board[(mPosition1 + position1) / 2, (mPosition2 + position2) / 2] == EPiece.RedKing)
                        {
                            EPiece piece = board[position1, position2];
                            board[(mPosition1 + position1) / 2, (mPosition2 + position2) / 2] = EPiece.Empty;
                            board[mPosition1, mPosition2] = piece;
                            board[position1, position2] = EPiece.Empty;
                            redPiecesNumber--;
                            selectedPiece = Tuple.Create(mPosition1, mPosition2);
                            madeMove = true;
                            if ((mPosition1 == 0 ) || (mPosition1 == 7 ))
                                promotePiece(mPosition1, mPosition2);
                        }
                }

            }
        }

        public void promotePiece(int pos1, int pos2)
        {
            if (board[pos1, pos2] == EPiece.WhiteSoldier)
                board[pos1, pos2] = EPiece.WhiteKing;
            if (board[pos1, pos2] == EPiece.RedSoldier)
                board[pos1, pos2] = EPiece.RedKing;
        }

        public bool MovePossible()
        {
            
            if (selectedPiece != null && selectedPiece.Item1!=-1 )
            {
                EPiece piece = board[selectedPiece.Item1, selectedPiece.Item2];
                if (piece != EPiece.RedSoldier && selectedPiece.Item1-1>= 0)
                {
                    if (piece != EPiece.RedKing)
                    {
                        if(selectedPiece.Item1 - 2>= 0 && selectedPiece.Item2 + 2<8 && board[selectedPiece.Item1 - 2, selectedPiece.Item2 + 2] == EPiece.Empty)
                        if (selectedPiece.Item2 + 1 < 8   && board[selectedPiece.Item1 - 1, selectedPiece.Item2 + 1] == EPiece.RedSoldier || board[selectedPiece.Item1 - 1, selectedPiece.Item2 + 1] == EPiece.RedKing)
                            return true;
                        if (selectedPiece.Item1 - 2 >= 0  && selectedPiece.Item2 - 2 >= 0 && board[selectedPiece.Item1 - 2, selectedPiece.Item2 - 2] == EPiece.Empty)
                            if (selectedPiece.Item2 - 1 >= 0 && board[selectedPiece.Item1 - 1, selectedPiece.Item2 - 1] == EPiece.RedSoldier || board[selectedPiece.Item1 - 1, selectedPiece.Item2 - 1] == EPiece.RedKing)
                            return true;
                    }
                    if (piece == EPiece.RedKing)
                    {
                        if (selectedPiece.Item1 - 2 >= 0  && selectedPiece.Item2 + 2 < 8 && board[selectedPiece.Item1 - 2, selectedPiece.Item2 + 2] == EPiece.Empty)
                            if (selectedPiece.Item2 + 1 < 8 && board[selectedPiece.Item1 - 1, selectedPiece.Item2 + 1] == EPiece.WhiteSoldier || board[selectedPiece.Item1 - 1, selectedPiece.Item2 + 1] == EPiece.WhiteKing)
                            return true;
                        if (selectedPiece.Item1 - 2 >= 0 && selectedPiece.Item2 - 2 >= 0 && board[selectedPiece.Item1 - 2, selectedPiece.Item2 - 2] == EPiece.Empty)
                            if (selectedPiece.Item2 - 1 >= 0 && board[selectedPiece.Item1 - 1, selectedPiece.Item2 - 1] == EPiece.WhiteSoldier || board[selectedPiece.Item1 - 1, selectedPiece.Item2 - 1] == EPiece.WhiteKing)
                            return true;
                    }


                }

                if (piece != EPiece.WhiteSoldier && selectedPiece.Item1 + 1 < 8)
                {
                    if (piece != EPiece.WhiteKing)
                    {
                        if (selectedPiece.Item1 + 2 < 8 && selectedPiece.Item2 + 2 < 8 && board[selectedPiece.Item1 + 2, selectedPiece.Item2 + 2] == EPiece.Empty)
                            if (selectedPiece.Item2 + 1 < 8 && board[selectedPiece.Item1 + 1, selectedPiece.Item2 + 1] == EPiece.WhiteSoldier || board[selectedPiece.Item1 + 1, selectedPiece.Item2 + 1] == EPiece.WhiteKing)
                            return true;

                        if (selectedPiece.Item1 + 2 < 8 && selectedPiece.Item2 - 2 >= 0 && board[selectedPiece.Item1 + 2, selectedPiece.Item2 - 2] == EPiece.Empty)
                            if (selectedPiece.Item2 - 1 >= 0 && board[selectedPiece.Item1 + 1, selectedPiece.Item2 - 1] == EPiece.WhiteSoldier || board[selectedPiece.Item1 + 1, selectedPiece.Item2 - 1] == EPiece.WhiteKing)
                            return true;
                    }
                    if (piece == EPiece.WhiteKing)
                    {
                        if (selectedPiece.Item1 + 2 < 8 && selectedPiece.Item2 + 2 < 8 && board[selectedPiece.Item1 + 2, selectedPiece.Item2 + 2] == EPiece.Empty)
                            if (selectedPiece.Item2 + 1 < 8 && board[selectedPiece.Item1 + 1, selectedPiece.Item2 + 1] == EPiece.RedSoldier || board[selectedPiece.Item1 + 1, selectedPiece.Item2 + 1] == EPiece.RedKing)
                            return true;

                        if (selectedPiece.Item1 + 2 < 8 && selectedPiece.Item2 - 2 >= 0 && board[selectedPiece.Item1 + 2, selectedPiece.Item2 - 2] == EPiece.Empty)
                            if (selectedPiece.Item2 - 1 >= 0 && board[selectedPiece.Item1 + 1, selectedPiece.Item2 - 1] == EPiece.RedSoldier || board[selectedPiece.Item1 + 1, selectedPiece.Item2 - 1] == EPiece.RedKing)
                            return true;
                    }


                }


            }

            return false;
        }


    }
}
