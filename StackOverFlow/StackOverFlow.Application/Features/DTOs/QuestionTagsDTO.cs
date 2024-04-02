using StackOverFlow.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverFlow.Application.Features.DTOs
{
    public class QuestionTagsDTO
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public IList<Tag>  tags { get; set; }
    }
}
