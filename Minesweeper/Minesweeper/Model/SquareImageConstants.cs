using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Model
{
  static public class SquareImageConstants
  {
    static public string Bomb = "../../Resources/bomb.png";
    static public string RedBomb = "../../Resources/red_bomb.png";
    static public string Smiley = "../../Resources/smiley.png";
    static public string GameOverSmiley = "../../Resources/game_over_smiley.png";
    static public string Square = "../../Resources/square.png";
    static public string Square0 = "../../Resources/square0.png";
    static public string Square1 = "../../Resources/square1.png";
    static public string Square2 = "../../Resources/square2.png";
    static public string Square3 = "../../Resources/square3.png";
    static public string Square4 = "../../Resources/square4.png";
    static public string Square5 = "../../Resources/square5.png";
    static public string Flag = "../../Resources/flag.png";

    static public string GetSquare(int number)
    {
      return string.Format("../../Resources/square{0}.png", number);
    }
  }
}

