using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPU_SCHEDULERS
{
    class PRIORITY
    {
        struct Process
        {
            public int pid;  // Process ID 
            public int at;   // arrival time
            public int bt;   // CPU Burst time required 
            public int priority; // Priority of this process 
        };

        public static void prioritySort(int size, int[] at, int[] bt, int[] prio, int[] st, int[] ps)
        {
            int t_sum = at[0], min_at, min_index, i, j = 0;
            IList<Process> pr = new List<Process>();
            Process temp;
            for (i = 0; i < size; i++)
            {
                temp.pid = ps[i];
                temp.at = at[i];
                temp.bt = bt[i];
                temp.priority = prio[i];
                pr.Add(temp);
            }
            for (int iter = 0; iter < (size - 1); iter++)
            {
                for (i = 0; i < (size - 1-iter); i++)
                {
                    if (pr[i].priority > pr[i+1].priority)
                    {
                        temp = pr[i];
                        pr[i] = pr[i+1];
                        pr[i+1] = temp;
                    }
                }
            }
            while (pr.Count > 0)
            {
                min_at = pr[0].at;
                min_index = 0;
                for (i = 0; i < pr.Count; i++)
                {
                    if (pr[i].at <= t_sum)
                        break;
                    else if (pr[i].at < min_at)
                    {
                        min_at = pr[i].at;
                        min_index = i;
                    }
                }
                if (i == pr.Count)
                {
                    st[j] = pr[min_index].at;
                    ps[j] = pr[min_index].pid;
                    at[j] = pr[min_index].at;
                    t_sum = pr[min_index].at + pr[min_index].bt;
                    pr.RemoveAt(min_index);
                    j++;
                }
                else
                {
                    st[j] = t_sum;
                    ps[j] = pr[i].pid;
                    at[j] = pr[i].at;
                    t_sum += pr[i].bt;
                    pr.RemoveAt(i);
                    j++;
                }
            }
            st[j] = t_sum;
        }
        public static float avgWaiting(int size, int[] at, int[] st)
        {
            float wt = 0;
            for (int j = 0; j < size; j++)
            {
                wt += st[j] - at[j];
            }
            wt /= (float)size;
            return wt;
        }
        public static float avgTurnAround(int size, int[] at, int[] st)
        {
            float tat = 0;
            for (int k = 0; k < size; k++)
            {
                tat += st[k+1] - at[k];
            }
            tat /= (float)size;
            return tat;
        }
    }
}
