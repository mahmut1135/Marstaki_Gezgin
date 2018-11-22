using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marstaki_Gezgin
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Boyut giriniz örn: 5 5");
            var size = Console.ReadLine();
            Console.WriteLine("1. RobotkKonumunu giriniz örn: 1 3 N");
            var row1 = Console.ReadLine();
            Console.WriteLine("1. Robot hareketini giriniz örn: LMLMLMLMM");
            var move1 =Console.ReadLine();
            Console.WriteLine("2. RobotkKonumunu giriniz örn: 1 3 N");
            var row2 =Console.ReadLine();
            Console.WriteLine("1. Robot hareketini giriniz örn: LMLMLMLMM");
            var move2 = Console.ReadLine();

            //kontrol
            //girilen değerler hatalı ise exception fırlatmaması için talebinize göre ayrıca kontrol yapabilirim.


            Rover rover = new Rover(size);
            rover.SetPosition(row1);
            rover.CommandExecute(move1);
            Console.WriteLine(rover.GetPosition());
           

            Rover rover2 = new Rover(size);
            rover2.SetPosition(row2);
            rover2.CommandExecute(move2);
            Console.WriteLine(rover2.GetPosition());
            
            Console.ReadLine();
        }
    }
    public class Rover
    {
        public char Direction;

        public int X;
        public int Y;

        private int _width;
        private int _height;

        public Rover(string size)
        {
            string[] sizeArray = size.Split(' ');
            if (sizeArray.Length != 2)
                throw new Exception("Invalid size input.");
            bool isWidthInt = int.TryParse(sizeArray[0], out _width);
            bool isHeightInt = int.TryParse(sizeArray[1], out _height);
            if (isHeightInt == false | isWidthInt == false)
                throw new Exception("Invalid Size.");
        }

        public void SetPosition(string position)
        {
            string[] positionArray = position.ToUpper().Split(' ');
            if (positionArray.Length != 3)
                throw new Exception("Invalid position input.");
            if (this.ValidateAndAssign(positionArray))
                throw new Exception("Invalid position.");

        }

        private bool ValidateAndAssign(string[] positions)
        {

            bool isCorrectLenght = positions.Length > 2;
            bool isXInt = int.TryParse(positions[0], out X);
            bool isYInt = int.TryParse(positions[1], out Y);
            bool isDirectionChar = char.TryParse(positions[2], out Direction);

            return isCorrectLenght == false
                    | isXInt == false
                    | isYInt == false
                    | isDirectionChar == false;
        }

        public void CommandExecute(string commands)
        {
            if (ValidatePosition() == false)
                return;

            char[] commandArray = commands.ToUpper().ToCharArray();
            foreach (var command in commandArray)
            {
                if (command == 'M')
                    this.Move();
                else
                    this.Turn(command);
            }
        }

        public string GetPosition()
        {
            return string.Format("{0} {1} {2}", X, Y, Direction);
        }

        private bool ValidatePosition()
        {
            bool validX = X < _width;
            bool validY = Y < _height;
            return validX && validY;
        }

        private void Move()
        {

            switch (this.Direction)
            {
                case 'N':
                    if(_height != Y)
                    this.Y += 1;
                    break;

                case 'E':
                    if (_width != X)
                        this.X += 1;
                    break;

                case 'S':
                    if (Y != 0)
                        this.Y -= 1;
                    break;

                case 'W':
                    if (X != 0)
                        this.X -= 1;
                    break;
            }

        }

        private void Turn(char way)
        {
            switch (way)
            {
                case 'L':
                    this.TurnLeft();
                    break;
                case 'R':
                    this.TurnRight();
                    break;
                default:
                    throw new Exception("Invalid Way!");
            }
        }
        private void TurnLeft()
        {
            switch (Direction)
            {
                case 'W':
                    this.Direction = 'S';
                    break;
                case 'S':
                    this.Direction = 'E';
                    break;
                case 'E':
                    this.Direction = 'N';
                    break;
                case 'N':
                    this.Direction = 'W';
                    break;
            }
        }
        private void TurnRight()
        {
            switch (Direction)
            {
                case 'W':
                    this.Direction = 'N';
                    break;
                case 'N':
                    this.Direction = 'E';
                    break;
                case 'E':
                    this.Direction = 'S';
                    break;
                case 'S':
                    this.Direction = 'W';
                    break;
            }
        }
    }
}
