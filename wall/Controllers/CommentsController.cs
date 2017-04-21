using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using wall.DbConnectors;


namespace wall.Controllers
{
    public class CommentsController : Controller
    {
        [HttpPost]
        [RouteAttribute("postcomment/{msgid}")]
         public IActionResult postcomment(string comment, string msgid){

            string newcomment = $"INSERT INTO comments(users_userid, comment, created_at, updated_at, messages_messageid) VALUES( '{HttpContext.Session.GetInt32("id")}','{comment}', NOW(), NOW(), '{msgid}' )";
            
           
            DbConnector.Query(newcomment);
        return RedirectToAction("wall", "messages");

        }
    }
}