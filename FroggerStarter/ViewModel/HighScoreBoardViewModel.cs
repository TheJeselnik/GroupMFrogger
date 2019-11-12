using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using FroggerStarter.Extensions;
using FroggerStarter.Model;
using FroggerStarter.Utility;
using FroggerStarter.View;

namespace FroggerStarter.ViewModel
{
    public class HighScoreBoardViewModel : INotifyPropertyChanged
    {

        private readonly HighScoreBoard scoreBoard;

        private ObservableCollection<HighScore> scores;

        private string playerInfo;

        public event PropertyChangedEventHandler PropertyChanged;

        public RelayCommand SortByNameCommand { get; private set; }

        public RelayCommand SortByScoreCommand { get; private set; }

        public RelayCommand SortByLevelCommand { get; private set; }

        public RelayCommand ClearCommand { get; private set; }

        public RelayCommand ViewScoreBoardGameOverCommand { get; private set; }

        public RelayCommand ViewScoreBoardStartCommand { get; private set; }

        public RelayCommand SubmitPlayerNameCommand { get; private set; }

        public RelayCommand DisplayScoresCommand { get; private set; }

        public string CurrentPlayerInfo
        {
            get => this.playerInfo;
            set
            {
                this.playerInfo = value;
                this.onPropertyChanged();
            }
        }

        public ObservableCollection<HighScore> Scores
        {
            get => scores;
            set
            {
                this.scores = value;
                this.onPropertyChanged();
            }
        }

        public HighScoreBoardViewModel()
        {
            this.SortByNameCommand = new RelayCommand(this.sortByName, this.canSort);
            this.SortByScoreCommand = new RelayCommand(this.sortByScore, this.canSort);
            this.SortByLevelCommand = new RelayCommand(this.sortByLevel, this.canSort);
            this.ClearCommand = new RelayCommand(this.clearBoard, this.canClearBoard);
            this.ViewScoreBoardGameOverCommand = new RelayCommand(this.viewScoreBoardFromGameOver, this.canViewScoreBoard);
            this.ViewScoreBoardStartCommand = new RelayCommand(this.viewScoreBoardFromStart, this.canViewScoreBoard);
            this.SubmitPlayerNameCommand = new RelayCommand(this.createHighScore, this.canCreateHighScore);
            this.DisplayScoresCommand = new RelayCommand(this.setupScoreBoard, this.canSort);

            this.scoreBoard = new HighScoreBoard();

            FileIOSerialization.BinaryDeserializer(this.scoreBoard);

            this.Scores = this.scoreBoard.Scores.ToObservableCollection();

            this.CurrentPlayerInfo = "No Name";
        }

        private bool canSort(object obj)
        {
            return this.scoreBoard.Count > 0 && this.scoreBoard != null;
        }

        private bool canClearBoard(object obj)
        {
            return this.scoreBoard.Count > 0 && this.scoreBoard != null;
        }

        private async void clearBoard(object obj)
        {
            FileIOSerialization.BinaryFileClear();

            var clearBoardDialog = new ClearedBoardMessageDialog();
            await clearBoardDialog.ShowAsync();
        }

        private void sortByName(object obj)
        {
            foreach (var highScore in this.scoreBoard.Scores)
            {
                highScore.SortDescriptionDefault();
            }

            //TODO: Handle sorting list by name, score, level

            this.Scores = this.scoreBoard.Scores.ToObservableCollection();
        }

        private void sortByScore(object obj)
        {
            foreach (var highScore in this.scoreBoard.Scores)
            {
                highScore.SortDescriptionByScore();
            }

            //TODO: Handle sorting list by name, score, level

            this.Scores = this.scoreBoard.Scores.ToObservableCollection();
        }

        private void sortByLevel(object obj)
        {
            foreach (var highScore in this.scoreBoard.Scores)
            {
                highScore.SortDescriptionByLevel();
            }

            //TODO: Handle sorting list by name, score, level

            this.Scores = this.scoreBoard.Scores.ToObservableCollection();
        }

        private bool canViewScoreBoard(object obj)
        {
            return this.scoreBoard != null;
        }

        private async void viewScoreBoardFromStart(object obj)
        {
            var dialog = new HighScoreBoardDialog { IsOpenedAtStartScreen = true };
            await dialog.ShowAsync();
        }

        private async void viewScoreBoardFromGameOver(object obj)
        {
            var highScoreDialog = new HighScoreBoardDialog { IsOpenedAtStartScreen = false };
            await highScoreDialog.ShowAsync();
        }

        private bool canCreateHighScore(object obj)
        {
            return this.CurrentPlayerInfo != string.Empty;
        }

        private async void createHighScore(object obj)
        {
            const int nameIndex = 0;
            const int scoreIndex = 1;
            const int levelIndex = 2;

            var info = this.CurrentPlayerInfo.Split(",");

            var name = info[nameIndex];
            var score = Convert.ToInt32(info[scoreIndex]);
            var level = Convert.ToInt32(info[levelIndex]);

            var highScore = new HighScore(name, score, level);

            FileIOSerialization.BinarySerializer(highScore);

            this.Scores = this.scoreBoard.Scores.ToObservableCollection();

            var menuDialog = new GameOverMenuDialog();
            await menuDialog.ShowAsync();
        }

        private void setupScoreBoard(object obj)
        {
            //TODO: Sort scoreboard by top 10


            this.Scores = this.scoreBoard.Scores.ToObservableCollection();
        }

        protected virtual void onPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
