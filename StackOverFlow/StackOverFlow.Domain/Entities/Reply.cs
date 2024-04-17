using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverFlow.Domain.Entities
{
    public class Reply : IEntity<Guid>
    {
        public Guid Id { get; set; }

        public string Body { get; set; }

        public Guid RepliedByUserId {  get; set; }

        public string ReplyByEmail { get; set; }


        public DateTime ReplyTime { get; set; }

        public Guid AnswerId { get; set; }

        public Guid QuestionId { get; set; }

        public Question Question { get; set; }

        public Answer Answer { get; set; }
    }
}
