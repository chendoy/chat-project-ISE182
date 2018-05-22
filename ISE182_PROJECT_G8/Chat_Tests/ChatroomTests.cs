using System;
using ISE182_PROJECT_G8.logicLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Chat_Tests
{
    [TestClass]
    public class ChatroomTests
    {
        [TestMethod]
        public void Register_with_invalid_details()
        {
            Chatroom chatRoom = new Chatroom();

            chatRoom.clearUsersList();

            Assert.IsFalse(chatRoom.Register("Chen", -1));
            Assert.IsFalse(chatRoom.Register("Chen2", 0));
            Assert.IsFalse(chatRoom.Register("", -1));
            Assert.IsFalse(chatRoom.Register("", 5));
            Assert.IsFalse(chatRoom.Register("Bruce Wayne", 5));
            Assert.IsFalse(chatRoom.Register("Bruce Wayne", -5));
            Assert.IsFalse(chatRoom.Register(" ", -1));
            Assert.IsFalse(chatRoom.Register("      ", 5));
        }

        [TestMethod]
        public void Send_empty_message_OR_more_than_150()
        {
            Chatroom chatroom = new Chatroom();
            chatroom.Register("Chen", 8);
            chatroom.clearUsersList();
            chatroom.Login("Chen", 8);

            Assert.IsFalse(chatroom.Send(""));
            Assert.IsFalse(chatroom.Send(" "));
            Assert.IsFalse(chatroom.Send("  "));
            Assert.IsFalse(chatroom.Send("MORE_THEN_150_CHARACTERS_MORE_THEN_150_CHARACTERS_" +
                "MORE_THEN_150_CHARACTERS_MORE_THEN_150_CHARACTERS_MORE_THEN_150_CHARACTERS_" +
                "MORE_THEN_150_CHARACTERS_MORE_THEN_150_CHARACTERS_MORE_THEN_150_CHARACTERS_"));
        }

        [TestMethod]
        public void Send_message_without_logging_in()
        {
            Chatroom chatroom = new Chatroom();

            Assert.IsFalse(chatroom.Send("This will probably not be sent"));
            Assert.IsFalse(chatroom.Send("This will not either"));
            chatroom.Register("chendoy", 8);
            chatroom.Login("chendoy", 8);
            Assert.IsTrue(chatroom.Send("This SHOULD be sent"));
            chatroom.LogOut();
            Assert.IsFalse(chatroom.Send("Aaaaand this won't"));
            Assert.IsFalse(chatroom.Send("This will not either"));
        }

        [TestMethod]
        public void Message_Validity_By_MessageHandler()
        {
            Assert.IsTrue(MessageHandler.isValid("hi"));
            Assert.IsTrue(MessageHandler.isValid("hello"));
            Assert.IsTrue(MessageHandler.isValid("ISE182"));
            Assert.IsFalse(MessageHandler.isValid(""));
            Assert.IsFalse(MessageHandler.isValid("      "));
            Assert.IsFalse(MessageHandler.isValid("MORE_THEN_150_CHARACTERS_MORE_THEN_150_CHARACTERS_" +
                "MORE_THEN_150_CHARACTERS_MORE_THEN_150_CHARACTERS_MORE_THEN_150_CHARACTERS_" +
                "MORE_THEN_150_CHARACTERS_MORE_THEN_150_CHARACTERS_MORE_THEN_150_CHARACTERS_"));
        }
        [TestMethod]
        public void Logout_test()
        {
            Chatroom chatroom = new Chatroom();
            chatroom.Register("chendoy", 8);
            chatroom.Login("chendoy", 8);
            chatroom.LogOut();
            Assert.IsNull(chatroom.GetLoggedInUser());
            chatroom.Login("chendoy", 8);
            chatroom.LogOut();
            Assert.IsNull(chatroom.GetLoggedInUser());
            chatroom.Login("chendoy", 8);
            Assert.IsNotNull(chatroom.GetLoggedInUser());
        }

        [TestMethod]
        public void Register_With_Valid_Details()
        {
            Chatroom chatroom = new Chatroom();
            chatroom.clearUsersList();
            Assert.IsTrue(chatroom.Register("bruce_wayne", 42));
            Assert.IsTrue(chatroom.Register("peter_parker8", 16));
            Assert.IsTrue(chatroom.Register("clark_kent", 3));
        }

        [TestMethod]
        public void Login_With_Non_Existing_User()
        {
            Chatroom chatroom = new Chatroom();
            Assert.IsFalse(chatroom.Login("chendoy", 8));
            Assert.IsFalse(chatroom.Login("Bruce_Wayne", 5));
            Assert.IsFalse(chatroom.Login("clark_kent", 22));
            Assert.IsFalse(chatroom.Login("peter_parker8", 8));
            Assert.IsFalse(chatroom.Login("tony_stark", 31));
        }

    }
}
