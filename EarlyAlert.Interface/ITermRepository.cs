using System.Collections.Generic;
using EarlyAlert.Model;

namespace EarlyAlert.Interface
{
    public interface ITermRepository : IRepository<Term>
    {
        List<Term> GetAllTerms();
    }
}
