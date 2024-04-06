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
        public Guid QuestionId { get; set; }
        public string QuestionTitle { get; set; }
        public string QuestionBody { get; set; }
        public Guid QuestionCreatorUserId { get; set; }
        public DateTime QuestionCreationDateTime { get; set; }
        public List<TagDto> Tags { get; set; }
    }
}
