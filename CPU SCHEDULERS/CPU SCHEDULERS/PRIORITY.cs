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

        /*private static void sortList(IList<Process> pr)
        {
            Process temp;
            for (int iter = 0; iter < (pr.Count - 1); iter++)
            {
                for (int i = 0; i < (pr.Count - 1 - iter); i++)
                {
                    if (pr[i].priority > pr[i + 1].priority)
                    {
                        temp = pr[i];
                        pr[i] = pr[i + 1];
                        pr[i + 1] = temp;
                    }
                }
            }
        }*/

        public static void PrioritySort(bool prm, int size, int[] at, int[] bt, int[] prio, int[] ps, IList<int> stp, IList<int> psp)
        {
            int t_sum = at[0], min_at, min_index, prev_index, i, j = 0;
            IList<Process> pr = new List<Process>();
            IList<int> atp = new List<int>();
            Process temp;
            for (i = 0; i < size; i++)
            {
                temp.pid = ps[i];
                temp.at = at[i];
                temp.bt = bt[i];
                temp.priority = prio[i];
                pr.Add(temp);
                if (at[i] < t_sum)
                    t_sum = at[i];
            }
            //sortList(pr);
            pr = pr.OrderBy(x => x.priority).ToList();
            if (t_sum > 0)
            {
                stp.Add(0);
                psp.Add(0);
            }
            while (pr.Count > 0)
            {
                min_at = pr[0].at;
                min_index = 0;
                prev_index = 0;
                for (i = 0; i < pr.Count; i++)
                {
                    if (pr[i].at <= t_sum)
                        break;
                    else if (pr[i].at < min_at)
                    {
                        min_at = pr[i].at;
                        prev_index = min_index;
                        min_index = i;
                    }
                }
                if (i == pr.Count)
                {
                    stp.Add(t_sum);
                    psp.Add(0);
                    stp.Add(pr[min_index].at);
                    psp.Add(pr[min_index].pid);
                    t_sum = pr[min_index].at + pr[min_index].bt;
                    if (prm)
                    {
                        atp.Add(pr[min_index].at);
                        if (pr[prev_index].at < t_sum && min_index != prev_index)
                        {
                            temp = pr[min_index];
                            temp.bt = t_sum - pr[prev_index].at;
                            //temp.at = pr[prev_index].at + pr[prev_index].bt;
                            pr[min_index] = temp;
                            t_sum = pr[prev_index].at;
                        }
                        else
                            pr.RemoveAt(min_index);
                    }
                    else
                    {
                        at[j] = pr[min_index].at;
                        pr.RemoveAt(min_index);
                        j++;
                    }
                }
                else
                {
                    stp.Add(t_sum);
                    psp.Add(pr[i].pid);
                    t_sum += pr[i].bt;
                    if(prm)
                    {
                        atp.Add(pr[i].at);
                        if (pr[min_index].at < t_sum && i != min_index)
                        {
                            temp = pr[i];
                            temp.bt = t_sum - pr[min_index].at;
                            //temp.at = pr[min_index].at + pr[min_index].bt;
                            pr[i] = temp;
                            t_sum = pr[min_index].at;
                        }
                        else
                            pr.RemoveAt(i);
                    }
                    else
                    {
                        at[j] = pr[i].at;
                        pr.RemoveAt(i);
                        j++;
                    }
                }
            }
            stp.Add(t_sum);
        }
    }
}
