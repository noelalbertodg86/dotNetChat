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
        /// <summary>
        /// Test Save a message into Data Base with correct data
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public void SaveMessageFromUsertoChatRoom()
        {
            MessagesBusiness messagesBusiness = new MessagesBusiness();
            bool result = messagesBusiness.SaveUserToChatRoomMessage(3, 1, "Unit test message");
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Test Create New user to the data base whith random data 
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public void CreateNewUserWithCorrectData()
        {
            EncryptManager encryptManager = new EncryptManager();
            string password = encryptManager.EncryptTextBase64("123456");
            UserBusiness userBusiness = new UserBusiness();
            bool result = userBusiness.CreateNewUser("Noel Alberto", "Diaz Garcia", Convert.ToDateTime("1986-01-27"), "noelalbertodg86@gmail.com", "noel", password, "desarrollo");
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Test of login of existing and correct user and password
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public void LoginWithCorrectUserAndPassword()
        {
            try
            {
                EncryptManager encryptManager = new EncryptManager();
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

        /// <summary>
        /// test of the robot that listens to the queue of rabbitMQ and obtains the quote from an online service
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async void GetAPPLStockUsingBotProcessWithAPPLCommand()
        {
            RabbitManager.SendRabbitMQMessage rabbitManager = new SendRabbitMQMessage();
            string resultBot = await rabbitManager.SendAndRecieve("/stock=APPL​");
            Assert.IsTrue(resultBot.Contains("APPL quote is"));
        }

    }
}
