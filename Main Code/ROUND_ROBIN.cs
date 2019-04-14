using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPU_SCHEDULERS
{
    class ROUND_ROBIN
    {
        struct Process
        {
            public int pid;  // Process ID 
            public int at;   // arrival time
            public int bt;   // CPU Burst time required 
        };

        public static void RobinSort(int Qtm, int size, int[] at, int[] bt, int[] ps, IList<int> stp, IList<int> psp)
        {
            int t_sum = 0, i;
            IList<Process> pr = new List<Process>();
            IList<int> wt = new List<int>();
            Process temp;
            for (i = 0; i < size; i++)
            {
                temp.pid = ps[i];
                temp.at = at[i];
                temp.bt = bt[i];
                pr.Add(temp);
                wt.Add(0);
            }
            pr = pr.OrderBy(x => x.at).ToList();
            if (pr[0].at > 0)
            {
                stp.Add(0);
                psp.Add(0);
                t_sum = pr[0].at;
            }
            while (pr.Count > 0)
            {
                for (i = 0; i < pr.Count; i++)
                {
                    if(pr[i].at > t_sum)
                    {
                        if (wt.Contains(1))
                            break;
                        stp.Add(t_sum);
                        psp.Add(0);
                        stp.Add(pr[i].at);
                        psp.Add(pr[i].pid);
                        t_sum = pr[i].at;
                    }
                    else
                    {
                        stp.Add(t_sum);
                        psp.Add(pr[i].pid);
                    }
                    if (pr[i].bt < Qtm)
                        t_sum += pr[i].bt;
                    else
                        t_sum += Qtm;
                    temp = pr[i];
                    temp.bt -= Qtm;
                    if (temp.bt > 0)
                    {
                        pr[i] = temp;
                        wt[i] = 1;
                    }
                    else
                    {
                        pr.RemoveAt(i);
                        wt.RemoveAt(i);
                        i--;
                    }
                }
            }
            stp.Add(t_sum);
        }
    }
}
