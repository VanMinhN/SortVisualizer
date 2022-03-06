using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SortVisualizer
{
    class QuickSort : ISortVisualizer
    {
        private int[] panelArr;
        private Graphics g;
        private int ArrSize = 0;
        private readonly int MaxVal;
        Brush BlackBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
        Brush WhiteBrush = new System.Drawing.SolidBrush(System.Drawing.Color.White);
        public QuickSort(int[] panelArray, Graphics graph, int MaxValue, int ArraySize)
        {
            panelArr = panelArray;
            g = graph;
            MaxVal = MaxValue;
            ArrSize = ArraySize;
        }

        /*
         * Sort Algorithm: QuickSort Sort
         */
        public void NextSort()
        {
            int size = panelArr.Length;
            quickSort(0, size-1);
            
        }

        private void quickSort(int low, int high)
        {
            if (low < high)
            {
                // find pivot element such that
                // elements smaller than pivot are on the left
                // elements greater than pivot are on the right
                int pivot = partition(low, high);
                quickSort(low, pivot - 1);
                quickSort(pivot + 1, high);
            }
        }

        private int partition(int low, int high)
        {
            int pivot = panelArr[high];
            int lowIndex = (low - 1);
            for (int j = low; j<high; j++)
            {
                if (panelArr[j] < pivot)
                {
                    lowIndex++;
                    Swap(lowIndex, j);
                }
            }
            Swap(lowIndex+1, high);
            return (lowIndex + 1);
        }

        private void Swap(int i, int j)
        {
            int temp = panelArr[i];
            panelArr[i] = panelArr[j];
            panelArr[j] = temp;
            DrawNewValue(i, panelArr[i]);
            DrawNewValue(j, panelArr[j]);
        }

        public bool IsSorted()
        {
            //Iterate and check if the array is sorted or not
            for (int i = 0; i < panelArr.Count() - 1; i++)
            {
                if (panelArr[i] > panelArr[i + 1]) return false;
            }
            return true;
        }

        private void DrawNewValue(int pos, int height)
        {
            /*
             * Remove the old value from the panel
             */
            if (pos >= 2)
            {
                Thread.Sleep(18);
                g.FillRectangle(BlackBrush, pos * ArrSize + pos, 0, ArrSize, MaxVal);
                Thread.Sleep(18);
                g.FillRectangle(WhiteBrush, pos * ArrSize + pos, MaxVal - height, ArrSize, MaxVal);
            }
            else if (pos == 1)
            {
                Thread.Sleep(18);
                g.FillRectangle(BlackBrush, pos + ArrSize, 0, ArrSize, MaxVal);
                Thread.Sleep(18);
                g.FillRectangle(WhiteBrush, pos + ArrSize, MaxVal - height, ArrSize, MaxVal);
            }
            else
            {
                Thread.Sleep(18);
                g.FillRectangle(BlackBrush, pos, 0, ArrSize, MaxVal);
                Thread.Sleep(18);
                g.FillRectangle(WhiteBrush, pos, MaxVal - height, ArrSize, MaxVal);
            }
        }

        public void ReDraw()
        {
            /*
             * Display new value into the panel
             */
            for (int i = 0; i < panelArr.Count(); i++)
            {
                g.FillRectangle(WhiteBrush, i, MaxVal - panelArr[i], 1, MaxVal);
            }
        }
    }
}
