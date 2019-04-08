using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPU_SCHEDULERS
{
    class FCFS
    {
        public static void waitingTime(int size, int[] at, int[] bt, int[] wt)
        {    // process "array of processes ,size "no of processes, at" arrival time array", bt "brusts array"
            // wt "waiting time array"

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

        public static void turnaround(int size, int[] at, int[] bt, int[] wt, int[] tat)
        {
            waitingTime(size, at, bt, wt);
            for (int i = 0; i < size; i++)
            {
                tat[i] = bt[i] + wt[i];
            }
        }

        public static void averageTime(int size, int[] bt, int[] at, ref float a, ref float b)
        {   //turnaround
            int[] wt = new int[size];
            int[] tat = new int[size];
            int sumTurnaround = 0;
            turnaround(size, at, bt, wt, tat);
            //waiting time
            int sumWaitingTime = 0;
            waitingTime(size, at, bt, wt);

            for (int i = 0; i < size; i++)
            {
                sumTurnaround += tat[i];
                sumWaitingTime += wt[i];
            }
            a = (float)sumWaitingTime / (float)size;
            b = (float)sumTurnaround / (float)size;
        }

        public static void GanttView(int size, int[] at, int[] bt, int[] st)
        {
            int[] wt = new int[size];
            waitingTime(size, at, bt, wt);
            for (int i = 0; i <= size; i++)
            {
                if (i == size)
                    st[i] = wt[i - 1] + at[i - 1] + bt[i - 1];
                else
                {
                    st[i] = wt[i] + at[i];
                }
            }
        }
    }
}
