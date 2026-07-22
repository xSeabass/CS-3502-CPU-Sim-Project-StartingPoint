using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;

namespace CpuScheduler
{
    public static class Algorithms
    {
        /// <summary>
        /// Executes the First Come First Serve scheduling algorithm.
        /// </summary>
        /// <param name="processCountInput">The number of processes to schedule.</param>
        public static void RunFirstComeFirstServe(string processCountInput)
        {
            if (!int.TryParse(processCountInput, out int processCount) || processCount <= 0)
            {
                MessageBox.Show("Invalid number of processes", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            double[] burstTimes = new double[processCount];
            double[] waitingTimes = new double[processCount];
            double totalWaitingTime = 0.0;
            double averageWaitingTime;
            int i;

            DialogResult result = MessageBox.Show(
                "First Come First Serve Scheduling",
                string.Empty,
                MessageBoxButtons.YesNo,
                    MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                for (i = 0; i < processCount; i++)
                {
                    string input = Microsoft.VisualBasic.Interaction.InputBox(
                        "Enter Burst time:",
                        "Burst time for P" + (i + 1),
                        string.Empty,
                        -1,
                        -1);

                    if (!double.TryParse(input, out burstTimes[i]) || burstTimes[i] < 0)
                    {
                        MessageBox.Show("Invalid burst time", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                }

                for (i = 0; i < processCount; i++)
                {
                    if (i == 0)
                    {
                        waitingTimes[i] = 0;
                    }
                    else
                    {
                        waitingTimes[i] = waitingTimes[i - 1] + burstTimes[i - 1];
                        MessageBox.Show(
                            "Waiting time for P" + (i + 1) + " = " + waitingTimes[i],
                            "Job Queue",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.None);
                    }
                }
                for (i = 0; i < processCount; i++)
                {
                    totalWaitingTime = totalWaitingTime + waitingTimes[i];
                }
                averageWaitingTime = totalWaitingTime / processCount;
                MessageBox.Show(
                    "Average waiting time for " + processCount + " processes = " + averageWaitingTime + " sec(s)",
                    "Average Waiting Time",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.None);
            }
        }

        /// <summary>
        /// Executes the Shortest Job First scheduling algorithm.
        /// </summary>
        /// <param name="processCountInput">The number of processes to schedule.</param>
        public static void RunShortestJobFirst(string processCountInput)
        {
            if (!int.TryParse(processCountInput, out int processCount) || processCount <= 0)
            {
                MessageBox.Show("Invalid number of processes", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            double[] burstTimes = new double[processCount];
            double[] waitingTimes = new double[processCount];
            double[] sortedBurstTimes = new double[processCount];
            double totalWaitingTime = 0.0;
            double averageWaitingTime;
            int x, i;
            double temp = 0.0;
            bool found = false;

            DialogResult result = MessageBox.Show(
                "Shortest Job First Scheduling",
                string.Empty,
                MessageBoxButtons.YesNo,
                    MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                for (i = 0; i < processCount; i++)
                {
                    string input =
                        Microsoft.VisualBasic.Interaction.InputBox("Enter burst time: ",
                                                           "Burst time for P" + (i + 1),
                                                           "",
                                                           -1, -1);

                    if (!double.TryParse(input, out burstTimes[i]) || burstTimes[i] < 0)
                    {
                        MessageBox.Show("Invalid burst time", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                for (i = 0; i < processCount; i++)
                {
                    sortedBurstTimes[i] = burstTimes[i];
                }
                for (x = 0; x <= processCount - 2; x++)
                {
                    for (i = 0; i <= processCount - 2; i++)
                    {
                        if (sortedBurstTimes[i] > sortedBurstTimes[i + 1])
                        {
                            temp = sortedBurstTimes[i];
                            sortedBurstTimes[i] = sortedBurstTimes[i + 1];
                            sortedBurstTimes[i + 1] = temp;
                        }
                    }
                }
                for (i = 0; i < processCount; i++)
                {
                    if (i == 0)
                    {
                        for (x = 0; x < processCount; x++)
                        {
                            if (sortedBurstTimes[i] == burstTimes[x] && found == false)
                            {
                                waitingTimes[i] = 0;
                                MessageBox.Show(
                                    "Waiting time for P" + (x + 1) + " = " + waitingTimes[i],
                                    "Waiting time:",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.None);
                                burstTimes[x] = 0;
                                found = true;
                            }
                        }
                        found = false;
                    }
                    else
                    {
                        for (x = 0; x < processCount; x++)
                        {
                            if (sortedBurstTimes[i] == burstTimes[x] && found == false)
                            {
                                waitingTimes[i] = waitingTimes[i - 1] + sortedBurstTimes[i - 1];
                                MessageBox.Show(
                                    "Waiting time for P" + (x + 1) + " = " + waitingTimes[i],
                                    "Waiting time",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.None);
                                burstTimes[x] = 0;
                                found = true;
                            }
                        }
                        found = false;
                    }
                }
                for (i = 0; i < processCount; i++)
                {
                    totalWaitingTime = totalWaitingTime + waitingTimes[i];
                }
                averageWaitingTime = totalWaitingTime / processCount;
                MessageBox.Show(
                    "Average waiting time for " + processCount + " processes = " + averageWaitingTime + " sec(s)",
                    "Average waiting time",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Executes the Priority scheduling algorithm.
        /// </summary>
        /// <param name="processCountInput">The number of processes to schedule.</param>
        public static void RunPriorityScheduling(string processCountInput)
        {
            if (!int.TryParse(processCountInput, out int processCount) || processCount <= 0)
            {
                MessageBox.Show("Invalid number of processes", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult result = MessageBox.Show(
                "Priority Scheduling",
                string.Empty,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                double[] burstTimes = new double[processCount];
                double[] waitingTimes = new double[processCount + 1];
                int[] priorities = new int[processCount];
                int[] sortedPriorities = new int[processCount];
                int x, i;
                double totalWaitingTime = 0.0;
                double averageWaitingTime;
                int temp = 0;
                bool found = false;
                for (i = 0; i < processCount; i++)
                {
                    string input =
                        Microsoft.VisualBasic.Interaction.InputBox("Enter burst time: ",
                                                           "Burst time for P" + (i + 1),
                                                           "",
                                                           -1, -1);
                    if (!double.TryParse(input, out burstTimes[i]) || burstTimes[i] < 0)
                    {
                        MessageBox.Show("Invalid burst time", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                for (i = 0; i < processCount; i++)
                {
                    string input2 =
                        Microsoft.VisualBasic.Interaction.InputBox("Enter priority: ",
                                                           "Priority for P" + (i + 1),
                                                           "",
                                                           -1, -1);
                    if (!int.TryParse(input2, out priorities[i]) || priorities[i] < 0)
                    {
                        MessageBox.Show("Invalid priority", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                for (i = 0; i < processCount; i++)
                {
                    sortedPriorities[i] = priorities[i];
                }
                for (x = 0; x <= processCount - 2; x++)
                {
                    for (i = 0; i <= processCount - 2; i++)
                    {
                        if (sortedPriorities[i] > sortedPriorities[i + 1])
                        {
                            temp = sortedPriorities[i];
                            sortedPriorities[i] = sortedPriorities[i + 1];
                            sortedPriorities[i + 1] = temp;
                        }
                    }
                }
                for (i = 0; i < processCount; i++)
                {
                    if (i == 0)
                    {
                        for (x = 0; x < processCount; x++)
                        {
                            if (sortedPriorities[i] == priorities[x] && found == false)
                            {
                                waitingTimes[i] = 0;
                                MessageBox.Show(
                                    "Waiting time for P" + (x + 1) + " = " + waitingTimes[i],
                                    "Waiting time",
                                    MessageBoxButtons.OK);
                                temp = x;
                                priorities[x] = 0;
                                found = true;
                            }
                        }
                        found = false;
                    }
                    else
                    {
                        for (x = 0; x < processCount; x++)
                        {
                            if (sortedPriorities[i] == priorities[x] && found == false)
                            {
                                waitingTimes[i] = waitingTimes[i - 1] + burstTimes[temp];
                                MessageBox.Show(
                                    "Waiting time for P" + (x + 1) + " = " + waitingTimes[i],
                                    "Waiting time",
                                    MessageBoxButtons.OK);
                                temp = x;
                                priorities[x] = 0;
                                found = true;
                            }
                        }
                        found = false;
                    }
                }
                for (i = 0; i < processCount; i++)
                {
                    totalWaitingTime = totalWaitingTime + waitingTimes[i];
                }
                averageWaitingTime = totalWaitingTime / processCount;
                MessageBox.Show(
                    "Average waiting time for " + processCount + " processes = " + averageWaitingTime + " sec(s)",
                    "Average waiting time",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Executes the Round Robin scheduling algorithm.
        /// </summary>
        /// <param name="processCountInput">The number of processes to schedule.</param>
        public static void RunRoundRobin(string processCountInput)
        {
            if (!int.TryParse(processCountInput, out int processCount) || processCount <= 0)
            {
                MessageBox.Show("Invalid number of processes", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int index, counter = 0;
            double total;
            double timeQuantum;
            double waitTime = 0, turnaroundTime = 0;
            double averageWaitTime, averageTurnaroundTime;
            double[] arrivalTimes = new double[processCount];
            double[] burstTimes = new double[processCount];
            double[] remainingTimes = new double[processCount];
            int remainingProcesses = processCount;

            DialogResult result = MessageBox.Show(
                "Round Robin Scheduling",
                string.Empty,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                for (index = 0; index < processCount; index++)
                {
                    string arrivalInput =
                            Microsoft.VisualBasic.Interaction.InputBox("Enter arrival time: ",
                                                               "Arrival time for P" + (index + 1),
                                                               "",
                                                               -1, -1);
                    if (!double.TryParse(arrivalInput, out arrivalTimes[index]) || arrivalTimes[index] < 0)
                    {
                        MessageBox.Show("Invalid arrival time", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    string burstInput =
                            Microsoft.VisualBasic.Interaction.InputBox("Enter burst time: ",
                                                               "Burst time for P" + (index + 1),
                                                               "",
                                                               -1, -1);
                    if (!double.TryParse(burstInput, out burstTimes[index]) || burstTimes[index] < 0)
                    {
                        MessageBox.Show("Invalid burst time", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    remainingTimes[index] = burstTimes[index];
                }
                string timeQuantumInput =
                            Microsoft.VisualBasic.Interaction.InputBox("Enter time quantum: ", "Time Quantum",
                                                               "",
                                                               -1, -1);

                if (!double.TryParse(timeQuantumInput, out timeQuantum) || timeQuantum <= 0)
                {
                    MessageBox.Show("Invalid quantum time", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                Helper.QuantumTime = timeQuantumInput;

                for (total = 0, index = 0; remainingProcesses != 0;)
                {
                    if (remainingTimes[index] <= timeQuantum && remainingTimes[index] > 0)
                    {
                        total = total + remainingTimes[index];
                        remainingTimes[index] = 0;
                        counter = 1;
                    }
                    else if (remainingTimes[index] > 0)
                    {
                        remainingTimes[index] = remainingTimes[index] - timeQuantum;
                        total = total + timeQuantum;
                    }
                    if (remainingTimes[index] == 0 && counter == 1)
                    {
                        remainingProcesses--;
                        MessageBox.Show("Turnaround time for Process " + (index + 1) + " : " + (total - arrivalTimes[index]), "Turnaround time for Process " + (index + 1), MessageBoxButtons.OK);
                        MessageBox.Show("Wait time for Process " + (index + 1) + " : " + (total - arrivalTimes[index] - burstTimes[index]), "Wait time for Process " + (index + 1), MessageBoxButtons.OK);
                        turnaroundTime = turnaroundTime + total - arrivalTimes[index];
                        waitTime = waitTime + total - arrivalTimes[index] - burstTimes[index];
                        counter = 0;
                    }
                    if (index == processCount - 1)
                    {
                        index = 0;
                    }
                    else if (arrivalTimes[index + 1] <= total)
                    {
                        index++;
                    }
                    else
                    {
                        index = 0;
                    }
                }
                averageWaitTime = Convert.ToInt64(waitTime * 1.0 / processCount);
                averageTurnaroundTime = Convert.ToInt64(turnaroundTime * 1.0 / processCount);
                MessageBox.Show("Average wait time for " + processCount + " processes: " + averageWaitTime + " sec(s)", string.Empty, MessageBoxButtons.OK);
                MessageBox.Show("Average turnaround time for " + processCount + " processes: " + averageTurnaroundTime + " sec(s)", string.Empty, MessageBoxButtons.OK);
            }
        }

        // TODO: Add new scheduling algorithms below. Use the above methods as
        // examples when expanding functionality.
        // Shortest Remaining Time First (SRTF) - Preemptive
        /// <summary>
        /// Executes the Shortest Remaining Time First (SRTF) scheduling algorithm.
        /// </summary>
        /// <param name="processCountInput">The number of processes to schedule.</param>
       public static void RunSRTF(string processCountInput, CpuSchedulerForm targetForm)
        {
            if (!int.TryParse(processCountInput, out int processCount) || processCount <= 0)
            {
                MessageBox.Show("Invalid number of processes", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            double[] arrivalTimes = new double[processCount];
            double[] burstTimes = new double[processCount];
            double[] remainingTimes = new double[processCount];
            int[] startTimes = new int[processCount];
            bool[] started = new bool[processCount];

            DialogResult result = MessageBox.Show(
                "Shortest Remaining Time First (SRTF) Scheduling",
                string.Empty,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                for (int i = 0; i < processCount; i++)
                {
                    string arrivalInput = Microsoft.VisualBasic.Interaction.InputBox(
                        "Enter arrival time: ", "Arrival time for P" + (i + 1), string.Empty, -1, -1);
                    if (!double.TryParse(arrivalInput, out arrivalTimes[i]) || arrivalTimes[i] < 0)
                    {
                        MessageBox.Show("Invalid arrival time", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    string burstInput = Microsoft.VisualBasic.Interaction.InputBox(
                        "Enter burst time: ", "Burst time for P" + (i + 1), string.Empty, -1, -1);
                    if (!double.TryParse(burstInput, out burstTimes[i]) || burstTimes[i] < 0)
                    {
                        MessageBox.Show("Invalid burst time", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    remainingTimes[i] = burstTimes[i];
                    startTimes[i] = -1;
                    started[i] = false;
                }

                List<CpuSchedulerForm.SchedulingResult> resultsList = new List<CpuSchedulerForm.SchedulingResult>();
                int currentTime = 0;
                int completed = 0;

                while (completed < processCount)
                {
                    int shortestIndex = -1;
                    double minRemaining = double.MaxValue;

                    for (int i = 0; i < processCount; i++)
                    {
                        if (arrivalTimes[i] <= currentTime && remainingTimes[i] > 0 && remainingTimes[i] < minRemaining)
                        {
                            minRemaining = remainingTimes[i];
                            shortestIndex = i;
                        }
                    }

                    if (shortestIndex == -1)
                    {
                        currentTime++;
                        continue;
                    }

                    if (!started[shortestIndex])
                    {
                        startTimes[shortestIndex] = currentTime;
                        started[shortestIndex] = true;
                    }

                    remainingTimes[shortestIndex]--;
                    currentTime++;

                    if (remainingTimes[shortestIndex] == 0)
                    {
                        completed++;
                        int finishTime = currentTime;
                        int turnaround = finishTime - (int)arrivalTimes[shortestIndex];
                        int waiting = turnaround - (int)burstTimes[shortestIndex];

                        resultsList.Add(new CpuSchedulerForm.SchedulingResult
                        {
                            ProcessID = "P" + (shortestIndex + 1),
                            ArrivalTime = (int)arrivalTimes[shortestIndex],
                            BurstTime = (int)burstTimes[shortestIndex],
                            StartTime = startTimes[shortestIndex],
                            FinishTime = finishTime,
                            WaitingTime = waiting,
                            TurnaroundTime = turnaround
                        });
                    }
                }

                // Send data straight back into the main form visual tracking list view
                // Uses Reflection to access the private method on your form safely on any OS
                var method = targetForm.GetType().GetMethod("DisplaySchedulingResults", 
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                
                if (method != null)
                {
                    method.Invoke(targetForm, new object[] { resultsList.OrderBy(r => r.ProcessID).ToList(), "SRTF (Preemptive)" });
                }
            }
        }

       
        /// <summary>
        /// Executes the Highest Response Ratio Next (HRRN) scheduling algorithm.
        /// </summary>
        /// <param name="processCountInput">The number of processes to schedule.</param>
        /// <summary>
        /// Executes Non-Preemptive Highest Response Ratio Next (HRRN) Scheduling.
        /// </summary>
        public static void RunHRRN(string processCountInput, CpuSchedulerForm targetForm)
        {
            if (!int.TryParse(processCountInput, out int processCount) || processCount <= 0)
            {
                MessageBox.Show("Invalid number of processes", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            double[] arrivalTimes = new double[processCount];
            double[] burstTimes = new double[processCount];
            bool[] isCompleted = new bool[processCount];

            DialogResult result = MessageBox.Show(
                "Highest Response Ratio Next (HRRN) Scheduling",
                string.Empty,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                for (int i = 0; i < processCount; i++)
                {
                    string arrivalInput = Microsoft.VisualBasic.Interaction.InputBox(
                        "Enter arrival time: ", "Arrival time for P" + (i + 1), string.Empty, -1, -1);
                    if (!double.TryParse(arrivalInput, out arrivalTimes[i]) || arrivalTimes[i] < 0)
                    {
                        MessageBox.Show("Invalid arrival time", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    string burstInput = Microsoft.VisualBasic.Interaction.InputBox(
                        "Enter burst time: ", "Burst time for P" + (i + 1), string.Empty, -1, -1);
                    if (!double.TryParse(burstInput, out burstTimes[i]) || burstTimes[i] < 0)
                    {
                        MessageBox.Show("Invalid burst time", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    isCompleted[i] = false;
                }

                List<CpuSchedulerForm.SchedulingResult> resultsList = new List<CpuSchedulerForm.SchedulingResult>();
                int currentTime = 0;
                int completed = 0;

                while (completed < processCount)
                {
                    int targetIndex = -1;
                    double highestRatio = -1.0;

                    for (int i = 0; i < processCount; i++)
                    {
                        if (arrivalTimes[i] <= currentTime && !isCompleted[i])
                        {
                            double currentWait = currentTime - arrivalTimes[i];
                            double ratio = (currentWait + burstTimes[i]) / burstTimes[i];
                            if (ratio > highestRatio)
                            {
                                highestRatio = ratio;
                                targetIndex = i;
                            }
                        }
                    }

                    if (targetIndex == -1)
                    {
                        currentTime++;
                        continue;
                    }

                    int startTime = currentTime;
                    currentTime += (int)burstTimes[targetIndex];
                    isCompleted[targetIndex] = true;
                    completed++;

                    int turnaround = currentTime - (int)arrivalTimes[targetIndex];
                    int waiting = turnaround - (int)burstTimes[targetIndex];

                    resultsList.Add(new CpuSchedulerForm.SchedulingResult
                    {
                        ProcessID = "P" + (targetIndex + 1),
                        ArrivalTime = (int)arrivalTimes[targetIndex],
                        BurstTime = (int)burstTimes[targetIndex],
                        StartTime = startTime,
                        FinishTime = currentTime,
                        WaitingTime = waiting,
                        TurnaroundTime = turnaround
                    });
                }

                var method = targetForm.GetType().GetMethod("DisplaySchedulingResults", 
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                
                if (method != null)
                {
                    method.Invoke(targetForm, new object[] { resultsList.OrderBy(r => r.ProcessID).ToList(), "HRRN (Non-Preemptive)" });
                }
            }
        }
}
}

