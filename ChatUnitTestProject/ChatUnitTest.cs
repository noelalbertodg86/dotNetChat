using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BussinesClass;
using RabbitManager;
using Encrypt;
using Entities;

namespace ChatUnitTestProject
{
    [TestClass]
    public class ChatUnitTest
    {
        [TestMethod]
        public void SaveMessage()
        {
            MessagesBusiness messagesBusiness = new MessagesBusiness();
            bool result = messagesBusiness.SaveUserToChatRoomMessage(3, 1, "Unit test message");
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CreateNewUser()
        {
            Encrypt.EncryptManager encryptManager = new EncryptManager();
            string password = encryptManager.EncryptTextBase64("123456");
            UserBusiness userBusiness = new UserBusiness();
            bool result = userBusiness.CreateNewUser("Noel Alberto", "Diaz Garcia", Convert.ToDateTime("1986-01-27"), "noelalbertodg86@gmail.com", "noel", password, "desarrollo");
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Login()
        {
            try
            {
                Encrypt.EncryptManager encryptManager = new EncryptManager();
                string password = encryptManager.EncryptTextBase64("123456");
                UserBusiness userBusiness = new UserBusiness();
                User result = userBusiness.Login("noel", password);
                Assert.IsTrue(result != null);
            }
            catch (Exception e)
            {
                string error = e.Message;
                Assert.IsTrue(false);
            }
        }


        [TestMethod]
        public void InvoqueBot()
        {
            RabbitManager.SendRabbitMQMessage rabbitManager = new SendRabbitMQMessage();
            string resultBot = rabbitManager.SendAndRecieve("/stock=APPL​");
            Assert.IsTrue(resultBot.Contains("APPL quote is"));
        }

    }
}
