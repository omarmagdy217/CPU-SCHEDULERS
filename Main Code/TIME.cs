using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPU_SCHEDULERS
{
    class TIME
    {
        public static float avgWaiting(int size, int[] at, int[] bt, IList<int> stp, IList<int> psp)
        {
            float wt_sum = 0;
            int[] wt = new int[size];
            for (int j = 0; j < psp.Count; j++)
            {
                if (psp[j] != 0)
                    wt[psp[j] - 1] = stp[j + 1] - at[psp[j] - 1] - bt[psp[j] - 1];
            }
            for (int k = 0; k < size; k++)
            {
                wt_sum += wt[k];
            }
            wt_sum /= (float)size;
            return wt_sum;
        }

        public static float avgTurnAround(int size, int[] at, IList<int> stp, IList<int> psp)
        {
            float tat_sum = 0;
            int[] tat = new int[size];
            for (int j = 0; j < psp.Count; j++)
            {
                if (psp[j] != 0)
                    tat[psp[j] - 1] = stp[j + 1] - at[psp[j] - 1];
            }
            for (int k = 0; k < size; k++)
            {
                tat_sum += tat[k];
            }
            tat_sum /= (float)size;
            return tat_sum;
        }
    }
}
