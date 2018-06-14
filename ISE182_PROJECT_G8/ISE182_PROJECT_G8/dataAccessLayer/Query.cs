using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISE182_PROJECT_G8.dataAccessLayer
{
    abstract class Query<T>
    {
        protected string connectionString;
        protected SqlCommand command;
        protected IQueryFilter[] filters;
        // Indexes of filters
        private const int TIME = 0;
        private const int GROUP = 1;
        private const int NICKNAME = 2;

        protected Query(string connectionString)
        {
            this.connectionString = connectionString;
            this.command = new SqlCommand(); ;
            this.filters = new IQueryFilter[3];
            ClearFilters();
        }

        public abstract IList<T> Select();

        protected string WhereStatement()
        {
            command.Parameters.Clear();
            string where = "";
            bool first = true;
            foreach (IQueryFilter filter in this.filters)
            {
                if (filter != null)
                {
                    if (!first)
                    {
                        where += " AND ";
                    }

                    where += filter.GenerateWhereClause(command);
                    first = false;
                }
            }

            if (!String.IsNullOrWhiteSpace(where))
            {
                where = "WHERE " + where;
            }

            return where;
        }

        #region Filters
        public void SetTimeFilter(DateTime? dateTime)
        {
            if (dateTime.HasValue)
            {
                this.filters[TIME] = new TimeFilter(dateTime.Value);
            }
            else
            {
                this.filters[TIME] = null;
            }
        }

        public bool HasTimeFilter()
        {
            return this.filters[TIME] != null;
        }

        public bool SetGroupFilter(int? groupId)
        {
            bool filterChanged = false;
            if (groupId.HasValue)
            {
                GroupFilter newFilter = new GroupFilter(groupId.Value);
                if (!newFilter.Equals(this.filters[GROUP]))
                {
                    this.filters[GROUP] = newFilter;
                    filterChanged = true;
                }
            }
            else
            {
                if (this.filters[GROUP] != null)
                {
                    this.filters[GROUP] = null;
                    filterChanged = true;
                }
            }

            return filterChanged;
        }

        public bool SetNicknameFilter(string nickname)
        {
            bool filterChanged = false;
            if (nickname != null)
            {
                NicknameFilter newFilter = new NicknameFilter(nickname);
                if (!newFilter.Equals(this.filters[NICKNAME]))
                {
                    this.filters[NICKNAME] = newFilter;
                    filterChanged = true;
                }
            }
            else
            {
                if (this.filters[NICKNAME] != null)
                {
                    this.filters[NICKNAME] = null;
                    filterChanged = true;
                }
            }

            return filterChanged;
        }

        public void ClearFilters()
        {
            for (int i = 0; i < this.filters.Length; i++)
            {
                this.filters[i] = null;
            }
        }
        #endregion
    }
}
