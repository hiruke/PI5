using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FirmDAL;
using FirmDAL.Tables;
using System.IO;

namespace WebAPI.Controllers
{
			public class APIController : Controller
			{
						// GET: Default
						public ActionResult Index()
						{
									return View();
						}
						[ValidateInput(false)]
						public JsonResult Login(string email, string password)
						{

									var resultado = new
									{
												status=0,
												name = "",
												uid = 0,
												phone = "",
												email = "",
									};
									try
									{
												DBUser usuario = DBCommander.GetUser(email);
												if (usuario.password == password)
												{
															resultado = new
															{
																		status=usuario.status,
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
						}

						/*               [ValidateInput(false)]
																					public JsonResult Cadastro(string email, string password, string name, string phone)
			{

																									DBCommander commando = new DBCommander();
																									try{
																													string s = comando.CreateUser(name,phone,email,password);     
																													if(usuario = password){
																																	var resultado = new
												{
																																									cod = 1
												};
																													}
																									}catch(IOExceptio e){
																													var resultado = new
												{
																																									cod = 0
												};
																									}
						return Json(resultado, JsonRequestBehavior.AllowGet);
			}

																					[ValidateInput(false)]
																					public JsonResult OferecerServico(int uid, int cid, string name, string desc) {
																									DBCommander comando = new DBCommander();
																									try
																									{
																													comando.CreateService(uid, cid, name, phone);
																													var resposta = new
																													{
																																	cod = 1
																													};
																									}
																									catch (IOException e) {
																													var resposta = new
																													{
																																	cod = 0
																													};
																									}
																									return Json(resultado, JsonRequestBehavior.AllowGet);
																					}

																					[ValidateInput(false)]
																					public JsonResult BuscarServico() { 
                        
																					}*/
			}
}
