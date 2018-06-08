using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISE182_PROJECT_G8.dataAccessLayer
{
    class MessageRetriever
    {
        private readonly int retrieveCap = 200;
        private readonly string fields = "[Guid], [Group_Id], [Nickname], [SendTime], [Body]";
        private readonly string from = "[dbo].[Messages] JOIN[dbo].[Users] ON[User_Id] = [Id]";

        private Query query;

        public MessageRetriever()
        {
        }


    }
}
