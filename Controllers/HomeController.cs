using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RandomPasscode.Models;
using Microsoft.AspNetCore.Http;

namespace RandomPasscode.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet(""), HttpPost("")]
        public IActionResult Index()
        {
            //to retrieve an int from session we use ".GetInt32"
            //since we don't have any int in session right now it will be equal to null
            
            if(HttpContext.Session.GetInt32("Count") == null)
            {
                // to store in int in session we use ".SetInt32"
                //if it's equal to null then set "Count" to 1 
                HttpContext.Session.SetInt32("Count", 1);
                HttpContext.Session.SetString("randomString", generateRandom());
            }
            else
            {
                // to retrieve an int from session or null.  
                int? Count = HttpContext.Session.GetInt32("Count");
                Count ++; //each time you hit generate button it would add to count on the page
                HttpContext.Session.SetInt32("Count", (int)Count); //casting Count to be an int
                HttpContext.Session.SetString("randomString", generateRandom());
            }
            // ViewBag is a dictionary that only persists over one view return. It must be set in the method rendering the view we're sending data to. 
            ViewBag.Count=HttpContext.Session.GetInt32("Count");//when the resulting view is rendered, it will have access to the ViewBag property that we set. see Index.cshtml page. 
            ViewBag.randomString=HttpContext.Session.GetString("randomString");

            
            return View();
        }

        public String generateRandom() //create Password random generator method
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"; //creating a string of characters
            char [] passcode = new char [14]; //creating a new character array that is 14 characters in length
            for(int i=0; i < passcode.Length;i++) //create a for loop to generate a random number at each indices of the array 
            {
                Random rand = new Random();
                int randomNum=rand.Next(0,chars.Length);
                passcode[i]=chars[randomNum];
            }
            return new string(passcode);//converting the character array to a string
        }

    }
}
