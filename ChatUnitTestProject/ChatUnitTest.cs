using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BussinesClass;
using RabbitManager;
using DBTEMP;
using System.Data.SqlClient;
using System.Data.Entity.Core.EntityClient;

namespace ChatUnitTestProject
{
    [TestClass]
    public class ChatUnitTest
    {
        [TestMethod]
        public void SaveMessage()
        {
            MessagesBusiness messagesBusiness = new MessagesBusiness();
            bool result = messagesBusiness.SaveUserToChatRoomMessage(1, 1, "Unit test message");
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CreateNewUser()
        {
            UserBusiness userBusiness = new UserBusiness();
            bool result = userBusiness.CreateNewUser("Noel Alberto", "Diaz Garcia", Convert.ToDateTime("1986-01-27"), "noelalbertodg86@gmail.com", "noel.diaz", "123456", "");
            Assert.IsTrue(result);
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
