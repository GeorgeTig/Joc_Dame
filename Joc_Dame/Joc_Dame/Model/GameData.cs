using Joc_Dame.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using System.Xml.Serialization;
using System.IO;
using Microsoft.Win32;
using Joc_Dame.Model;

namespace Joc_Dame.Model
{
    public class GameData
    {
        public int[] board { get; set; }
        public bool RedTurn { get; set; }
        public bool multipleJumps { get; set; }
        public GameData(EPiece[,] board, bool redTurn, bool multipleJumps)
        {
            
            RedTurn = redTurn;
            this.multipleJumps = multipleJumps;
            this.board = new int[8*8];
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    this.board[i * 8 + j] = (int)board[i, j];
                }
            }
        }

        public GameData(int[] board, bool redTurn, bool multipleJumps)
        {
            this.board = board;
            RedTurn = redTurn;
            this.multipleJumps = multipleJumps;
        }

        public GameData()
        {
        }

        public void SaveGame()
        {
            GameData gameData = new GameData(board, RedTurn, multipleJumps);
            XmlSerializer serializer = new XmlSerializer(typeof(GameData));

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.RestoreDirectory = true;
            if (saveFileDialog.ShowDialog() == true)
            {
                using (TextWriter writer = new StreamWriter(saveFileDialog.FileName))
                {
                    serializer.Serialize(writer, gameData);
                }
            }
            
        }

        public void LoadGame()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(GameData));

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;

           if(openFileDialog.ShowDialog() == true)
                using (TextReader reader = new StreamReader(openFileDialog.FileName))
                {
                    GameData gameData = (GameData)serializer.Deserialize(reader);
                    board = gameData.board;
                    RedTurn = gameData.RedTurn;
                    multipleJumps = gameData.multipleJumps;
                }
            
        }
    }
}
