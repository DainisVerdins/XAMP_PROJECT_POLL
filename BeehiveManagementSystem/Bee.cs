using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeehiveManagementSystem { 
    internal class Bee : IWorker
    {
        /// <summary>
        /// Title of the job what bee is doing
        /// </summary>
        public string Job { get; private set; }

        /// <summary>
        /// How much honey bee consumes per shift
        /// </summary>
        public virtual float CostPerShift { get; }
        public Bee(string jobTitle)
        { 
            Job = jobTitle;
        }
        /// <summary>
        /// COnsumes CostPerShift honey amount  if in HoneyVoult have honey and do job. If not do nothing
        /// </summary>
        public void WorkTheNextShift()
        {
            if (HoneyVault.ConsumeHoney(CostPerShift))
            {
                DoJob();
            }
        }
        protected virtual void DoJob() { /* the subclass overrides this */ }
    }
}
