/*********************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPU_SCHEDULERS
{
    class process
    {
        public int process_id;
        public int arrival_time;
        public int burset_time;
        public int needed_time;
        public int start_time;
        public bool take_cpu;
        public bool enter_before;
        public bool finish;
        public process(int s, int at, int bt)
        {
            process_id = s;
            arrival_time = at;
            burset_time = bt;
            needed_time = bt;
            start_time = 0;
            take_cpu = false;
            enter_before = false;
            finish = false;
        }
        public process()
        {
            process_id = 0;
            arrival_time = 0;
            burset_time = 0;
            needed_time = 0;
            start_time = 0;
            take_cpu = false;
            finish = false;
        }
    };

    class SJF
    {
        static void sort_by_burset(List<process> arr)//el func
        {

            process temp;
            for (int p = 0; p < arr.Count; p++)
            {
                for (int j = 0; j < arr.Count - 1; j++)
                {
                    if (arr[j].burset_time > arr[j + 1].burset_time)
                    {
                        temp = arr[j + 1];
                        arr[j + 1] = arr[j];
                        arr[j] = temp;
                    }
                }


            }

        }
        static void sortby_arrival_time(List<process> arr)
        {
            process temp;
            for (int p = 0; p < arr.Count; p++)
            {
                for (int j = 0; j < arr.Count - 1; j++)
                {
                    if (arr[j].arrival_time > arr[j + 1].arrival_time)
                    {
                        temp = arr[j + 1];
                        arr[j + 1] = arr[j];
                        arr[j] = temp;
                    }
                }


            }


        }

        public static void make_list(List<process> pr, int size, int[] at, int[] bt, int[] st, int[] ps)
        {


            for (int i = 0; i < size; i++)
            {
                pr.Add(new process(ps[i], at[i], bt[i]));

            }

        }
        public static void swab(List<process> p, int ind1, int ind2)
        {
            process temp;
            temp = p[ind1];
            p[ind1] = p[ind2];
            p[ind2] = temp;
        }
        static int min_arrival(List<process> t, int n)
        {
            int min = t[0].arrival_time;
            int index = 0;
            for (int k = 0; k < n; k++)
            {
                if (t[k].arrival_time < min)
                {
                    min = t[k].arrival_time;
                    index = k;
                }

            }
            return index;
        }
        public static void swab(process[] p, int ind1, int ind2)
        {
            process temp;
            temp = p[ind1];
            p[ind1] = p[ind2];
            p[ind2] = temp;
        }
        public static int smallest_bt_ind(List<process> t, int counter, int finish_id)
        {
            int min = t[0].burset_time;
            int index = 0;
            for (int k = 0; k < t.Count; k++)
            {
                if (t[k].burset_time < min && t[k].arrival_time < counter && t[k].process_id != finish_id)
                {
                    min = t[k].burset_time;
                    index = k;
                }

            }
            return index;
        }
        public static int Last_arrival_index(List<process> t, int counter)
        {
            int last_arrival_index = 0;
            for (int h = 0; h < t.Count; h++)
            {
                if (t[h].arrival_time <= counter)
                {

                    last_arrival_index = h;//d el index ely hya w kol ely ablha wslo
                }
            }
            return last_arrival_index;
        }
        public static bool check_all_before(List<process> t)
        {
            bool all_before_finish = true;
            for (int h = 0; h < t.Count; h++)
            {
                if (t[h].enter_before == true)
                {
                    if (t[h].needed_time != 0)
                    { all_before_finish = false; }//d el index ely hya w kol ely ablha wslo
                }
            }
            return all_before_finish;


        }
        public static void sjf_nonprmptive(IList<int> start_time_arr, IList<int> id_arr, int size, int[] at, int[] bt, int[] ps)
        {
            List<process> t = new List<process>();
            for (int i = 0; i < size; i++)
            {
                t.Add(new process(ps[i], at[i], bt[i]));

            }
            sort_by_burset(t);
            int min_index = min_arrival(t, size);
            process[] new_arr;
            new_arr = new process[size];
            if (t[min_index].arrival_time > 0) { id_arr.Add(0); start_time_arr.Add(0); }
            new_arr[0] = t[min_index];//awl mkan f el array el gdid mazbot
            new_arr[0].start_time = t[min_index].arrival_time;
            t[min_index].take_cpu = true;
            start_time_arr.Add(t[min_index].arrival_time);//hna 3rft enha 5lass et7atet f el cpu 
            id_arr.Add(t[min_index].process_id);
            int new_arr_index = 1;//dah el sagl feh mkan el int ely 3leh el door 34an yd5ol
            int current_time = t[min_index].arrival_time;
            int next_compare_time = current_time + t[min_index].burset_time;
            t.RemoveAt(min_index);
            while (new_arr_index < size && t.Count != 0)
            {
                for (int k = 0; k < t.Count; k++)
                {
                    if (t[k].arrival_time <= next_compare_time)
                    {
                        new_arr[new_arr_index] = t[k];
                        t[k].take_cpu = true;
                        current_time = next_compare_time;
                        start_time_arr.Add(current_time);//hna 3rft enha 5lass et7atet f el cpu 
                        new_arr[new_arr_index].start_time = current_time;
                        id_arr.Add(t[k].process_id);
                        next_compare_time = current_time + t[k].burset_time;
                        t.RemoveAt(k);
                        new_arr_index++;
                        k = -1;

                    }
                }
                for (int k = 0; k < t.Count; k++)
                {
                    if (t[k].arrival_time > next_compare_time)
                    {
                        id_arr.Add(0);
                        current_time = new_arr[new_arr_index - 1].start_time + new_arr[new_arr_index - 1].burset_time;
                        start_time_arr.Add(current_time);
                        min_index = min_arrival(t, t.Count);
                        new_arr[new_arr_index] = t[min_index];

                        t[min_index].take_cpu = true;
                        current_time = t[min_index].arrival_time;
                        new_arr[new_arr_index].start_time = current_time;
                        //hna 3rft enha 5lass et7atet f el cpu 
                        id_arr.Add(t[min_index].process_id);
                        start_time_arr.Add(current_time);
                        next_compare_time = current_time + t[min_index].burset_time;
                        t.RemoveAt(min_index);
                        new_arr_index++;

                    }



                }


            }
            start_time_arr.Add(next_compare_time);
            for (int h = 0; h < t.Count; h++)
            {
                t[h] = new_arr[h];
            }
        }
        public static void sjf_prmptive(IList<int> start_time_arr, IList<int> id_arr, int size, int[] at, int[] bt, int[] ps)
        {
            List<process> t = new List<process>();
            bool all_before_finish = true;
            for (int i = 0; i < size; i++)
            {
                t.Add(new process(ps[i], at[i], bt[i]));
                // temp.Add(new process(ps[i], at[i], bt[i]));
            }

            sortby_arrival_time(t);
            // orderd_equl_atby_bt(t);
            for (int p = 0; p < t.Count; p++)
            {
                for (int j = 0; j < t.Count - 1; j++)
                {
                    if (t[j].arrival_time == t[j + 1].arrival_time)
                    {
                        if (t[j].burset_time > t[j + 1].burset_time)
                        {
                            swab(t, j, j + 1);

                        }

                    }
                }


            }
            int number_of_finished = 0;

            bool all_arive = false;
            List<process> new_arr = new List<process>();
            if (t[0].arrival_time > 0) { id_arr.Add(0); start_time_arr.Add(0); }

            id_arr.Add(t[0].process_id);
            new_arr.Add(t[0]);//awl mkan f el array el gdid mazbot
            t[0].enter_before = true;
            int new_arr_index = 0;//dah el sagl feh mkan el int bysgl el index bta3 el arr elgdyd
            int this_process = 0;

            t[this_process].take_cpu = true; //hna 3rft enha 5lass et7atet f el cpu 

            t[0].start_time = t[0].arrival_time;
            start_time_arr.Add(t[0].start_time);
            int new_process = 1;
            int next_compare_time = 0;
            int suposed_finish_time = t[0].start_time + t[0].burset_time;//el wa2t el mafrod y5ls 3ando law mafi4 7aga 2at3to
            if (t.Count > 1) next_compare_time = t[new_process].arrival_time;//every comapre_time(new process arrive ||the process in cpu finished  ) compare(needed_time) for all process they already arrive and not finish  
            //dah zay el time byzeed kol clock 

            int counter = 0, last_arrival_index = 0;
            //dool lel while
            bool still_here = false;
            int last_process = 0;
            while (number_of_finished != t.Capacity || t.Count != 0)
            {
                if (all_arive != true)
                {
                    last_arrival_index = Last_arrival_index(t, counter);//d el index ely hya w kol ely ablha wslo     
                }
                if (last_arrival_index == t.Count - 1) { all_arive = true; }


                if (counter == suposed_finish_time)
                {
                    t[this_process].finish = true;
                    t[this_process].take_cpu = false;
                    t[this_process].needed_time = 0;
                    new_arr[new_arr_index].finish = true;
                    new_arr[new_arr_index].take_cpu = false;
                    new_arr[new_arr_index].needed_time = 0;
                    number_of_finished++;
                }

                all_before_finish = check_all_before(t); //d el index ely hya w kol ely ablha wslo


                if (all_before_finish == false && counter < next_compare_time && t[this_process].take_cpu == false && t[this_process].finish == true && all_arive == false)
                {

                    this_process = smallest_bt_ind(t, counter, t[this_process].process_id);
                    new_arr.Add(t[this_process]);
                    t[this_process].enter_before = true;
                    id_arr.Add(t[this_process].process_id);
                    new_arr_index++;
                    new_arr[new_arr_index].take_cpu = true;
                    t[this_process].take_cpu = true;
                    t[this_process].start_time = counter;
                    new_arr[new_arr_index].take_cpu = true;

                    new_arr[new_arr_index].start_time = counter;
                    start_time_arr.Add(new_arr[new_arr_index].start_time);
                    suposed_finish_time = counter + new_arr[new_arr_index].needed_time;
                }

                if (t[this_process].take_cpu == false && t[this_process].finish == true && all_arive == false && counter < next_compare_time && all_before_finish == true)
                {
                    process nothing_in_cpu = new process(0, 0, 0);
                    nothing_in_cpu.start_time = counter;
                    new_arr.Add(nothing_in_cpu);
                    start_time_arr.Add(nothing_in_cpu.start_time);
                    id_arr.Add(nothing_in_cpu.process_id);
                    new_arr_index++;

                }

                if (t[this_process].finish == true)
                {

                    t.RemoveAt(this_process);
                    if (this_process != 0) this_process--;
                    if (t.Count == 0) break;
                    //  next_compare_time = t[this_process].arrival_time;//arival time b3d ma at3mlo swap hyb2a how ely 3leh el door
                }

                if ((counter >= next_compare_time || all_arive) && t.Count != 0)
                {
                    //hna el mafrod nkarn el ba2y bs w n4of meen haymsk
                    if (t[this_process].take_cpu == true)
                    {
                        t[this_process].needed_time = t[this_process].needed_time - (counter - t[this_process].start_time);
                    }
                    if (last_arrival_index >= t.Count) { last_arrival_index--; }
                    for (int w = 0; w <= last_arrival_index; w++)
                    {

                        if (t[this_process].needed_time > t[w].needed_time && t[w].finish == false && t[w].take_cpu == false && t[w].arrival_time <= counter)
                        {
                            t[this_process].take_cpu = false;
                            if (new_process < t.Count && all_arive == false)
                            {
                                new_process++;
                                if (new_process < t.Count) next_compare_time = t[new_process].arrival_time;
                            }
                            if ((w + 1) <= last_arrival_index)
                            {
                                if (t[w + 1].needed_time < t[w].needed_time) { continue; }
                            }
                            last_process = this_process;
                            if (t[this_process].needed_time > 0) still_here = true;
                            this_process = w;
                            new_arr.Add(t[w]);
                            t[w].enter_before = true;
                            id_arr.Add(t[w].process_id);
                            new_arr_index++;
                            new_arr[new_arr_index].take_cpu = true;
                            t[w].take_cpu = true;
                            t[w].start_time = counter;
                            new_arr[new_arr_index].take_cpu = true;

                            new_arr[new_arr_index].start_time = counter;
                            start_time_arr.Add(new_arr[new_arr_index].start_time);
                            suposed_finish_time = counter + new_arr[new_arr_index].needed_time;

                        }
                    }
                    if (t[this_process].take_cpu == false)
                    {// hna law da5l w la2a en mfi4 7aga a7sn mno ta5od el cpu f ya5do howa
                        t[this_process].take_cpu = true;
                        // new_process++;
                        if (new_process < t.Count && all_arive == false) next_compare_time = t[new_process].arrival_time;
                        new_arr.Add(t[this_process]);
                        t[this_process].enter_before = true;
                        id_arr.Add(t[this_process].process_id);
                        new_arr_index++;
                        new_arr[new_arr_index].take_cpu = true;
                        new_arr[new_arr_index].start_time = counter;
                        start_time_arr.Add(new_arr[new_arr_index].start_time);
                        suposed_finish_time = counter + new_arr[new_arr_index].needed_time;


                    }

                }
                counter++;
                all_before_finish = true;
            }

            for (int h = 0; h < new_arr.Count; h++)
            {
                t.Add(new_arr[h]);
            }

            // start_time_arr.Add(new_arr[new_arr_index].start_time+ new_arr[new_arr_index].needed_time);
            start_time_arr.Add(suposed_finish_time);
        }

    }
}
/*********************/
