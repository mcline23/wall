using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using wall.DbConnectors;
using wall.Models;


namespace wall.Controllers
{
    public class MessagesController : Controller
    {
       [HttpPost]
       [Route("postmessage")]
       public IActionResult Postmessage(Message nwmsg){
        if(ModelState.IsValid){
            string newmessage = $"INSERT INTO messages(users_userid, message, created_at, updated_at) VALUES( '{HttpContext.Session.GetInt32("id")}','{nwmsg.message}', NOW(), NOW())";
            DbConnector.Execute(newmessage);
           
        }
         return RedirectToAction("wall");
           }
       // return RedirectToAction("Wall");
       

       
     [HttpGet]
        [Route("wall")]
        public IActionResult Wall()
        {
        string newmessage =$"Select message, messageid, messages.created_at, messages.users_userid, first_name, last_name FROM messages LEFT Join users ON userid = messages.users_userid Where users.userid = messages.users_userid ORDER BY messages.created_at DESC";
        List<Dictionary<string, object>> allmessages = DbConnector.Query(newmessage);

        string newcomment = $"Select comments.created_at, comment, first_name, last_name, message, messageid, messages_messageid From users join messages on userid = users_userid Join comments on messageid = messages_messageid";
        
        
        // Select comment, comments.created_at, first_name, last_name FROM comments JOIN users ON userid = comments.users_userid Where users.userid = comments.users_userid; Select message, messageid, messages_messageid FROM comments JOIN messages ON messageid=comments.messages_messageid;";
        List<Dictionary<string, object>> allcomments = DbConnector.Query(newcomment);      
            
                ViewBag.Regerror = new List<string>();
                ViewBag.messages = allmessages;
                ViewBag.comments = allcomments;
                ViewBag.name= HttpContext.Session.GetString("name");
                ViewBag.id = HttpContext.Session.GetInt32("id");
          //Thinking will need to have a get string session for displaying the wall
          //Also a view bag or tempdata

            return View("wall");
       
    }
    [HttpPost]
    [Route("delete/{msg_id}")]

    public IActionResult Delete(string msg_id){
       // DELETE FROM messages Where id = {msg}
       string delete = $"DELETE FROM comments Where messages_messageid = '{msg_id}'; DELETE FROM messages where messageid= '{msg_id}';";
       DbConnector. Execute(delete);

        return RedirectToAction("wall");
    }
}}