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

        public event PropertyChangedEventHandler PropertyChanged;

        public RelayCommand SortByNameCommand { get; set; }

        public RelayCommand SortByScoreCommand { get; set; }

        public RelayCommand SortByLevelCommand { get; set; }

        public RelayCommand ClearCommand { get; set; }

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

            this.scoreBoard = new HighScoreBoard();
            //TODO: Handle reading file and adding data to scoreboard list

            this.Scores = this.scoreBoard.Scores.ToObservableCollection();
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
            //TODO: Handle removing all data from file

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

        protected virtual void onPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
