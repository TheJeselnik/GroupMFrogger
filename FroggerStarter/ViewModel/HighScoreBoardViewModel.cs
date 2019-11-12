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
    /// <summary>ViewModel for the HighScore Board.</summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public class HighScoreBoardViewModel : INotifyPropertyChanged
    {
        #region Data members

        private const int MaxSortSize = 10;

        private readonly HighScoreBoard scoreBoard;

        private ObservableCollection<HighScore> scores;

        private string playerInfo;

        #endregion

        #region Properties

        /// <summary>Gets the sort by name command.</summary>
        /// <value>The sort by name command.</value>
        public RelayCommand SortByNameCommand { get; }

        /// <summary>Gets the sort by score command.</summary>
        /// <value>The sort by score command.</value>
        public RelayCommand SortByScoreCommand { get; }

        /// <summary>Gets the sort by level command.</summary>
        /// <value>The sort by level command.</value>
        public RelayCommand SortByLevelCommand { get; }

        /// <summary>Gets the clear command.</summary>
        /// <value>The clear command.</value>
        public RelayCommand ClearCommand { get; }

        /// <summary>Gets the view score board game over command.</summary>
        /// <value>The view score board game over command.</value>
        public RelayCommand ViewScoreBoardGameOverCommand { get; }

        /// <summary>Gets the view score board start command.</summary>
        /// <value>The view score board start command.</value>
        public RelayCommand ViewScoreBoardStartCommand { get; }

        /// <summary>Gets the submit player name command.</summary>
        /// <value>The submit player name command.</value>
        public RelayCommand SubmitPlayerNameCommand { get; }

        /// <summary>Gets the display scores command.</summary>
        /// <value>The display scores command.</value>
        public RelayCommand DisplayScoresCommand { get; }

        /// <summary>Gets or sets the current player information.</summary>
        /// <value>The current player information.</value>
        public string CurrentPlayerInfo
        {
            get => this.playerInfo;
            set
            {
                this.playerInfo = value;
                this.onPropertyChanged();
            }
        }

        /// <summary>Gets or sets the scores.</summary>
        /// <value>The scores.</value>
        public ObservableCollection<HighScore> Scores
        {
            get => this.scores;
            set
            {
                this.scores = value;
                this.onPropertyChanged();
            }
        }

        #endregion

        #region Constructors

        /// <summary>Initializes a new instance of the <see cref="HighScoreBoardViewModel" /> class.</summary>
        public HighScoreBoardViewModel()
        {
            this.SortByNameCommand = new RelayCommand(this.sortByName, this.canSort);
            this.SortByScoreCommand = new RelayCommand(this.sortByScore, this.canSort);
            this.SortByLevelCommand = new RelayCommand(this.sortByLevel, this.canSort);
            this.ClearCommand = new RelayCommand(clearBoard, this.canClearBoard);
            this.ViewScoreBoardGameOverCommand = new RelayCommand(viewScoreBoardFromGameOver, this.canViewScoreBoard);
            this.ViewScoreBoardStartCommand = new RelayCommand(viewScoreBoardFromStart, this.canViewScoreBoard);
            this.SubmitPlayerNameCommand = new RelayCommand(this.writeHighScore, this.canCreateHighScore);
            this.DisplayScoresCommand = new RelayCommand(this.setupScoreBoard, this.canSort);

            this.scoreBoard = new HighScoreBoard();

            FileIoSerialization.BinaryDeserializer(this.scoreBoard);

            this.Scores = this.scoreBoard.Scores.ToObservableCollection();

            this.CurrentPlayerInfo = "No Name";
        }

        #endregion

        #region Methods

        /// <summary>Occurs when a property value changes.</summary>
        /// <returns>PropertyChangedEventHandler for ViewModel</returns>
        public event PropertyChangedEventHandler PropertyChanged;

        private bool canSort(object obj)
        {
            return this.scoreBoard.Count > 0 && this.scoreBoard != null;
        }

        private bool canClearBoard(object obj)
        {
            return this.scoreBoard.Count > 0 && this.scoreBoard != null;
        }

        private static void clearBoard(object obj)
        {
            FileIoSerialization.BinaryFileOverwrite();

            showClearedBoardMessageDialog();
        }

        private static async void showClearedBoardMessageDialog()
        {
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

        private static void viewScoreBoardFromStart(object obj)
        {
            openHighScoreBoardDialogFromStart();
        }

        private static void viewScoreBoardFromGameOver(object obj)
        {
            openHighScoreBoardDialogFromGameOver();
        }

        private static async void openHighScoreBoardDialogFromStart()
        {
            var highScoreDialog = new HighScoreBoardDialog {IsOpenedAtStartScreen = true};
            await highScoreDialog.ShowAsync();
        }

        private static async void openHighScoreBoardDialogFromGameOver()
        {
            var highScoreDialog = new HighScoreBoardDialog {IsOpenedAtStartScreen = false};
            await highScoreDialog.ShowAsync();
        }

        private bool canCreateHighScore(object obj)
        {
            return this.CurrentPlayerInfo != string.Empty;
        }

        private void writeHighScore(object obj)
        {
            this.handleWritingHighScoreToFile();
            openGameOverMenuDialog();
        }

        private void handleWritingHighScoreToFile()
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

            FileIoSerialization.BinarySerializer(highScore);

            this.Scores = this.scoreBoard.Scores.ToObservableCollection();
        }

        private void setupScoreBoard(object obj)
        {
            var result = this.scoreBoard.Scores.OrderByDescending(s => s.GameScore).Take(MaxSortSize);

            this.Scores = result.ToObservableCollection();
        }

        private static async void openGameOverMenuDialog()
        {
            var menuDialog = new GameOverMenuDialog();
            await menuDialog.ShowAsync();
        }

        /// <summary>Ons the property changed.</summary>
        /// <param name="propertyName">Name of the property.</param>
        protected virtual void onPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}