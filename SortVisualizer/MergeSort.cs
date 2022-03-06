using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SortVisualizer
{
    class MergeSort : ISortVisualizer
    {

        private int[] panelArr;
        private Graphics g;
        private int ArrSize = 0;
        private int MaxVal;
        Brush BlackBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
        Brush WhiteBrush = new System.Drawing.SolidBrush(System.Drawing.Color.White);
        public MergeSort(int[] panelArray, Graphics graph, int MaxValue, int ArraySize)
        {
            panelArr = panelArray;
            g = graph;
            MaxVal = MaxValue;
            ArrSize = ArraySize;
        }
        /*
         * Sort Algorithm: Merge Sort
         */
        public void NextSort()
        {
            int size = panelArr.Count();
            mergeSort(panelArr,0,size-1);
        }

        private void mergeSort(int[] _panelArr, int q, int r)
        {
            if (q < r)
            {
                int midpoint = (q + r) / 2;
                mergeSort(_panelArr, q, midpoint);
                mergeSort(_panelArr, midpoint + 1, r);
                merge(_panelArr, q, midpoint, r);
            }
        }

        private void merge(int[] _panelArr, int p, int q, int r)
        {
            int n1 = q-p+1;
            int n2 = r- q;
            int[] L = new int[n1];
            int[] R = new int[n2]; //left and right list

            for (int d = 0; d < n1; d++)
                L[d] = _panelArr[p + d];
            for (int c = 0; c < n2; c++)
                R[c] = _panelArr[q + 1 + c];

            // Maintain current index of sub-arrays and main array
            int i, j, k;
            i = 0;
            j = 0;
            k = p;

            /*
             Until reach end of either L or R array. Pich larger elements
            among L and R and place them in correct pos in panelArr
             */
            while (i<n1 && j<n2)
            {
                if (L[i] <= R[j])
                {
                    _panelArr[k] = L[i];
                    DrawNewValue(k, _panelArr[k]);
                    i++;
                }
                else
                {
                    _panelArr[k] = R[j];
                    DrawNewValue(k, _panelArr[k]);
                    j++;
                }
                k++;
            }

            /*
             When we are run out of element in L or R array. pick up 
            remainding elemetns and put in _panelArr[p...r]
             */
            while (i < n1)
            {
                _panelArr[k] = L[i];
                DrawNewValue(k, _panelArr[k]);
                i++;
                k++;
            }
            while (j < n2)
            {
                _panelArr[k] = R[j];
                DrawNewValue(k, _panelArr[k]);
                j++;
                k++;
            }
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
