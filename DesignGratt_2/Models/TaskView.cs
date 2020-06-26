using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGratt.Models
{
    [Table("TaskView")]
    public class TaskView
    {
        [Key]
        public DateTime startList { get; set; }
        public DateTime endList { get; set; }
        public int heightDateColumn { get; set; }
        public int widthDateColumn { get; set; }
        public int count { get; set; }
        public TaskView(DateTime Start, DateTime End, int Length, int Width, int Count)
        {
            startList = Start;
            endList = End;
            heightDateColumn = Length;
            widthDateColumn = Width;
            count = Count;
        }

    }
}
