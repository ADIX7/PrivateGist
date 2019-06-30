using System;
using System.IO;
using System.Text;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;

namespace PrivateGist.Models
{
    public class InMemoryFileProvider : IFileProvider
    {
        private readonly IFileInfo _file;

        public InMemoryFileProvider(MemoryFileInfo fileInfo)
        {
            _file = fileInfo;
        }

        public IDirectoryContents GetDirectoryContents(string subpath)
        {
            throw new NotImplementedException();
        }

        public IFileInfo GetFileInfo(string subpath)
        {
            return _file;
        }

        public IChangeToken Watch(string filter)
        {
            throw new NotImplementedException();
        }

        public class MemoryFileInfo : IFileInfo
        {
            readonly byte[] _content;

            public MemoryFileInfo(string filename, string data)
            {
                Name = filename;
                _content = Encoding.UTF8.GetBytes(data);
                LastModified = DateTime.Now;
            }

            public MemoryFileInfo(string filename, byte[] data)
            {
                Name = filename;
                _content = data;
                LastModified = DateTime.Now;
            }

            public bool Exists => true;

            long IFileInfo.Length => _content.LongLength;

            public string PhysicalPath => null;

            public string Name { get; }

            public DateTimeOffset LastModified { get; }

            public bool IsDirectory => false;

            public Stream CreateReadStream()
            {
                return new MemoryStream(_content);
            }
        }
    }
}