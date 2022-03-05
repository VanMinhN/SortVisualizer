using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortVisualizer
{
    class SelectionSort : ISortVisualizer
    {
        private int[] panelArr;
        private Graphics g;
        private int MaxVal;
        Brush BlackBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
        Brush WhiteBrush = new System.Drawing.SolidBrush(System.Drawing.Color.White);
        public SelectionSort(int[] panelArray, Graphics graph, int MaxValue)
        {
            panelArr = panelArray;
            g = graph;
            MaxVal = MaxValue;
        }

        /*
         * Sort Algorithm: Selection Sort
         */
        public void NextSort()
        {
            int size = panelArr.Count();
            for (int i =0; i<size-1; i++)
            {
                int min_index = i;
                for (int j = i+1;j < size;j++)
                {
                    if (panelArr[j] < panelArr[min_index])
                    {
                        min_index = i;
                    }
                }
                Swap(i, min_index);
            }
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
