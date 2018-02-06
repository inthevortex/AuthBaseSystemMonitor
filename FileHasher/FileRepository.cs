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
            if (!_context.File.Where(x => x.Name == file.Name && x.Version == file.Version).Any())
            {
                _context.File.Add(file);
                _context.SaveChanges(); 
            }
        }

        public void SaveFiles(List<File> files)
        {
            foreach (File file in files)
                SaveFile(file);
        }

        public bool CheckHash(string hash)
        {
            return _context.File.Where(x => x.Hash == hash && x.Whitelisted == true).Any();
        }

        public void DeleteAll()
        {
            _context.File.Remove(_context.File.First());
        }
    }
}
