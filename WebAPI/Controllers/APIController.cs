using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FirmDAL;
using FirmDAL.Tables;
using System.IO;
using System.Diagnostics;


namespace WebAPI.Controllers
{
    public class APIController : Controller
    {
        // GET: Default
        public JsonResult Index()
        {
            var resultado = new
            {
                status = "OK"
            };
            //return View()
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }
        [ValidateInput(false)]
        public JsonResult Login(string email, string password)
        {

            var resultado = new
            {
                status = 0,
                name = "",
                uid = 0,
                phone = "",
                email = "",
            };

            DBUser usuario = DBCommander.GetUser(email);
            if (usuario.getPassword() == password)
            {
                resultado = new
                {
                    status = usuario.status,
                    name = usuario.name,
                    uid = usuario.uid,
                    phone = usuario.phone,
                    email = usuario.email,
                };
            }

            return Json(resultado, JsonRequestBehavior.AllowGet);
        }//login

        [ValidateInput(false)]
        public JsonResult Signup(string email, string password, string name, string phone, double latitude, double longitude)
        {
            var resultado = new
            {
                cod = int.Parse(DBCommander.CreateUser(name, phone, email, password))
            };
            int uid = DBCommander.GetUser(email).uid;
            DBCommander.CreateLocation(uid, latitude, longitude);
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        [ValidateInput(false)]
        public JsonResult AddService(int uid, int cid, string name, string desc)
        {

            var resultado = new
            {
                cod = int.Parse(DBCommander.CreateService(uid, cid, name, desc))
            };
            return Json(resultado, JsonRequestBehavior.AllowGet);

            /*	try
                {
                            DBCommander.CreateService(uid, cid, name, desc);
                            var resposta = new
                            {
                                        cod = 1
                            };
                            return Json(resposta, JsonRequestBehavior.AllowGet);
                }
                catch (IOException e)
                {
                            var resposta = new
                            {
                                        cod = 0
                            };
                            return Json(resposta, JsonRequestBehavior.AllowGet);
                }*/

        }

        [ValidateInput(false)]
        public JsonResult GetServices(int cid, int distance, double latitude, double longitude)
        {
            List<DBServices> listaServicos = new List<DBServices>();
            List<DBUser> lista = DBCommander.GetUsersByLocation(distance, latitude, longitude);

            foreach (DBUser usuario in lista)
            {
                listaServicos = listaServicos.Concat(DBCommander.GetServices(usuario.uid, cid)).ToList();
            }
            return Json(listaServicos, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetMyServices(int uid)
        {
            List<DBServices> listaServicos = new List<DBServices>();
            listaServicos = listaServicos.Concat(DBCommander.GetServices(uid)).ToList();
            return Json(listaServicos, JsonRequestBehavior.AllowGet);
        }

        [ValidateInput(false)]
        public JsonResult GetMyNotifications(int uid)
        {

            List<DBNotifications> listaNotificacoes = DBCommander.GetNotifications(uid);
            return Json(listaNotificacoes, JsonRequestBehavior.AllowGet);
        }

    } //controller
}//namespace
