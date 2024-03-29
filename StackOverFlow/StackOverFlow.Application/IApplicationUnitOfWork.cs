using StackOverFlow.Domain;
using StackOverFlow.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverFlow.Application
{
    public interface IApplicationUnitOfWork : IUnitOfWork
    {
        IQuestionRepository QuestionRepository { get; }

       /* ICourseRepository CourseRepository { get; }
        Task<(IList<CourseEnrollmentDTO> records, int total, int totalDisplay)> GetCourseEnrollmentsAsync(
            int pageIndex,
            int pageSize,
            string orderBy,
            string courseName,
            string studentName,
            DateTime enrollmentDateFrom,
            DateTime enrollmentDateTo);*/
    }
}
