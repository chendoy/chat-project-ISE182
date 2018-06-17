using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISE182_PROJECT_G8.dataAccessLayer
{
    /*
    * interface that each where-clause extends.
    * In this way we can create generic where-clauses 
    */
    interface IQueryFilter
    {
        string GenerateWhereClause(SqlCommand command);

    }
}
