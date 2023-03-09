using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeehiveManagementSystem
{
    internal interface IWorker
    {
        /// <summary>
        /// Title of the job what bee is doing
        /// </summary>
        string Job { get; }

        void WorkTheNextShift();
    }
}
