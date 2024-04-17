using Joc_Dame.Model;
using Joc_Dame.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Joc_Dame.Properties;
using ICommandDemoAgain.Commands;
using Joc_Dame.ViewModel;
using Checkers.ViewModels;
using System.Windows;

namespace Joc_Dame.ViewModel
{
    public class GameViewModel : BaseViewModel
    {
        GameLogic gameLogic = new GameLogic();
        public ObservableCollection<PieceImage> Pieces { get; private set; } = new ObservableCollection<PieceImage>();
        private bool _isMultipleJumpsEnabled;
        private bool _isCheckBoxEnabled = true;
        private bool _isTableActive = false;
        private string _currentPlayer;
        private Visibility _tableVisibility = Visibility.Hidden;
        public string GameState = "";
       

        int Clickpos1 =-1;
        int Clickpos2=-1;
        public ICommand StartGameCommand { get; private set;}
        public ICommand SwitchTurnCommand { get; private set;}
        public ICommand PieceClickedCommand { get; private set;}
        public ICommand SaveGameCommand { get; private set;}
        public ICommand LoadGameCommand { get; private set;}


        public GameViewModel()
        {
            DrawPieces();

            gameLogic.multipleJumps = _isMultipleJumpsEnabled;
            StartGameCommand = new RelayCommand(o => OnStartGame());
            PieceClickedCommand = new RelayCommand(o => OnPieceClicked(o));
            SaveGameCommand = new RelayCommand(o => OnSaveGame());
            LoadGameCommand = new RelayCommand(o => OnLoadGame());


        }

       
        void OnStartGame()
        {
            gameLogic.StartGame(_isMultipleJumpsEnabled);
            gameLogic.multipleJumps = _isMultipleJumpsEnabled;
            IsTableActive = true;
            
            TableVisibility = Visibility.Visible;
            DrawPieces();
            IsCheckBoxEnabled = false;
            IsTableActive = true;
            
            if (gameLogic.isRedTurn == true)
            {
                CurrentPlayer = "Red";
            }
            else
            {
                CurrentPlayer = "Black";
            }
            
        }

        void OnSaveGame()
        {
            gameLogic.SaveGame();
        }

        void OnLoadGame()
        {
            gameLogic.LoadGame();
            DrawPieces();
            IsTableActive = true;
            IsCheckBoxEnabled = false;
            TableVisibility = Visibility.Visible;
            if (gameLogic.isRedTurn == true)
            {
                CurrentPlayer = "Red";
            }
            else
            {
                CurrentPlayer = "Black";
            }
        }

        void OnPieceClicked(object o )
        {
            var piece = o as PieceImage;
            EPiece type = gameLogic.board.board[piece.Position1, piece.Position2];
            

            if ( VerifyTurn(type)&& gameLogic.board.madeMove == false)
            {
                Clickpos1 = piece.Position1;
                Clickpos2 = piece.Position2;
            }
            else
            if (gameLogic.board.board[piece.Position1, piece.Position2] == EPiece.Empty)
            {
                if (gameLogic.board.madeMove == true)
                {
                    gameLogic.MovePiece(gameLogic.board.selectedPiece.Item1, gameLogic.board.selectedPiece.Item2, piece.Position1, piece.Position2);
                }
                else
                {
                    gameLogic.MovePiece(Clickpos1, Clickpos2, piece.Position1, piece.Position2);
                }
                    DrawPieces();
                    
               
            }
            if(gameLogic.checkWinner()!=EPiece.Empty)
            {                
                if (gameLogic.checkWinner() == EPiece.RedSoldier || gameLogic.checkWinner() == EPiece.RedKing)
                {
                    MessageBox.Show("Red wins");
                }
                else
                {
                    MessageBox.Show("Black wins");
                }
                IsTableActive = false;
                IsCheckBoxEnabled = true;
            }
            
        }

        public string CurrentPlayer
        {
            get { return _currentPlayer; }
            set
            {
                if (_currentPlayer != value)
                {
                    _currentPlayer = value;
                    OnPropertyChanged(nameof(CurrentPlayer));
                }
            }
        }

        public Visibility TableVisibility
        {
            get { return _tableVisibility; }
            set
            {
                if (_tableVisibility != value)
                {
                    _tableVisibility = value;
                    OnPropertyChanged(nameof(TableVisibility));
                }
            }
        }

        public bool IsTableActive
        {
            get { return _isTableActive; }
            set
            {
                if (_isTableActive != value)
                {
                    _isTableActive = value;
                    OnPropertyChanged(nameof(IsTableActive));
                }
            }
        }

        public bool IsCheckBoxEnabled
        {
            get { return _isCheckBoxEnabled; }
            set
            {
                if (_isCheckBoxEnabled != value)
                {
                    _isCheckBoxEnabled = value;
                    OnPropertyChanged(nameof(IsCheckBoxEnabled));
                }
            }
        }

        public bool IsMultipleJumpsEnabled
        {
            get { return _isMultipleJumpsEnabled; }
            set
            {
                if (_isMultipleJumpsEnabled != value)
                {
                    _isMultipleJumpsEnabled = value;
                    OnPropertyChanged(nameof(IsMultipleJumpsEnabled));
                }
            }
        }


        void DrawPieces()
        {
            if (gameLogic.isRedTurn == true)
            {
                CurrentPlayer = "Red";
            }
            else
            {
                CurrentPlayer = "Black";
            }
            Pieces.Clear();
            for(int i=0; i<8; i++)
                for(int j=0; j<8; j++)
                {
                    if (gameLogic.board.board[i,j] == EPiece.RedSoldier)
                    {
                        Pieces.Add(new PieceImage(i, j, "/Assets/RedSoldier.png"));
                    }
                    else if (gameLogic.board.board[i,j] == EPiece.WhiteSoldier)
                    {
                        Pieces.Add(new PieceImage(i, j, "/Assets/BlackSoldier.png"));
                    }
                    else if (gameLogic.board.board[i,j] == EPiece.RedKing)
                    {
                        Pieces.Add(new PieceImage(i, j, "/Assets/RedKing.png"));
                    }
                    else if (gameLogic.board.board[i,j] == EPiece.WhiteKing)
                    {
                        Pieces.Add(new PieceImage(i, j, "/Assets/BlackKing.png"));
                    }
                    else
                    {
                        Pieces.Add(new PieceImage(i, j, "/Assets/Empty.png"));
                    }
                }
        }

        bool VerifyTurn(EPiece piece)
        {
            if (gameLogic.isRedTurn == true && (piece == EPiece.RedSoldier || piece == EPiece.RedKing))
            {
                return true;
            }
            if (gameLogic.isRedTurn == false && (piece == EPiece.WhiteSoldier || piece == EPiece.WhiteKing))
            {
                return true;
            }
            return false;
        }


    }

    public class PieceImage
    {
        public int Position1 { get; set; }
        public int Position2 { get; set; }
        public string ImagePath { get; set; }
        
        public PieceImage(int position1, int position2, string imagePath)
        {
            Position1 = position1;
            Position2 = position2;
            ImagePath = imagePath;
        }

    }
}
