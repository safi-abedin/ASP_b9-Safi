using StackOverFlow.Application;
using Microsoft.EntityFrameworkCore;
using StackOverFlow.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackOverFlow.Domain.Repositories;
using StackOverFlow.Application.Features.DTOs;

namespace StackOverFlow.Infrastructure
{
	public class ApplicationUnitOfWork : UnitOfWork, IApplicationUnitOfWork
	{

        public IQuestionRepository QuestionRepository { get; private set; }

        public ITagRepository TagRepository { get; private set; }

        public IAnswerRepository AnswerRepository {  get; private set; }

        public ApplicationUnitOfWork(IQuestionRepository questionRepository,ITagRepository tagRepository
            ,IApplicationDbContext dbContext,IAnswerRepository answerRepository):base((DbContext)dbContext)
        {
            QuestionRepository = questionRepository;
            TagRepository = tagRepository;
            AnswerRepository = answerRepository;
        }


        //calling Stored Procedure
 /*       public async Task<(IList<QuestionTagsDTO> records,
            int total, int totalDisplay)> GetCourseEnrollmentsAsync(
            int pageIndex,
            int pageSize,
            string orderBy,
            string courseName,
            string studentName,
            DateTime enrollmentDateFrom,
            DateTime enrollmentDateTo)
        {
            var data = await AdoNetUtility.QueryWithStoredProcedureAsync<QuestionTagsDTO>(
                "GetCourseEnrollments",
                new Dictionary<string, object>
                {
                     { "PageIndex",  pageIndex},
                     { "PageSize",  pageSize },
                     { "OrderBy",  orderBy },
                     { "CourseName",  courseName},
                     { "StudentName",  studentName },
                     { "EnrollmentDateFrom",  enrollmentDateFrom},
                     { "EnrollmentDateTo",  enrollmentDateTo }
                },
                new Dictionary<string, Type>
                {
                     { "Total",  typeof(int)},
                     { "TotalDisplay",  typeof(int) }
                }
            );

            return (data.result, (int)data.outValues["Total"], (int)data.outValues["TotalDisplay"]);
        }*/
    }
}
