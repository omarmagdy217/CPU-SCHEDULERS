using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPU_SCHEDULERS
{
    class PRIORITY
    {
        public static void prioritySort(int size, int[] at, int[] bt, int[] prio, int[] st, int[] ps)
        {
            int temp,t_sum=0;
            for (int iter = 0; iter < (size - 1); iter++)
            {
                for (int i = 0; i < (size - 1-iter); i++)
                {
                    if (at[i] == at[i + 1])
                    {
                        if (prio[i] > prio[i + 1])
                        {
                            temp = ps[i];
                            ps[i] = ps[i + 1];
                            ps[i + 1] = temp;
                            temp = at[i];
                            at[i] = at[i + 1];
                            at[i + 1] = temp;
                            temp = bt[i];
                            bt[i] = bt[i + 1];
                            bt[i + 1] = temp;
                            temp = prio[i];
                            prio[i] = prio[i + 1];
                            prio[i + 1] = temp;
                        }
                    }
                }
            }
            for (int i = 0; i <= size; i++)
            {
                if (i == 0)
                {
                    t_sum += at[i];
                    st[i] = t_sum;
                }
                else
                {
                    t_sum += bt[i - 1];
                    if (i != size && t_sum < at[i])
                        st[i] = at[i];
                    else
                        st[i] = t_sum;
                }
            }
        }
        public static float avgWaiting(int size, int[] at, int[] st)
        {
            float wt = 0;
            for (int i = 0; i < size; i++)
            {
                wt += st[i] - at[i];
            }
            wt /= (float)size;
            return wt;
        }
        public static float avgTurnAround(int size, int[] at, int[] st)
        {
            float tat = 0;
            for (int i = 0; i < size; i++)
            {
                tat += st[i+1] - at[i];
            }
            tat /= (float)size;
            return tat;
        }
    }
}
