using System;
using System.Collections.Generic;
using System.Linq;

namespace CpuScheduler
{
    public static class BatchAlgorithms
    {
        public static List<SchedulingResult> ComputeFCFS(int[] arrivalTimes, int[] burstTimes)
        {
            int n = arrivalTimes.Length;
            var indices = Enumerable.Range(0, n).OrderBy(i => arrivalTimes[i]).ToList();
            var results = new List<SchedulingResult>();
            int currentTime = 0;
            foreach (var idx in indices)
            {
                if (currentTime < arrivalTimes[idx]) currentTime = arrivalTimes[idx];
                int start = currentTime;
                int finish = start + burstTimes[idx];
                int turnaround = finish - arrivalTimes[idx];
                int waiting = start - arrivalTimes[idx];
                results.Add(new SchedulingResult
                {
                    ProcessID = "P" + (idx + 1),
                    ArrivalTime = arrivalTimes[idx],
                    BurstTime = burstTimes[idx],
                    StartTime = start,
                    FinishTime = finish,
                    WaitingTime = waiting,
                    TurnaroundTime = turnaround,
                    ResponseTime = start - arrivalTimes[idx]
                });
                currentTime = finish;
            }
            return results;
        }

        public static List<SchedulingResult> ComputeSJF(int[] arrivalTimes, int[] burstTimes)
        {
            int n = arrivalTimes.Length;
            var remaining = new bool[n];
            for (int i = 0; i < n; i++) remaining[i] = false;
            var results = new List<SchedulingResult>();
            int completed = 0;
            int currentTime = 0;
            var done = new bool[n];

            while (completed < n)
            {
                // find ready with smallest burst
                int sel = -1;
                int minBurst = int.MaxValue;
                for (int i = 0; i < n; i++)
                {
                    if (!done[i] && arrivalTimes[i] <= currentTime)
                    {
                        if (burstTimes[i] < minBurst)
                        {
                            minBurst = burstTimes[i];
                            sel = i;
                        }
                    }
                }
                if (sel == -1)
                {
                    currentTime++;
                    continue;
                }
                int start = currentTime;
                int finish = start + burstTimes[sel];
                int turnaround = finish - arrivalTimes[sel];
                int waiting = start - arrivalTimes[sel];
                results.Add(new SchedulingResult
                {
                    ProcessID = "P" + (sel + 1),
                    ArrivalTime = arrivalTimes[sel],
                    BurstTime = burstTimes[sel],
                    StartTime = start,
                    FinishTime = finish,
                    WaitingTime = waiting,
                    TurnaroundTime = turnaround,
                    ResponseTime = start - arrivalTimes[sel]
                });
                currentTime = finish;
                done[sel] = true;
                completed++;
            }
            return results;
        }

        public static List<SchedulingResult> ComputePriority(int[] arrivalTimes, int[] burstTimes, int[] priorities)
        {
            int n = arrivalTimes.Length;
            var results = new List<SchedulingResult>();
            var done = new bool[n];
            int completed = 0;
            int currentTime = 0;

            while (completed < n)
            {
                int sel = -1;
                int bestPriority = int.MinValue; // higher number = higher priority
                for (int i = 0; i < n; i++)
                {
                    if (!done[i] && arrivalTimes[i] <= currentTime)
                    {
                        if (priorities[i] > bestPriority)
                        {
                            bestPriority = priorities[i];
                            sel = i;
                        }
                    }
                }
                if (sel == -1)
                {
                    currentTime++;
                    continue;
                }
                int start = currentTime;
                int finish = start + burstTimes[sel];
                int turnaround = finish - arrivalTimes[sel];
                int waiting = start - arrivalTimes[sel];
                results.Add(new SchedulingResult
                {
                    ProcessID = "P" + (sel + 1),
                    ArrivalTime = arrivalTimes[sel],
                    BurstTime = burstTimes[sel],
                    StartTime = start,
                    FinishTime = finish,
                    WaitingTime = waiting,
                    TurnaroundTime = turnaround,
                    ResponseTime = start - arrivalTimes[sel]
                });
                currentTime = finish;
                done[sel] = true;
                completed++;
            }
            return results;
        }

