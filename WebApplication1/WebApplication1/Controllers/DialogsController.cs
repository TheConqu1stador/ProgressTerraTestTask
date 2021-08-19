using iMessengerCoreAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("/GetDialogsByClientIDs/")]
    public class DialogsController : ControllerBase
    {
        private static List<RGDialogsClients> rGDialogs = RGDialogsClients.Init();

        [HttpPost]
        /// <param name="accountId">
        /// Required account ID.
        /// </param>
        public List<Guid> GetDialogsByClientIDs([FromBody] List<Guid> clientGuids)
        {
            List<List<Guid>> clientsDialogs = new List<List<Guid>>();
            foreach (Guid client in clientGuids)
            {
                clientsDialogs.Add(rGDialogs.FindAll(x => x.IDClient == client).Select(x => x.IDRGDialog).ToList());
            }

            if (clientsDialogs.Count == 0) clientsDialogs.Add(new List<Guid>());

            var intersection = clientsDialogs.Aggregate((previousList, nextList) => previousList.Intersect(nextList).ToList());

            if (intersection.Count == 0) intersection.Add(Guid.Empty);

            return intersection;
        }
    }
}
