using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace logicLayer
{
    class User
    {
        private String nickname;
        private String status;

        public User(String nickname, String status)
        {
            this.nickname = nickname;
            this.status = status;
        }

        public void logout()
    }
}
