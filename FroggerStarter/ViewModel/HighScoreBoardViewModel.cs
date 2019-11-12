using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using FroggerStarter.Extensions;
using FroggerStarter.Model;
using FroggerStarter.Model.DataObjects;
using FroggerStarter.Utility;
using FroggerStarter.View;

namespace FroggerStarter.ViewModel
{
    public class HighScoreBoardViewModel : INotifyPropertyChanged
    {

        private const int MaxSortSize = 10;

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
            this.ClearCommand = new RelayCommand(clearBoard, this.canClearBoard);
            this.ViewScoreBoardGameOverCommand = new RelayCommand(viewScoreBoardFromGameOver, this.canViewScoreBoard);
            this.ViewScoreBoardStartCommand = new RelayCommand(viewScoreBoardFromStart, this.canViewScoreBoard);
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

        private static async void clearBoard(object obj)
        {
            FileIOSerialization.BinaryFileClear();

            var clearBoardDialog = new ClearedBoardMessageDialog();
            await clearBoardDialog.ShowAsync();
        }

        private void sortByName(object obj)
        {
            foreach (var highScore in this.scoreBoard.Scores)
            {
                highScore.SortDescriptionByName();
            }

            var result = this.scoreBoard.Scores.OrderBy(s => s.Name).Take(MaxSortSize);

            this.Scores = result.ToObservableCollection();
        }

        private void sortByScore(object obj)
        {
            foreach (var highScore in this.scoreBoard.Scores)
            {
                highScore.SortDescriptionDefault();
            }

            var result = this.scoreBoard.Scores.OrderByDescending(s => s.GameScore).Take(MaxSortSize);

            this.Scores = result.ToObservableCollection();
        }

        private void sortByLevel(object obj)
        {
            foreach (var highScore in this.scoreBoard.Scores)
            {
                highScore.SortDescriptionByLevel();
            }

            var result = this.scoreBoard.Scores.OrderByDescending(s => s.GameLevel).Take(MaxSortSize);

            this.Scores = result.ToObservableCollection();
        }

        private bool canViewScoreBoard(object obj)
        {
            return this.scoreBoard != null;
        }

        private static async void viewScoreBoardFromStart(object obj)
        {
            var dialog = new HighScoreBoardDialog { IsOpenedAtStartScreen = true };
            await dialog.ShowAsync();
        }

        private static async void viewScoreBoardFromGameOver(object obj)
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

            if (string.IsNullOrEmpty(name))
            {
                name = "No Name";
            }

            var highScore = new HighScore(name, score, level);

            FileIOSerialization.BinarySerializer(highScore);

            this.Scores = this.scoreBoard.Scores.ToObservableCollection();

            var menuDialog = new GameOverMenuDialog();
            await menuDialog.ShowAsync();
        }

        private void setupScoreBoard(object obj)
        {
            var result = this.scoreBoard.Scores.OrderByDescending(s => s.GameScore).Take(MaxSortSize);

            this.Scores = result.ToObservableCollection();
        }

        protected virtual void onPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
