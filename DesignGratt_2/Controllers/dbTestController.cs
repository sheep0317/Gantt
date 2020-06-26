using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DesignGratt.Models;
using System.Data.Entity;



namespace DesignGratt.Controllers
{
    public class dbTestController
    {
        public static void initializeDB()
        {
            using (var _context = new DGrattContext())
            {
                 var taskwork= new TaskWork { title = "test" , dateStart = DateTime.Now, dateEnd = DateTime.Now };
                _context.tbTaskWorks.Add(taskwork);
                _context.SaveChanges();
            }
            
        }
    }
}
