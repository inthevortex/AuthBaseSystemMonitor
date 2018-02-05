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
        public int Id { get; set; }
        [Column(Name = "Name", DbType = "TEXT")]
        public string Name { get; set; }
        [Column(Name = "Path", DbType = "TEXT")]
        public string Path { get; set; }
        [Column(Name = "Hash", DbType = "TEXT")]
        public string Hash { get; set; }
        [Column(Name = "CreationTimeUtc", DbType = "NUMERIC")]
        public DateTime CreationTimeUtc { get; set; }
        [Column(Name = "LastAccessedTimeUtc", DbType = "NUMERIC")]
        public DateTime LastAccessedTimeUtc { get; set; }
        [Column(Name = "LastWriteTimeUtc", DbType = "NUMERIC")]
        public DateTime LastWriteTimeUtc { get; set; }
        [Column(Name = "Length", DbType = "INTEGER")]
        public long Length { get; set; }
        [Column(Name = "ReadOnly", DbType = "INTEGER")]
        public bool ReadOnly { get; set; }
        [Column(Name = "Version", DbType = "TEXT")]
        public string Version { get; set; }
        [Column(Name = "VTDetection", DbType = "TEXT")]
        public string VTDetection { get; set; }
        [Column(Name = "Length", DbType = "INTEGER")]
        public bool Whitelisted { get; set; }
        [Column(Name = "Length", DbType = "INTEGER")]
        public bool Checked { get; set; }
    }
}
