using Minesweeper.Model;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;

namespace Minesweeper.Model
{
  public class Square : BindableBase
  {
    public string Number { get; set; }

    public int Size { get; set; } = 25;

    private string _source = "../../Resources/square.png";
    public string Source
    {
      get
      {
        return _source;
      }
      set
      {
        _source = value;
        OnPropertyChanged();
      }
    }

    public int Line { get; set; }

    public Brush Background { get; set; } = (SolidColorBrush)(new BrushConverter().ConvertFrom("#c0c0c0"));// light gray

    public Brush Foreground { get; set; } = (SolidColorBrush)(new BrushConverter().ConvertFrom("#0000ff"));// blue

    public Square() { }

    public Square(Square square)
    {
      Background = square.Background;
      Number = square.Number;
    }
  }
}
