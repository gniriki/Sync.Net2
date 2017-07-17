using System;
using System.IO;

namespace Sync.Net
{
    public interface IFileObject
    {
        string Name { get; }
        bool Exists { get; }
        long Size { get; }
        DateTime ModifiedDate { get; set; }
        Stream GetStream();
        void Create();
    }
}