using System;
using System.Collections.Generic;
using EarlyAlert.Interface;
using EarlyAlert.Model;

namespace EarlyAlert.BLL
{
    public class TermBll : ITermBll
    {
        private readonly ITermRepository termRepository;

        public TermBll(ITermRepository termRepository)
        {
            this.termRepository = termRepository;
        }

        public List<Term> GetAllTerms()
        {
            return termRepository.GetAllTerms();
        }

        public Term GetCurrentTerm()
        {
            throw new NotImplementedException();
        }
    }
}
