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
            String dummyPassword = "1234";

            Assert.IsFalse(chatRoom.Register("Chen", -1, dummyPassword));
            Assert.IsFalse(chatRoom.Register("Chen2", 0, dummyPassword));
            Assert.IsFalse(chatRoom.Register("", -1, dummyPassword));
            Assert.IsFalse(chatRoom.Register("", 5, dummyPassword));
            Assert.IsFalse(chatRoom.Register("Bruce Wayne", 5, dummyPassword));
            Assert.IsFalse(chatRoom.Register("Bruce Wayne", -5, dummyPassword));
            Assert.IsFalse(chatRoom.Register(" ", -1, dummyPassword));
            Assert.IsFalse(chatRoom.Register("      ", 5, dummyPassword));
        }

        [TestMethod]
        public void Send_empty_message_OR_more_than_150()
        {
            Chatroom chatroom = new Chatroom();
            String dummyPassword = "1234";

            chatroom.Register("Chen", 8, dummyPassword);
            chatroom.clearUsersList();
            chatroom.Login("Chen", 8, dummyPassword);

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
            String dummyPassword = "1234";
            Random rnd = new Random();
            int appendix = rnd.Next(1000);

            Assert.IsFalse(chatroom.Send("This will probably not be sent"));
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

            String dummyPassword = "1234";
            Random rnd = new Random();
            int appendix = rnd.Next(9);
            Chatroom chatroom = new Chatroom();

            chatroom.Register("chend"+ appendix, appendix, dummyPassword);
            chatroom.Login("chend"+ appendix, appendix, dummyPassword);
            chatroom.LogOut();
            Assert.IsNull(chatroom.GetLoggedInUser());
            chatroom.Login("chend"+ appendix, appendix, dummyPassword);
            chatroom.LogOut();
            Assert.IsNull(chatroom.GetLoggedInUser());
            chatroom.Login("chend"+ appendix, appendix, dummyPassword);
            Assert.IsNotNull(chatroom.GetLoggedInUser());
        }

        [TestMethod]
        public void Register_With_Valid_Details()
        {
            Chatroom chatroom = new Chatroom();
            chatroom.clearUsersList();
            String dummyPassword = "1234";
            Random rnd = new Random();
            int appendix = rnd.Next(9);
   

            Assert.IsTrue(chatroom.Register("bruc"+ appendix, appendix, dummyPassword));
            Assert.IsTrue(chatroom.Register("pete"+ appendix, appendix, dummyPassword));
            Assert.IsTrue(chatroom.Register("clar"+ appendix, appendix, dummyPassword));
        }

        [TestMethod]
        public void Login_With_Non_Existing_User()
        {
            String dummyPassword = "1234";
            Chatroom chatroom = new Chatroom();
            Random rnd = new Random();
            int appendix = rnd.Next(1000);

            Assert.IsNull((chatroom.Login("chendoy"+ appendix, appendix, dummyPassword)));
            Assert.IsNull(chatroom.Login("Bruce_Wayne"+ appendix, appendix, dummyPassword));
            Assert.IsNull(chatroom.Login("clark_kent"+ appendix, appendix, dummyPassword));
            Assert.IsNull(chatroom.Login("peter_parker8"+ appendix, appendix, dummyPassword));
            Assert.IsNull(chatroom.Login("tony_stark"+ appendix, appendix, dummyPassword));
        }

    }
}
