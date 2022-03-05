using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortVisualizer
{
    class QuickSort : ISortVisualizer
    {
        private int[] panelArr;
        private Graphics g;
        private int MaxVal;
        Brush BlackBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
        Brush WhiteBrush = new System.Drawing.SolidBrush(System.Drawing.Color.White);
        public QuickSort(int[] panelArray, Graphics graph, int MaxValue)
        {
            panelArr = panelArray;
            g = graph;
            MaxVal = MaxValue;
        }

        /*
         * Sort Algorithm: QuickSort Sort
         */
        public void NextSort()
        {
            int size = panelArr.Length;
            quickSort(panelArr, 0, size-1);
            
        }

        private void quickSort(int[] panelArr_IN, int low, int high)
        {
            if (low < high)
            {
                // find pivot element such that
                // elements smaller than pivot are on the left
                // elements greater than pivot are on the right
                int pivot = partition(panelArr_IN, low, high);
                quickSort(panelArr_IN, low, pivot - 1);
                quickSort(panelArr_IN, pivot + 1, high);
            }
        }

        private int partition(int[] panelArr_IN, int low, int high)
        {
            int pivot = panelArr_IN[high];
            int lowIndex = (low - 1);
            for (int j = low; j<high; j++)
            {
                if (panelArr_IN[j] < pivot)
                {
                    lowIndex++;
                    Swap(lowIndex,j);
                }
            }
            Swap(lowIndex+1,high);
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
            g.FillRectangle(BlackBrush, pos, 0, 1, MaxVal);
            g.FillRectangle(WhiteBrush, pos, MaxVal - height, 1, MaxVal);
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
