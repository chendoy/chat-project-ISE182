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
            Assert.IsTrue(chatRoom.Register("bruce_wayne",42));
            Assert.IsTrue(chatRoom.Register("peter_parker8", 16));
            Assert.IsTrue(chatRoom.Register("clark_kent", 3));
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
            Assert.IsFalse(chatroom.Send("This not either"));
        }

    }
}
