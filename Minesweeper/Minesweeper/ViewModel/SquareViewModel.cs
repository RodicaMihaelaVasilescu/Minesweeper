using Minesweeper.Model;
using Prism.Commands;
using System;
using System.Windows.Input;

namespace Minesweeper.ViewModel
{
  class SquareViewModel : BindableBase
  {
    public Square Square { get; set; }

    public ICommand SquareClickedCommand { get; set; }
    public ICommand SquareRightClickedCommand { get; set; }

    public event EventHandler BombClickedEvent;
    public event EventHandler CheckIfGameIsOverEvent;
    public event EventHandler EmptySquareClickedEvent;

    public SquareViewModel()
    {
      SquareClickedCommand = new DelegateCommand(SquareClickedCommandReceived);
      SquareRightClickedCommand = new DelegateCommand(SquareRightClickedCommandReceived);
    }

    private void SquareRightClickedCommandReceived()
    {
      if (Square.DisplayedImage == SquareImageConstants.Square)
      {
        Square.DisplayedImage = SquareImageConstants.Flag;
      }
      else if (Square.DisplayedImage == SquareImageConstants.Flag)
      {
        Square.DisplayedImage = SquareImageConstants.Square;
      }
    }

    private void SquareClickedCommandReceived()
    {
      if (Square.DisplayedImage == SquareImageConstants.Flag)
      {
        return;
      }

      if (Square.IsBomb)
      {
        Square.DisplayedImage = SquareImageConstants.RedBomb;
        BombClickedEvent?.Invoke(this, new EventArgs());
      }
      else
      {
        if (Square.Number == 0)
        {
          EmptySquareClickedEvent?.Invoke(this, new EventArgs());
        }
        else
        {
          Square.DisplayedImage = SquareImageConstants.GetSquare(Square.Number);
        }

        CheckIfGameIsOverEvent?.Invoke(this, new EventArgs());
      }
    }

    public void RevealBomb()
    {
      Square.DisplayedImage =SquareImageConstants.Bomb;
    }
  }
}
