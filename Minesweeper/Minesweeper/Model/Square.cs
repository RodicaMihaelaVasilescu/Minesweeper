using Minesweeper.Model;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;

namespace Minesweeper.Model
{
  public class Square : BindableBase
  {
    private string _displayedImage = SquareImageConstants.Square;


    public int Number { get; set; }

    public bool IsBomb { get; set; }

    public Point Coordinates { get; set; }

    public int Size { get; set; } = 25;

    public string DisplayedImage
    {
      get
      {
        return _displayedImage;
      }
      set
      {
        _displayedImage = value;
        OnPropertyChanged();
      }
    }

  }
}
