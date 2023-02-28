using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BeehiveManagementSystem.src
{
    internal class Queen : Bee
    {
        public string StatusReport { get; private set; }
        protected override void DoJob()
        {
            //all working bees must work
            foreach (var worker in workers)
            {
                worker.WorkTheNextShift();
            }
            //feed unasigned bees

            HoneyVault.ConsumeHoney(unassignedWorkers * HONEY_PER_UNASSIGNED_WORKER);


            eggs += EGGS_PER_SHIFT;

            UpdateStatusReport();
        }
        public Queen() : base("Queen")
        {

            AssignBee("Honey Manufacturer");
            AssignBee("Egg Care");
            AssignBee("Nectar Collector");
        }

        public void CareForEggs(float eggsToConvert)
        {
            if (eggs >= eggsToConvert)
            {
                eggs -= eggsToConvert;
                ++unassignedWorkers;
            }
        }

        private void UpdateStatusReport()
        {
            StatusReport = $"Vault report:\n{HoneyVault.StatusReport}\n" +
            $"\nEgg count: {eggs:0.0}\nUnassigned workers: {unassignedWorkers:0.0}\n" +
            $"{WorkerStatus("Nectar Collector")}\n{WorkerStatus("Honey Manufacturer")}" +
            $"\n{WorkerStatus("Egg Care")}\nTOTAL WORKERS: {workers.Length}";
        }

        private string WorkerStatus(string job)
        {
            int count = 0;
            foreach (Bee worker in workers)
                if (worker.Job == job) count++;
            string s = "s";
            if (count == 1) s = "";
            return $"{count} {job} bee{s}";
        }

        public override float CostPerShift { get { return 2.15f; } }

        private Bee[] workers = new Bee[0];

        private float unassignedWorkers = 3;
        private float eggs;
        private const float EGGS_PER_SHIFT = 0.45f;
        private const float HONEY_PER_UNASSIGNED_WORKER = 0.5f;

        /// <summary>
        /// Resize workers array to add to back another worker
        /// </summary>
        /// <param name="worker"> worker bee what is added to array of workers</param>
        public void AddWorker(Bee worker)
        {

            if (unassignedWorkers >= 1)
            {
                unassignedWorkers--;
                Array.Resize(ref workers, workers.Length + 1);
                workers[workers.Length - 1] = worker;
            }

        }

        /// <summary>
        /// Creates new Bee according its job
        /// </summary>
        /// <param name="job">Bee type to be created</param>
        /// <exception cref="Exception">triggers if non of the jobs belongs to bee</exception>
        public void AssignBee(string job)
        {
            switch (job)
            {
                case "Honey Manufacturer":
                    AddWorker(new HoneyManufacturer());
                    break;
                case "Egg Care":
                    AddWorker(new EggCare(this));
                    break;
                case "Nectar Collector":
                    AddWorker(new NectarCollector());
                    break;
                default:
                    throw new Exception($"Not propariate job-{job}");
            }

            UpdateStatusReport();
        }
    }
}
