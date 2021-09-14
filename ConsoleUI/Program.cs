using System;
using Business;
using Business.Abstract;

namespace ConsoleUI
{
    class Program
    {
       
        static void Main(string[] args)
        {
            
            string apiUrl = "https://halfiyatlaripublicdata.ibb.gov.tr/api/HalManager/getCategories";
            CategoryManager cm = new CategoryManager();

            var data = cm.GetCategories(apiUrl);
            Console.WriteLine(data);

            Console.Read();
        }
    }
}
