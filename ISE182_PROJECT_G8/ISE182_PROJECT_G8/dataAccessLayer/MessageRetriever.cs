using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISE182_PROJECT_G8.dataAccessLayer
{
    class MessageRetriever
    {
        // Access via Chatroom

        private QueryMessage query;

        public MessageRetriever()
        {
            query = new QueryMessage();
        }

        public void SetTimeFilter(DateTime dateTime)
        {
            query.SetTimeFilter(dateTime);
        }

        public void SetGroupFilter(int groupId)
        {
            query.SetGroupFilter(groupId);
        }

        public void SetNicknameFilter(string nickname)
        {
            query.SetNicknameFilter(nickname);
        }

        public void ClearFilters()
        {
            query.ClearFilters();
        }
    }
}
