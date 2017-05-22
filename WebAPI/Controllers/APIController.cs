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
									//return View();
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
									try
									{
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
									}
									catch (IOException e)
									{

									}

									return Json(resultado, JsonRequestBehavior.AllowGet);
						}//login

						[ValidateInput(false)]
						public JsonResult Cadastro(string email, string password, string name, string phone, double latitude, double longitude)
						{
									var resultado = new
									{
												cod = int.Parse(DBCommander.CreateUser(name, phone, email, password))
									};
									return Json(resultado, JsonRequestBehavior.AllowGet);
						}


						[ValidateInput(false)]
						public JsonResult OferecerServico(int uid, int cid, string name, string desc)
						{
									try
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
									}
									return Json("");
						}

						[ValidateInput(false)]
						public JsonResult BuscarServico(int cid, int distance, double latitude, double longitude)
						{
									List<DBServices> listaServicos = new List<DBServices>();
									List<DBUser> lista = DBCommander.GetUsersByLocation(distance, latitude, longitude);

									foreach (DBUser usuario in lista)
									{
												listaServicos = listaServicos.Concat(DBCommander.GetServices(usuario.uid, cid)).ToList();
									}
									return Json(listaServicos, JsonRequestBehavior.AllowGet);
						}


			} //controller
}//namespace
