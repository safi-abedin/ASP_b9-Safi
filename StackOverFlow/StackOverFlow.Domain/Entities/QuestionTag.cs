using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverFlow.Domain.Entities
{
    public class QuestionTag:IEntity<Guid>
    {
        public Guid Id{ get; set; }

        public Question Question { get; set; }

        public Guid TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
