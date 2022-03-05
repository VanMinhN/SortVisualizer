using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SortVisualizer
{
    class BubbleSort : ISortVisualizer
    {
        private int[] panelArr;
        private Graphics g;
        private int ArrSize = 0;
        private int MaxVal;
        Brush BlackBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
        Brush WhiteBrush = new System.Drawing.SolidBrush(System.Drawing.Color.White);
        public BubbleSort(int[] panelArray, Graphics graph, int MaxValue, int ArraySize)
        {
            panelArr = panelArray;
            g = graph;
            MaxVal = MaxValue;
            ArrSize = ArraySize;
        }
        /*
         * Sort Algorithm: Bubble Sort
         */
        public void NextSort()
        {
            for (int i=0;i<panelArr.Count()-1;i++)
            {
                if (panelArr[i] > panelArr[i+1])
                {
                    Swap(i,i+1);     
                }
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
            for (int i = 0;i<panelArr.Count()-1;i++)
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
                g.FillRectangle(BlackBrush, pos * ArrSize + pos, 0, ArrSize, MaxVal);
                g.FillRectangle(WhiteBrush, pos * ArrSize + pos, MaxVal - height, ArrSize, MaxVal);
            }
            else if (pos == 1)
            {
                g.FillRectangle(BlackBrush, pos + ArrSize, 0, ArrSize, MaxVal);
                g.FillRectangle(WhiteBrush, pos + ArrSize, MaxVal - height, ArrSize, MaxVal);
            }
            else
            {
                g.FillRectangle(BlackBrush, pos, 0, ArrSize, MaxVal);
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
