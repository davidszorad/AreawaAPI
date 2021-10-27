using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Processor
{
    public class ProcessorService : IProcessorService
    {
        public async Task ProcessAsync(CancellationToken cancellationToken = default)
        {
            await Task.FromResult(0);

            // TODO: retrieve from queue
            // TODO: verify source URL
            // TODO: print website to pdf/image
            // TODO: upload printed result to storage
            // TODO: remove from queue / add to poison queue / retry mechanism
            // TODO: return PublicId from DB
        }
    }
}
