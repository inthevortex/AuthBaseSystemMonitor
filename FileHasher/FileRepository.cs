using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileHasher
{
    public class FileRepository
    {
        private FileHasherContext _context;

        public FileRepository(FileHasherContext context)
        {
            _context = context;
        }

        public void SaveFile(File file)
        {
            _context.File.Add(file);
            _context.SaveChanges();
        }
    }
}
