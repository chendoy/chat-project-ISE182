
﻿using ISE182_PROJECT_G8.persistantLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ISE182_PROJECT_G8.logicLayer
{
    [Serializable]
    public class UserPL
    {
        private String nickname;
        private int groupID;
        private String password;
        private int userid;

        public UserPL(String nickname, int groupID, string password,int userid)
        {
            if (UserHandler.isValid(nickname, groupID))
            {
                this.nickname = nickname;
                this.groupID = groupID;
                this.password = password;
                this.userid = userid;

                Logger.Instance.Info("User: " + nickname + " of group " + groupID + " created successfully");
            }
            else
            {
                Logger.Instance.Error("Failed to create User: " + nickname + " of group " + groupID + " already exist");

            }
        }

        public String GetNickname()
        {
            return this.nickname;
        }
        public int GetUserId()
        {
            return this.userid;
        }
        public int GetGroupID()
        {
            return this.groupID;
        }

        public override String ToString()
        {
            return this.nickname + "[" + this.groupID + "]";
        }
        public String GetPassword()
        {
            return this.password;
        }
    }
}