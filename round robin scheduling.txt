using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPU_SCHEDULERS
{

    class ROUND_ROBIN
    {
        static void Swap(ref int x, ref int y)
        {
            int tmp = x;
            x = y;
            y = tmp;
        }
        public static void averagetime(int size, int[] at, int[] bt, int[] ps, int quantum, ref float avg_wt, ref float avg_tat, ref IList<int> final_ps, ref IList<int> final_st)
        {

            //int[] sorted = new int[size];
            int[] rem_bt = new int[size];
            int[] wt = new int[size];
            int[] tat = new int[size];
            final_ps = new List<int>();
            final_st = new List<int>();
            avg_wt = 0;
            avg_tat = 0;
            int t = 0;

            //int j = 0;
            //////////sorting process according to arrival time///////////
            for (int i = 0; i < size; i++)
            {
                // sorted[i] = ps[i];
                rem_bt[i] = bt[i];
            }
            for (int iter = 0; iter < (size - 1); iter++)
            {
                for (int i = 0; i < (size - 1 - iter); i++)
                {
                    if (at[i] > at[i + 1])
                    {
                        Swap(ref ps[i], ref ps[i + 1]);
                        Swap(ref at[i], ref at[i + 1]);
                        Swap(ref bt[i], ref bt[i + 1]);
                    }
                }
            }

            int counter_finished = 0;
            // if (at[0] != 0) final_st.Add(0);
            //////////////calculating average waiting time///////////
            while (counter_finished != size)
            {
                bool done = true;
            loop: for (int i = 0; i < size; i++)
                {

                    if (t < at[i])
                    {
                        if (t >= at[0])
                        {
                            i = 0;
                            goto loop;
                        }
                        final_st.Add(t);
                        final_ps.Add(0);
                        t++;
                        i = -1;
                        //  final_st.Add(t);
                    }
                    else
                    {
                        // If burst time of a process is greater than 0 then only need to process further
                        if (rem_bt[i] > 0)
                        {
                            // There is a pending process 
                            done = false;
                            if (rem_bt[i] > quantum)
                            {
                                if (final_st.Count != 0)
                                {
                                    if (final_st[final_st.Count - 1] != t)
                                    { final_st.Add(t); }
                                }
                                else { final_st.Add(t); }
                                //final_st.Add(t);
                                t += quantum;
                                final_ps.Add(ps[i]);
                                rem_bt[i] -= quantum;
                                // j++;
                            }
                            else
                            {
                                if (final_st[final_st.Count - 1] != t)
                                { final_st.Add(t); }
                                t = t + rem_bt[i];
                                final_st.Add(t);
                                // Waiting time is current time minus burt time of this process 
                                wt[i] = t - bt[i] - at[i];     // subtract arrival time
                                tat[i] = t - at[i];

                                final_ps.Add(ps[i]);
                                rem_bt[i] = 0;
                                //j++;   
                            }
                        }
                        // If all processes are done 
                        if (done == true)
                        {
                            for (int f = 0; f < size; f++)
                            {
                                avg_wt += wt[f];
                                avg_tat += tat[f];
                            }
                            avg_wt = avg_wt / size;
                            avg_tat = avg_tat / size;
                            //return avg_wt;
                        }
                    }
                }
                counter_finished = 0;
                for (int k = 0; k < size; k++)
                {
                    if (rem_bt[k] == 0)
                    {
                        counter_finished++;
                    }
                }
            }
        }

    }
}
