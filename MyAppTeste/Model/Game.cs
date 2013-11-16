using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace MyAppTeste.Model
{
    public class Game
    {
        private UIElement[,] board;
        private List<UIElement> piecesInOrder;
        public int Moves { get; private set; } 



        public Game(List<UIElement> pieces)
        {
            piecesInOrder = pieces;

            board = new UIElement[3, 3];
            // PutPiecesInOrder();
            ShufflePieces();
        }

        private void PutPiecesInOrder()
        {
            var campo = 0;
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (campo < 8)
                        board[i, j] = piecesInOrder[campo++];
                }
            }
        }

        public void Move(UIElement piece)
        {      
            var index = board.GetIndex(piece);

            if (index == null) return;

            if (index[0] > 0)
                if (board[index[0] - 1, index[1]] == null)
                {
                    board[index[0], index[1]] = null;
                    board[index[0] - 1, index[1]] = piece;


                    Grid.SetColumn((FrameworkElement)piece, index[0] - 1);
                    Moves++;
                    return;
                }

            if (index[1] > 0)
                if (board[index[0], index[1] - 1] == null)
                {
                    board[index[0], index[1]] = null;
                    board[index[0], index[1] - 1] = piece;

                    Grid.SetRow((FrameworkElement)piece, index[1] - 1);
                    Moves++;
                    return;
                }

            if (index[0] < 2)
                if (board[index[0] + 1, index[1]] == null)
                {
                    board[index[0], index[1]] = null;
                    board[index[0] + 1, index[1]] = piece;

                    Grid.SetColumn((FrameworkElement)piece, index[0] + 1);
                    Moves++;
                    return;
                }

            if (index[1] < 2)
                if (board[index[0], index[1] + 1] == null)
                {
                    board[index[0], index[1]] = null;
                    board[index[0], index[1] + 1] = piece;

                    Grid.SetRow((FrameworkElement)piece, index[1] + 1);
                    Moves++;
                    return;
                }
        }

        public bool BoardIsInOrder()
        {
            var campo = 0;
            for (int j = 0; j < board.GetLength(0); j++)
            {
                for (int i = 0; i < board.GetLength(1); i++)
                {
                    if (campo < 8 && board[i, j] != piecesInOrder[campo++])
                        return false;
                }
            }

            return true;
        }

        public void HidePieces()
        {
            foreach (var piece in piecesInOrder)
            {
                piece.Visibility = Visibility.Collapsed;
            }
        }

        public void ShowPieces()
        {
            foreach (var piece in piecesInOrder)
            {
                piece.Visibility = Visibility.Visible;
            }
        }

        public void ShufflePieces()
        {
            Moves = 0;

            var r = new Random();
            var shufflePieces = piecesInOrder.OrderBy(p => r.Next()).ToList();

            var campo = 0;
            for (int j = 0; j < board.GetLength(0); j++)
            {
                for (int i = 0; i < board.GetLength(1); i++)
                {
                    if (campo < 8)
                    {
                        var piece = shufflePieces[campo++];

                        board[i, j] = piece;

                        Grid.SetColumn((FrameworkElement)piece, i);
                        Grid.SetRow((FrameworkElement)piece, j);

                    }
                    else
                    {
                        board[i, j] = null;
                    }
                }
            }

        }
    }

}

public static class ArrayExtension
{
    public static int[] GetIndex<T>(this T[,] array, T o)
    {
        for (int i = 0; i < array.GetLength(0); i++)
        {
            for (int j = 0; j < array.GetLength(1); j++)
            {
                if (o.Equals(array[i, j]))
                    return new int[] { i, j };
            }
        }

        return null;
    }
}