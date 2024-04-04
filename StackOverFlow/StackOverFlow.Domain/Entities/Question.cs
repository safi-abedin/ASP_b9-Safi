using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverFlow.Domain.Entities
{
    public class Question : IEntity<Guid>
    {
        public Guid Id { get; set; }

        public string title { get; set; }

        public string  Body { get; set; }


        public Guid CreatorUserId { get; set; }


        public DateTime CreationDateTime { get; set; }

        public int ViewCount { get; set; }


        public int VoteCount { get; set; }



        public ICollection<Answer> Answers { get; set; }



        public ICollection<Tag> Tags { get; set; }

    }
}
