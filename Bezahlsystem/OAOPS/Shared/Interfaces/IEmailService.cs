using AutoMapper.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAOPS.Shared.Interfaces
{
    public interface IEmailService
    {
        Task SendArticleAlmostEmptyMail(string storageName, string slotName, int remainingAmount, string ArticleName);
        Task SendArticleEmptyMail(string storageName, string slotName, string ArticleName);
    }
}
