using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace bot
{
    /// <summary>
    /// class focus on work with CSV files
    /// </summary>
    public class CSV 
    {
        private string url = string.Empty;
        private string returnMessage = string.Empty;

        public CSV(string urlParam, string returnMessageParam)
        {
            url = urlParam;
            returnMessage = returnMessageParam;
        }

        public CSV()
        {
        }

        public string Execute()
        {
            string data_ = getCSVData(url);
            Dictionary<String, StockData> stockResult = FillDataDictionary(data_);
            return String.Format(returnMessage, stockResult["Result"].high.ToString());
        }

        /// <summary>
        /// pull data based on a passed url text
        /// </summary>
        /// <param name="webpageUriString"></param>
        /// <returns></returns>
        string getCSVData(string webpageUriString)
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
        public Dictionary<String, StockData> FillDataDictionary(string csvData)
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
                        if (Double.TryParse(splitLine[3].Replace(".", ","), out tempOpen))
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

    }
}
