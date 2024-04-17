using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverFlow.Domain.Entities
{
    public class QuestionVotes : IEntity<Guid>
    {
        public Guid Id { get; set; }


        public Guid QuestionId { get; set; }


        public Guid VotedBYId { get; set; }

        public string VoterEmail { get; set; }


        public bool Up { get; set; }
        public bool Down { get; set; }


        public Question Question { get; set; }
    }
}
