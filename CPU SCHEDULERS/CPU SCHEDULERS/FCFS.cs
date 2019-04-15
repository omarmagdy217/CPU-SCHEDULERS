using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPU_SCHEDULERS
{
    class FCFS
    {
        public static void sortArrivaltime(int[] ps, int size, int[] at, int[] bt)
        {
            int temparri;
            int tempbru;
            int temppro;

            for (int i = 0; i < size; i++)
            {
                for (int j = i + 1; j < size; j++)
                {
                    if (at[j] < at[i])
                    {// swapping arrival
                        temparri = at[i];
                        at[i] = at[j];
                        at[j] = temparri;
                        //swappin brust
                        tempbru = bt[i];
                        bt[i] = bt[j];
                        bt[j] = tempbru;
                        //swapping process
                        temppro = ps[i];
                        ps[i] = ps[j];
                        ps[j] = temppro;
                    }
                }
            }

        }

        public static void waitingTime(int[] ps, int size, int[] at, int[] bt, int[] wt)
        {    // process "array of processes ,size "no of processes, at" arrival time array", bt "brusts array"
            // wt "waiting time array"

            sortArrivaltime(ps, size, at, bt);
            int[] sumprevBrusts = new int[size];
            sumprevBrusts[0] = 0;
            for (int i = 1; i < size; i++)
            {
                sumprevBrusts[i] = sumprevBrusts[i - 1] + bt[i - 1]; //total of prev brusts
                wt[i] = sumprevBrusts[i] - at[i];
                if (wt[i] < 0)
                {
                    wt[i] = 0;
                }
            }

        }

        public static void turnaround(int[] ps, int size, int[] at, int[] bt, int[] wt, int[] tat)
        {
            waitingTime(ps, size, at, bt, wt);
            for (int i = 0; i < size; i++)
            {
                tat[i] = bt[i] + wt[i];
            }
        }

        public static void averageTime(int[] ps, int size, int[] bt, int[] at, ref float a, ref float b)
        {   //turnaround
            int[] wt = new int[size];
            int[] tat = new int[size];
            int sumTurnaround = 0;
            turnaround(ps, size, at, bt, wt, tat);
            //waiting time
            int sumWaitingTime = 0;
            waitingTime(ps, size, at, bt, wt);

            for (int i = 0; i < size; i++)
            {
                sumTurnaround += tat[i];
                sumWaitingTime += wt[i];
            }
            a = (float)sumWaitingTime / (float)size;
            b = (float)sumTurnaround / (float)size;
        }

        public static void GanttView(int size, int[] at, int[] bt, int[] ps, IList<int> stp, IList<int> psp)
        {
            for (int i = 0; i <= size; i++)
            {
                if (i == 0)
                {
                    if (at[0] > 0)
                    {
                        stp.Add(0);
                        psp.Add(0);
                    }
                    stp.Add(at[0]);
                    psp.Add(ps[0]);
                }

                else if (i == size)
                {
                    stp.Add(stp.Last() + bt[i - 1]);
                }
                else if ((stp.Last() + bt[i - 1]) < at[i])
                {
                    stp.Add(stp.Last() + bt[i - 1]);
                    psp.Add(0);
                    stp.Add(at[i]);
                    psp.Add(ps[i]);
                }
                else
                {
                    stp.Add(stp.Last() + bt[i - 1]);
                    psp.Add(ps[i]);
                }
            }
        }

    }
}

