using Minesweeper.Model;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Minesweeper.ViewModel
{
  class MinewseeperBoardViewModel : BindableBase
  {
    private string _smileyImageSource;
    private ObservableCollection<ObservableCollection<SquareViewModel>> _board;

    public ObservableCollection<ObservableCollection<SquareViewModel>> Board
    {
      get
      {
        return _board;
      }
      set
      {
        _board = value;
        OnPropertyChanged();
      }
    }
    public List<Square> BombList { get; set; }
    public ICommand NewGameCommand { get; set; }
    public string SmileyImageSource
    {
      get
      {
        return _smileyImageSource;
      }
      set
      {
        _smileyImageSource = value;
        OnPropertyChanged();
      }
    }


    public MinewseeperBoardViewModel()
    {
      NewGameCommand = new DelegateCommand(NewGameCommandReceived);
      InitializeBoard();
    }

    private void NewGameCommandReceived()
    {
      InitializeBoard();
    }

    private void InitializeBoard()
    {
      SmileyImageSource = SquareImageConstants.Smiley;
      var coordinatesList = new List<Point>();
      Board = new ObservableCollection<ObservableCollection<SquareViewModel>>();
      for (int i = 0; i < 9; i++)
      {
        Board.Add(new ObservableCollection<SquareViewModel>());
        for (int j = 0; j < 9; j++)
        {
          coordinatesList.Add(new Point(i, j));
          var squareViewModel = new SquareViewModel
          {
            Square = new Square { Coordinates = new Point(i, j) },
          };
          squareViewModel.BombClickedEvent += RevealAllBombs;
          squareViewModel.EmptySquareClickedEvent += RevealEmptySquares;
          squareViewModel.CheckIfGameIsOverEvent += CheckIfGameIsOver;
          Board.Last().Add(squareViewModel);
        }
      }

      BombList = new List<Square>();
      Random rand = new Random();
      for (int i = 0; i < 10; i++)
      {
        int index = rand.Next(coordinatesList.Count());
        BombList.Add(Board[(int)coordinatesList[index].X][(int)coordinatesList[index].Y].Square);
        Board[(int)coordinatesList[index].X][(int)coordinatesList[index].Y].Square.IsBomb = true;
        coordinatesList.Remove(coordinatesList[index]);
      }

      for (int i = 0; i < 9; i++)
      {
        for (int j = 0; j < 9; j++)
        {
          if (!Board[i][j].Square.IsBomb)
          {
            Board[i][j].Square.Number = GetBombsNumber(i, j);
          }
        }
      }
    }

    private void CheckIfGameIsOver(object sender, EventArgs e)
    {
      int count = 0;
      foreach (var line in Board)
      {
        count += line.Count(l => l.Square.DisplayedImage == SquareImageConstants.Square || l.Square.DisplayedImage == SquareImageConstants.Flag);
      }
      if (count == BombList.Count())
      {
        MessageBox.Show("Congratulations! You've won!");
        InitializeBoard();
      }
    }

    private void RevealEmptySquares(object sender, EventArgs e)
    {
      var square = (sender as SquareViewModel).Square;
      Queue<Square> queue = new Queue<Square>();
      queue.Enqueue(square);
      while (queue.Any())
      {
        var sq = queue.Peek();

        queue.Dequeue();

        for (int i = (int)sq.Coordinates.X - 1; i <= (int)sq.Coordinates.X + 1; i++)
        {
          for (int j = (int)sq.Coordinates.Y - 1; j <= (int)sq.Coordinates.Y + 1; j++)
          {
            if (i >= 0 && j >= 0 && i < 9 && j < 9)
            {
              if (!Board[i][j].Square.IsBomb)
              {
                if (!queue.Contains(Board[i][j].Square) && Board[i][j].Square.Number == 0 && Board[i][j].Square.DisplayedImage == SquareImageConstants.Square)
                {
                  queue.Enqueue(Board[i][j].Square);
                }
                Board[i][j].Square.DisplayedImage = SquareImageConstants.GetSquare(Board[i][j].Square.Number);
              }
            }
          }
        }

      }
    }

    private int GetBombsNumber(int x, int y)
    {
      var bombsNumber = 0;
      for (int i = x - 1; i <= x + 1; i++)
      {
        for (int j = y - 1; j <= y + 1; j++)
        {
          if (i >= 0 && j >= 0 && i < 9 && j < 9 && Board[i][j].Square.IsBomb)
          {
            bombsNumber++;
          }
        }
      }
      return bombsNumber;
    }

    private void RevealAllBombs(object sender, EventArgs e)
    {
      SmileyImageSource = SquareImageConstants.GameOverSmiley;
      var coordinates = (sender as SquareViewModel).Square.Coordinates;
      foreach (var bomb in BombList)
      {
        if (bomb.Coordinates != coordinates)
        {
          bomb.DisplayedImage = SquareImageConstants.Bomb;
        }
      }
    }
  }
}
