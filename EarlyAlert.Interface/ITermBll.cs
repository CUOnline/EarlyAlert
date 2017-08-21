using System.Collections.Generic;
using EarlyAlert.Model;

namespace EarlyAlert.Interface
{
    public interface ITermBll : IBll<Term>
    {
        Term GetCurrentTerm();
        List<Term> GetAllTerms();
    }
   
}
