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

    public SquareViewModel()
    {
      SquareClickedCommand = new DelegateCommand(SquareClickedCommandReceived);
    }

    private void SquareClickedCommandReceived()
    {
      Square.Source = "../../Resources/empty_square.png";
    }
  }
}
