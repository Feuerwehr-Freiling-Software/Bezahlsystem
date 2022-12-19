using Paymentsystem.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paymentsystem.Shared.Interfaces
{
    public interface IArticleService
    {
        public List<Article> GetAllArticles();
    }
}
