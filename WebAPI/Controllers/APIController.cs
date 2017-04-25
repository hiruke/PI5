﻿using System;
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
						}//login

			    		[ValidateInput(false)]
						public JsonResult Cadastro(string email, string password, string name, string phone){

						    //DBCommander commando = new DBCommander();
						    //var resultado;
                            try{
							    string s = DBCommander.CreateUser(name,phone,email,password);     
						        if(s == ""){
								    var resultado = new
								    {
									    cod = 1
						    	    };
                                    return Json(resultado, JsonRequestBehavior.AllowGet);
							    }
						    }catch(IOException e){
						    }
                            var resultado2 = new
						    {
								    cod = 0
						    };
                            return Json(resultado2, JsonRequestBehavior.AllowGet);
						
			            }

						[ValidateInput(false)]
						public JsonResult OferecerServico(int uid, int cid, string name, string desc) {
						    try
							{
							    DBCommander.CreateService(uid, cid, name, desc);
							    var resposta = new
							    {
									cod = 1
								};
                                return Json(resposta, JsonRequestBehavior.AllowGet);
							}
                            catch (IOException e) {
								var resposta = new
								{
									cod = 0
								};
                                return Json(resposta, JsonRequestBehavior.AllowGet);
							}
							return Json("");
					    }

						[ValidateInput(false)]
						public JsonResult BuscarServico(int cid,int distance,double latitude, double longitude) {
                            
                            try {
                                List<DBUser> lista = DBCommander.GetUsersByLocation(distance, latitude, longitude);
                                List<DBServices> listaservicos = new List<DBServices>();
                                foreach(DBUser  usuario in lista){
                                    if((List<DBServices>)DBCommander.GetServices(usuario.uid,cid).Count>0){
                                        listaservicos.Aggregate(DBCommander.GetServices(usuario.uid,cid));
                                    }
                                }
                                return Json(listaservicos);
                            }
                            catch (IOException e) {
                            }
                        }

                        [ValidateInput(false)]
                        
			} //controller
}//namespace
