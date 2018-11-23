using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using RabbitManager;

namespace bot
{
    /// <summary>
    /// BotProcess class manage de process of get stock data from online services and return it to chatroom
    /// </summary>
    public class BotProcess
    {
        private readonly string apiUrl = ConfigurationManager.AppSettings["url"].ToString();
        private readonly string returnMessage = ConfigurationManager.AppSettings["returnMessage"].ToString();
        private static BotProcess botInstance = null;
        private RabbitManager.SendRabbitMQMessage rabbitManager = new SendRabbitMQMessage();

        /// <summary>
        /// The class is Singleton 
        /// </summary>
        public static BotProcess GetInstance()
        {
            if (botInstance == null)
                botInstance = new BotProcess();

            return botInstance;
        }

        //class constructor
        private BotProcess()
        {
        }

        /// <summary>
        /// getStock orchestrate de process to get stock, serialize and send de result
        /// </summary>
        /// <param name="IncomingMessage"></param>
        /// <returns>resultMessage</returns>
        public string getStock(string IncomingMessage)
        {
            try
            {
                string resultMessage = string.Empty;
                RabbitMessage rabbitMessage = JsonConvert.DeserializeObject<RabbitMessage>(IncomingMessage);

                if (string.IsNullOrEmpty(rabbitMessage.Message))
                    return "The received message does not comply with the structure validation";

                string data_ = getData(apiUrl);
                Dictionary<String, StockData> stockResult = FillDataDictionary(data_);
                resultMessage = String.Format(returnMessage, stockResult["Result"].high.ToString());
                SendBotResponse(rabbitMessage.IdMessage, resultMessage);
                return resultMessage;
            }
            catch(Exception err)
            {
                string messageError = "getStock method error " + err.Message + " - " + err.StackTrace;
                EventLog.WriteEntry("BOT", messageError, EventLogEntryType.Error);
                Console.WriteLine(messageError);
                return messageError;
            }

        }

        /// <summary>
        /// pull data based on a passed url text
        /// </summary>
        /// <param name="webpageUriString"></param>
        /// <returns></returns>
        string getData(string webpageUriString)
        {
            WebClient webConnector;
            string tempStorageString = "";

            if (webpageUriString != "")
            {
                //create a new instance of the class
                //using should properly close and dispose so we don't have to bother
                using (webConnector = new WebClient())
                {
                    using (Stream responseStream = webConnector.OpenRead(webpageUriString))
                    {
                        using (StreamReader responseStreamReader = new StreamReader(responseStream))
                        {
                            //extract the response we got from the internet server
                            tempStorageString = responseStreamReader.ReadToEnd();

                            //change the unix style line endings so they work here
                            tempStorageString = tempStorageString.Replace("\n", Environment.NewLine);
                        }
                    }
                }
            }

            return tempStorageString;
        }

        /// <summary>
        /// take csv data and push it into a dictionary
        /// </summary>
        /// <param name="csvData">data from csv in internet</param>
        /// <returns></returns>
        Dictionary<String, StockData> FillDataDictionary(string csvData)
        {
            Dictionary<String, StockData> parsedStockData = new Dictionary<String, StockData>();

            using (StringReader reader = new StringReader(csvData))
            {
                string csvLine;

                //drop the first row because it is a header we don't need
                reader.ReadLine();
                int count = 0;
                while ((csvLine = reader.ReadLine()) != null)
                {
                    count++;

                    // to avoid the header in the response
                    if (count == 1)
                        continue;

                    string[] splitLine = csvLine.Split(',');

                    if (splitLine.Length >= 8)
                    {
                        StockData newItem = new StockData();

                        //parse all values from the downloaded csv file
                        newItem.symbol = splitLine[0].ToString();

                        DateTime tempDate;
                        if (DateTime.TryParse(splitLine[1], out tempDate))
                        {
                            newItem.date = tempDate;
                        }
                        DateTime tempTime;
                        if (DateTime.TryParse(splitLine[2], out tempTime))
                        {
                            newItem.time = tempTime;
                        }
                        double tempOpen;
                        if (Double.TryParse(splitLine[3].Replace(".",","), out tempOpen))
                        {
                            newItem.open = tempOpen;
                        }
                        double tempHigh;
                        if (Double.TryParse(splitLine[4].Replace(".", ","), out tempHigh))
                        {
                            newItem.high = tempHigh;
                        }
                        double tempLow;
                        if (Double.TryParse(splitLine[5].Replace(".", ","), out tempLow))
                        {
                            newItem.low = tempLow;
                        }
                        double tempClose;
                        if (Double.TryParse(splitLine[6].Replace(".", ","), out tempClose))
                        {
                            newItem.close = tempClose;
                        }
                        double tempVolume;
                        if (Double.TryParse(splitLine[7].Replace(".", ","), out tempVolume))
                        {
                            newItem.volume = tempVolume;
                        }
                        parsedStockData.Add("Result", newItem);
                    }

                }
            }

            return parsedStockData;
        }

        /// <summary>
        /// in order to send back the result of a bot call, is create a new queue with a unique idMessage:bot this queue is listen by the
        /// request sender who was the first to set the idMessage value
        /// </summary>
        /// <param name="IdMessage"></param>
        /// <param name="OutputMessage"></param>
        /// <returns></returns>
        public void SendBotResponse(string IdMessage, string OutputMessage)
        {
            string hostName = ConfigurationManager.AppSettings["localhost"].ToString();
            string[] queueList = { IdMessage, ConfigurationManager.AppSettings["queue"].ToString() };

            string botQueue = string.Join(":", queueList);
            rabbitManager.Send(OutputMessage, IdMessage, botQueue);

           Console.WriteLine(" [x] Queue [{0}] Sent {1}", botQueue, OutputMessage);
                
            
        }
    }
}