        public static List<SchedulingResult> ComputeRoundRobin(int[] arrivalTimes, int[] burstTimes, int quantum = 4)
        {
            int n = arrivalTimes.Length;
            var remaining = new int[n];
            for (int i = 0; i < n; i++) remaining[i] = burstTimes[i];
            var startTimes = Enumerable.Repeat(-1, n).ToArray();
            var finishTimes = Enumerable.Repeat(0, n).ToArray();
            int currentTime = 0;
            var queue = new Queue<int>();
            var added = new bool[n];
            int completed = 0;

            // enqueue initial arrivals at time 0
            while (completed < n)
            {
                // add newly arrived
                for (int i = 0; i < n; i++)
                {
                    if (!added[i] && arrivalTimes[i] <= currentTime)
                    {
                        queue.Enqueue(i);
                        added[i] = true;
                    }
                }

                if (queue.Count == 0)
                {
                    currentTime++;
                    continue;
                }

                int idx = queue.Dequeue();
                if (startTimes[idx] == -1) startTimes[idx] = currentTime;
                int exec = Math.Min(quantum, remaining[idx]);
                remaining[idx] -= exec;
                currentTime += exec;

                // add newly arrived during execution
                for (int i = 0; i < n; i++)
                {
                    if (!added[i] && arrivalTimes[i] <= currentTime)
                    {
                        queue.Enqueue(i);
                        added[i] = true;
                    }
                }

                if (remaining[idx] > 0)
                {
                    queue.Enqueue(idx);
                }
                else
                {
                    finishTimes[idx] = currentTime;
                    completed++;
                }
            }

            var results = new List<SchedulingResult>();
            for (int i = 0; i < n; i++)
            {
                int start = startTimes[i] == -1 ? arrivalTimes[i] : startTimes[i];
                int finish = finishTimes[i];
                int turnaround = finish - arrivalTimes[i];
                int waiting = turnaround - burstTimes[i];
                results.Add(new SchedulingResult
                {
                    ProcessID = "P" + (i + 1),
                    ArrivalTime = arrivalTimes[i],
                    BurstTime = burstTimes[i],
                    StartTime = start,
                    FinishTime = finish,
                    WaitingTime = waiting,
                    TurnaroundTime = turnaround,
                    ResponseTime = start - arrivalTimes[i]
                });
            }
            return results;
        }

        public static List<SchedulingResult> ComputeSRTF(int[] arrivalTimes, int[] burstTimes)
        {
            int n = arrivalTimes.Length;
            var remaining = new int[n];
            for (int i = 0; i < n; i++) remaining[i] = burstTimes[i];
            var startTimes = Enumerable.Repeat(-1, n).ToArray();
            var finishTimes = Enumerable.Repeat(0, n).ToArray();
            int completed = 0;
            int currentTime = 0;

            while (completed < n)
            {
                int sel = -1;
                int minRem = int.MaxValue;
                for (int i = 0; i < n; i++)
                {
                    if (arrivalTimes[i] <= currentTime && remaining[i] > 0 && remaining[i] < minRem)
                    {
                        minRem = remaining[i];
                        sel = i;
                    }
                }
                if (sel == -1)
                {
                    currentTime++;
                    continue;
                }
                if (startTimes[sel] == -1) startTimes[sel] = currentTime;
                remaining[sel]--;
                currentTime++;
                if (remaining[sel] == 0)
                {
                    finishTimes[sel] = currentTime;
                    completed++;
                }
            }

            var results = new List<SchedulingResult>();
            for (int i = 0; i < n; i++)
            {
                int start = startTimes[i] == -1 ? arrivalTimes[i] : startTimes[i];
                int finish = finishTimes[i];
                int turnaround = finish - arrivalTimes[i];
                int waiting = turnaround - burstTimes[i];
                results.Add(new SchedulingResult
                {
                    ProcessID = "P" + (i + 1),
                    ArrivalTime = arrivalTimes[i],
                    BurstTime = burstTimes[i],
                    StartTime = start,
                    FinishTime = finish,
                    WaitingTime = waiting,
                    TurnaroundTime = turnaround,
                    ResponseTime = start - arrivalTimes[i]
                });
            }
            return results;
        }

        public static List<SchedulingResult> ComputeHRRN(int[] arrivalTimes, int[] burstTimes)
        {
            int n = arrivalTimes.Length;
            var done = new bool[n];
            int completed = 0;
            int currentTime = 0;
            var results = new List<SchedulingResult>();

            while (completed < n)
            {
                int sel = -1;
                double bestRR = -1.0;
                for (int i = 0; i < n; i++)
                {
                    if (!done[i] && arrivalTimes[i] <= currentTime)
                    {
                        double wait = currentTime - arrivalTimes[i];
                        double rr = (wait + burstTimes[i]) / (double)burstTimes[i];
                        if (rr > bestRR)
                        {
                            bestRR = rr;
                            sel = i;
                        }
                    }
                }
                if (sel == -1)
                {
                    currentTime++;
                    continue;
                }
                int start = currentTime;
                int finish = start + burstTimes[sel];
                int turnaround = finish - arrivalTimes[sel];
                int waiting = start - arrivalTimes[sel];
                results.Add(new SchedulingResult
                {
                    ProcessID = "P" + (sel + 1),
                    ArrivalTime = arrivalTimes[sel],
                    BurstTime = burstTimes[sel],
                    StartTime = start,
                    FinishTime = finish,
                    WaitingTime = waiting,
                    TurnaroundTime = turnaround,
                    ResponseTime = start - arrivalTimes[sel]
                });
                currentTime = finish;
                done[sel] = true;
                completed++;
            }
            return results;
        }
    }
}
