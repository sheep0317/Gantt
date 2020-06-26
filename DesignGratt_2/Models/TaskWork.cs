using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DesignGratt.Models
{
    [Table("TaskWork")]
    public class TaskWork
    {
        [Key]
        public int ID { get; set; }
        public string title { get; set; }
        public string Note { get; set; }
        public int duration { get; set; }
        public DateTime dateStart { get; set; }
        public DateTime dateEnd { get; set; }


    }
}
