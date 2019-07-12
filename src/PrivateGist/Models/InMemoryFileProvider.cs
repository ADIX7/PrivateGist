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

        public InMemoryFileProvider(MemoryFileInfo fileInfo) => _file = fileInfo;

#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
        public IDirectoryContents GetDirectoryContents(string subpath) => throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.

        public IFileInfo GetFileInfo(string subpath) => _file;

#pragma warning disable RCS1079 // Throwing of new NotImplementedException.
        public IChangeToken Watch(string filter) => throw new NotImplementedException();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.

        public class MemoryFileInfo : IFileInfo
        {
            private readonly byte[] _content;

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

            public Stream CreateReadStream() => new MemoryStream(_content);
        }
    }
}