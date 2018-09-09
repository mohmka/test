using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SampleApp.Models;
using Microsoft.EntityFrameworkCore;

namespace SampleApp.Repository
{
    public class StudentsRepository : IStudentsRepository, IDisposable
    {

        private SampleAppContext _context;

        public StudentsRepository(SampleAppContext context)
        {
            _context = context;
        }

        public void Add(Student student)
        {
            _context.Add(student);
           
        }

        public async Task<Student> Find(int id)
        {
            return await _context.Students.SingleOrDefaultAsync(m => m.Id == id);

        }

        public async Task<IEnumerable<Student>> GetAll()
        {
          return  await _context.Students.ToListAsync();
        }

        public async Task Remove(int id)
        {
            var student = await _context.Students.SingleOrDefaultAsync(m => m.Id == id);
            _context.Students.Remove(student);
         
        }

        public void Update(Student student)
        {
            _context.Update(student);
            
        }

        public async Task Save()
        {

            await _context.SaveChangesAsync();
        }

        public async Task<bool> StudentExists(int id)
        {
            return await _context.Students.AnyAsync(e => e.Id == id);
        }

     
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _context.Dispose();
                }

                disposedValue = true;
            }
        }

       

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            GC.SuppressFinalize(this);
        }
       
    }
}
