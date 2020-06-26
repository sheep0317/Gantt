using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DesignGratt.Models
{
    public class DGrattContext: DbContext
    {
        public TaskWork task;
        public DGrattContext() : base("name=DGEntityTaskWork")
        {

        }
        public DGrattContext(TaskWork task) : base("name=DGEntityTaskWork")
        {
            this.task = task;
        }
        public DbSet<TaskView> tbTaskViews{ set; get; }
        public DbSet<TaskWork> tbTaskWorks { set; get; }
    }
}
