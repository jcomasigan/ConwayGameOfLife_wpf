﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConwayGameOfLife_wpf
{
    public class Coordinates
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Coordinates(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    class GLife
    {
        public static Dictionary<Coordinates, CellState> GenerateLife(int gridSize, double probability = 0.6)
        {
            Random rand = new Random();
            Dictionary<Coordinates, CellState> dic = new Dictionary<Coordinates, CellState>();
            for(int col = 0; col <= gridSize; col++ )
            {
                for(int row = 0; row <= gridSize; row++)
                {
                    Coordinates coord = new Coordinates(col, row);
                    bool deadOrAlive = rand.NextDouble() < probability;
                    dic.Add(coord, deadOrAlive? CellState.ALIVE : CellState.DEAD);
                }
            }
            return dic;
        }

        public static Dictionary<Coordinates, CellState> NextGen(int gridSize, Dictionary<Coordinates, CellState> dic)
        {
            Dictionary<Coordinates, CellState> newDic = new Dictionary<Coordinates, CellState>();
            for (int col = 0; col <= gridSize; col++)
            {
                for (int row = 0; row <= gridSize; row++)
                {
                    Coordinates coord = new Coordinates(col, row);
                    CellState currentState = CellState.NULL;
                    foreach (KeyValuePair<Coordinates, CellState> cell in dic)
                    {
                        Coordinates cellCoord = cell.Key;
                        if(cellCoord.X == coord.X && cellCoord.Y == coord.Y)
                        {
                            currentState = cell.Value;
                            break;
                        }
                    }
                    CellState newState = SetState(dic, coord, currentState);
                    newDic.Add(coord, newState);                   
                }
            }

            return newDic;
        }

        private static CellState SetState(Dictionary<Coordinates, CellState> dic, Coordinates currentCoordinates, CellState currentState)
        {
            if(currentState == CellState.NULL)
            {
                string haha;
            }
            int xD = 0, yD = 0; //direction of the cell to check
            int x = currentCoordinates.X;
            int y = currentCoordinates.Y;
            int neighbours = 0;
            for(int i = 1; i <= 8; i++)
            {
                //check state clockwise starting from immediate left of cell
                GetXnY(i, ref x, ref y);
                //int newX = x - xD;
                //int newY = y - yD;
                CellState previousState = CellState.NULL;
                Coordinates coord = new Coordinates(x, y);
                if (coord.X > 0 && coord.Y > 0)
                {
                    foreach (KeyValuePair<Coordinates, CellState> cell in dic)
                    {
                        Coordinates cellCoord = cell.Key;
                        if (cellCoord.X == coord.X && cellCoord.Y == coord.Y)
                        {
                            previousState = cell.Value;
                            break;
                        }

                    }
                }
                if (previousState == CellState.ALIVE)
                {
                    neighbours++;
                }
            }
            if (currentState == CellState.DEAD && neighbours == 3)
            {
                //Any dead cell with exactly three live neighbours becomes a live cell, as if by reproduction
                return CellState.ALIVE;
            }
            else if (neighbours < 2)
            {
                //Any live cell with fewer than two live neighbours dies, as if caused by underpopulation.
                return CellState.DEAD;
            }
            else if(neighbours == 3 || neighbours == 2)
            {
                //Any live cell with two or three live neighbours lives on to the next generation.
                return CellState.ALIVE;
            }
            else
            {
                //Any live cell with more than three live neighbours dies, as if by overpopulation.
                return CellState.DEAD;
            }
        }

        /// <summary>
        /// LOL Will write a better algorithim later.
        /// </summary>
        private static void GetXnY(int i, ref int x, ref int y)
        {
            //Please don't laugh
            switch(i)
            {
                case 1:
                    {
                        x -=1;
                        break;
                    }
                case 2:
                    {
                        x -= 1;
                        y -= 1;
                        break;
                    }
                case 3:
                    {
                        y -= 1;
                        break;
                    }
                case 4:
                    {
                        x += 1;
                        y -= 1;
                        break;
                    }
                case 5:
                    {
                        x += 1;
                        break;
                    }
                case 6:
                    {
                        x += 1;
                        y += 1;
                        break;
                    }
                case 7:
                    {
                        y += 1;
                        break;
                    }
                case 8:
                    {
                        x -= 1;
                        y += 1;
                        break;
                    }
            }
        }


        private static bool GetState(Dictionary<Coordinates, CellState> dic, Coordinates coord)
        {
            return dic.ContainsKey(coord);
        }
    }
}