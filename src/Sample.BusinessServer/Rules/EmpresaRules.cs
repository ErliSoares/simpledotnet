﻿using System;
using System.Collections.Generic;

using System.Text;
using Sample.BusinessInterface;
using SimpleLibrary.ServiceModel;
using NHibernate;
using SimpleLibrary.DataAccess;
using Sample.BusinessInterface.Domain;
using Sample.BusinessServer.DataAccess;
using SimpleLibrary.Rules;

namespace Sample.BusinessServer.Rules
{
    public class EmpresaRules : BaseRules<Empresa, EmpresaDao>, IEmpresaRules
    {
        public virtual IList<Empresa> GetAllWithQuery()
        {
            EmpresaDao dao = new EmpresaDao();
            return dao.GetAllWithQuery();
        }

        public virtual IList<Empresa> GetAllWithSQLQuery()
        {
            EmpresaDao dao = new EmpresaDao();
            return dao.GetAllWithSQLQuery();
        }

        //public virtual IList<Empresa> GetAllWithLINQ()
        //{
        //    EmpresaDao dao = new EmpresaDao();
        //    return dao.GetAllWithLINQ();
        //}

        public virtual IList<Empresa> GetAllWithCriteria()
        {
            EmpresaDao dao = new EmpresaDao();
            return dao.GetAllWithCriteria();
        }

        public virtual Empresa GetOne()
        {
            SimpleContext.Get().CustomData["okok"] = "ok2";
            return null;
        }
    }
}