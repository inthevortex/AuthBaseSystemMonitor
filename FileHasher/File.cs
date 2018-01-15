using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;

namespace FileHasher
{
    [Table(Name = "File")]
    public class File
    {
        [Column(Name = "ID", IsDbGenerated = true, IsPrimaryKey = true, DbType = "INTEGER")]
        [Key]
        public int FId { get; set; }
        [Column(Name = "Name", DbType = "VARCHAR")]
        public string Name { get; set; }
        [Column(Name = "Path", DbType = "VARCHAR")]
        public string Path { get; set; }
        [Column(Name = "Hash", DbType = "VARCHAR")]
        public string Hash { get; set; }
        [Column(Name = "CreationTimeUtc", DbType = "")]
        public DateTime CreationTimeUtc { get; set; }
        [Column(Name = "LastAccessedTimeUtc", DbType = "")]
        public DateTime LastAccessedTimeUtc { get; set; }
        [Column(Name = "LastWriteTimeUtc", DbType = "")]
        public DateTime LastWriteTimeUtc { get; set; }
        [Column(Name = "Length", DbType = "")]
        public long Length { get; set; }
        [Column(Name = "ReadOnly", DbType = "")]
        public bool ReadOnly { get; set; }
        [Column(Name = "Version", DbType = "VARCHAR")]
        public string Version { get; set; }
    }
}
