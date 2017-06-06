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

									var result = new
									{
												status = 0,
												name = "",
												uid = 0,
												phone = "",
												email = "",
												qualification = (float)0,
									};

									DBUser user = DBCommander.GetUser(email);

									if (user.getPassword() == password)
									{
												result = new
												{
															status = user.status,
															name = user.name,
															uid = user.uid,
															phone = user.phone,
															email = user.email,
															qualification = 100 * (float)(user.qualification_pos / (float)(user.qualification_neg + user.qualification_pos)),
												};
									}

									return Json(result, JsonRequestBehavior.AllowGet);
						}//login

						[ValidateInput(false)]
						public JsonResult GetProfile(int uid)
						{

									var result = new
									{
												status = 0,
												name = "",
												uid = 0,
												phone = "",
												email = "",
									};

									DBUser user = DBCommander.GetUser(uid);
									result = new
									{
												status = user.status,
												name = user.name,
												uid = user.uid,
												phone = user.phone,
												email = user.email,
									};

									return Json(result, JsonRequestBehavior.AllowGet);
						}

						[ValidateInput(false)]
						public JsonResult UpdateLocation(int uid, double latitude, double longitude)
						{
									var result = new
									{
												cod = DBCommander.UpdateLocation(uid, latitude, longitude)
									};
									return Json(result, JsonRequestBehavior.AllowGet);
						}

						[ValidateInput(false)]
						public JsonResult Signup(string email, string password, string name, string phone, double latitude, double longitude)
						{
									var result = new
									{
												cod = int.Parse(DBCommander.CreateUser(name, phone, email, password))
									};
									int uid = DBCommander.GetUser(email).uid;
									DBCommander.CreateLocation(uid, latitude, longitude);
									return Json(result, JsonRequestBehavior.AllowGet);
						}

						[ValidateInput(false)]
						public JsonResult AddService(int uid, int cid, string name, string desc)
						{

									var result = new
									{
												cod = int.Parse(DBCommander.CreateService(uid, cid, name, desc))
									};
									return Json(result, JsonRequestBehavior.AllowGet);
						}

						[ValidateInput(false)]
						public JsonResult GetServices(int cid, int distance, double latitude, double longitude, int uid = 0)
						{
									List<DBServices> serviceList = new List<DBServices>();
									List<DBUser> list = DBCommander.GetUsersByLocation(distance, latitude, longitude);

									foreach (DBUser user in list)
									{
												serviceList = serviceList.Concat(DBCommander.GetServices(user.uid, cid)).ToList();
									}
									foreach (DBServices service in serviceList)
									{
												service.owner = DBCommander.GetUser(service.uid).name;
									}
									if (uid > 0)
									{
												serviceList.RemoveAll(service => service.uid == uid);
									}
									return Json(serviceList, JsonRequestBehavior.AllowGet);
						}

						[ValidateInput(false)]
						public JsonResult GetMyServices(int uid)
						{
									List<DBServices> serviceList = new List<DBServices>();
									serviceList = serviceList.Concat(DBCommander.GetServices(uid)).ToList();
									return Json(serviceList, JsonRequestBehavior.AllowGet);
						}

						[ValidateInput(false)]
						public JsonResult GetMyClients(int uid)
						{
									List<DBClients> clientList = DBCommander.GetMyClients(uid);

									foreach (DBClients client in clientList)
									{
												client.name = DBCommander.GetUser(client.uid).name;
												client.service = DBCommander.GetServicesByID(client.sid).name;
									}

									return Json(clientList, JsonRequestBehavior.AllowGet);
						}

						[ValidateInput(false)]
						public JsonResult AddClient(int uid, int sid)
						{
									var result = new
									{
												cod = int.Parse(DBCommander.CreateClient(uid, sid))
									};
									return Json(result, JsonRequestBehavior.AllowGet);
						}

						[ValidateInput(false)]
						public JsonResult GetMyNotifications(int uid)
						{

									List<DBNotifications> notificationList = DBCommander.GetNotifications(uid);
									return Json(notificationList, JsonRequestBehavior.AllowGet);
						}

						[ValidateInput(false)]
						public JsonResult Vote(int sid, bool positive)
						{
									return Json(DBCommander.Vote(sid, positive), JsonRequestBehavior.AllowGet);
						}

						[ValidateInput(false)]
						public JsonResult removeService(int sid)
						{
									return Json(DBCommander.GetServicesByID(sid).Delete(), JsonRequestBehavior.AllowGet);
						}

						[ValidateInput(false)]
						public JsonResult endRequest(int clid)
						{
									var result = new
									{
												cod = int.Parse(DBCommander.UpdateClient(clid, 1))
									};
									if (result.cod == 0)
									{
												DBClients client = DBCommander.GetClientByID(clid);
												DBServices service = DBCommander.GetServicesByID(client.sid);
												DBUser owner = DBCommander.GetUser(service.uid);
												DBCommander.CreateNotification(client.uid, 2, "Sua opinião é muito importante para a plataforma. Por favor nos ajuda a avaliar o proficional que lhe atendeu no serviço " + service.name + ", prestado por " + owner, client.sid.ToString());
									}
									return Json(result, JsonRequestBehavior.AllowGet);
						}


						[ValidateInput(false)]
						public JsonResult rejectRequest(int clid)
						{
									var result = new
							{
										cod = int.Parse(DBCommander.UpdateClient(clid, 1))
							};
									if (result.cod == 0)
									{
												DBClients client = DBCommander.GetClientByID(clid);
												DBServices service = DBCommander.GetServicesByID(client.sid);
												DBUser owner = DBCommander.GetUser(service.uid);
												DBCommander.CreateNotification(client.uid, 2, "Infelizemente, referente a sua solicitação para o serviço " + service.name + ", não pode ser atendida por " + owner + " no momento. Mas você pode buscar por outro profissional", client.sid.ToString());
									}
									return Json(result, JsonRequestBehavior.AllowGet);
						}

			} //controller
}//namespace
