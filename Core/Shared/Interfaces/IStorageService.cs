﻿using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Shared;

public interface IStorageService
{
    Task<string> UploadAsync(Stream stream, string folder, string file, CancellationToken cancellationToken = default);
    Task DeleteFolderAsync(string folder, CancellationToken cancellationToken = default);
}