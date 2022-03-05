using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortVisualizer
{
    class HeapSort : ISortVisualizer
    {
        private int[] panelArr;
        private int ArrSize = 0;
        private Graphics g;
        private int MaxVal;
        Brush BlackBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
        Brush WhiteBrush = new System.Drawing.SolidBrush(System.Drawing.Color.White);
        public HeapSort(int[] panelArray, Graphics graph, int MaxValue, int ArraySize)
        {
            panelArr = panelArray;
            g = graph;
            MaxVal = MaxValue;
            ArrSize = ArraySize;
        }

        /*
         * Sort Algorithm: Heap Sort
         */
        public void NextSort()
        {
            int size = panelArr.Length;
            heapSort(panelArr, size);
        }

        private void heapSort(int[] _panelArr, int n)
        {
            for (int i = (n/2)-1; i >= 0; i--)
            {
                heapify(_panelArr, n, i);
            }
            for (int i=n-1;i>=0;i--)
            {
                Swap(0,i);
                heapify(_panelArr, i, 0);
            }
        }

        private void heapify(int[] _panelArr, int n, int i)
        {
            int largest = i;
            int left = 2 * i + 1; //left node
            int right = 2 * i + 2; //right node
            if (left < n && _panelArr[left] > _panelArr[largest])
            {
                largest = left;
            }
            if (right < n && _panelArr[right] > _panelArr[largest])
            {
                largest = right;
            }
            if (largest!=i)
            {
                Swap(i, largest);
                heapify(_panelArr, n, largest);
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
            if (pos>=2)
            {
                g.FillRectangle(BlackBrush, pos * ArrSize + pos, 0, ArrSize, MaxVal);
                g.FillRectangle(WhiteBrush, pos * ArrSize + pos, MaxVal - height, ArrSize, MaxVal);
            }
            else if (pos == 1)
            {
                g.FillRectangle(BlackBrush, pos + ArrSize, 0, ArrSize, MaxVal);
                g.FillRectangle(WhiteBrush, pos + ArrSize, MaxVal - height, ArrSize, MaxVal);
            }
            else {
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
