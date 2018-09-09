using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SampleApp.Models;

namespace SampleApp.Repository
{
    public interface IStudentsRepository
    {
        void Add(Student student);
        Task<IEnumerable<Student>> GetAll();
        Task<Student> Find(int id);
        Task Remove(int id);
        void Update(Student student);
        Task Save();
        Task<bool> StudentExists(int id);
    }
}
