using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace StackOverFlow.Domain.Entities
{
    public class Answer : IEntity<Guid>
    {
        public Guid Id { get; set; }

        public string Body { get; set; }

        public Guid AnsweredByUserId { get; set; }

        public string AnsweredByCreatorEmail { get; set; }



        public DateTime AnswerTime { get; set; }


        public int VoteCount { get; set; }


        public Guid QuestionId { get; set; }


        public ICollection<AnswerVotes> AnswerVotes { get; set; }

        public Question Question { get; set; }

        public ICollection<Reply> Replies { get; set; }
    }
}
