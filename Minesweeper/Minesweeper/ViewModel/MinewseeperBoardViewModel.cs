using Minesweeper.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.ViewModel
{
  class MinewseeperBoardViewModel
  {
    public ObservableCollection<ObservableCollection<SquareViewModel>> Board { get; set; }

    public MinewseeperBoardViewModel()
    {
      InitializeBoard();
    }

    private void InitializeBoard()
    {
      Board = new ObservableCollection<ObservableCollection<SquareViewModel>>();
      for (int i = 0; i < 9; i++)
      {
        var list = new ObservableCollection<SquareViewModel>();
        for (int j = 0; j < 9; j++)
        {
          list.Add(new SquareViewModel
          {
            Square = new Square()
          });
        }
          Board.Add(list);
      }
    }
  }
}
