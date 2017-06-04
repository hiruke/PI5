using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirmDAL.Tables;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
//using System.Diagnostics;

namespace FirmDAL
{
			public static class DBCommander
			{

						#region Ações do usuário
						public static string CreateUser(string name, string phone, string email, string password)
						{
									DBUser usuario = new DBUser(name, phone, email, password);
									return usuario.Create();

						}
						public static string UpdateUser(int uid, string name, string phone, string email, string password)
						{
									DBUser usuario = new DBUser(uid, 1, name, phone, email, password);
									return usuario.Update();

						}
						public static string DeleteUser(int uid)
						{
									DBUser usuario = new DBUser(uid);
									usuario.Delete();
									return "";
						}

						/// <summary>
						/// Retorna o usuario a partir do ID
						/// </summary>
						/// <param name="uid"></param>
						/// <returns></returns>
						public static DBUser GetUser(int uid)
						{
									return new DBUser(uid);
						}


						/// <summary>
						/// Retorna o usuario a partir do email
						/// </summary>
						/// <param name="email"></param>
						/// <returns></returns>
						public static DBUser GetUser(string email)
						{
									return new DBUser(email);
						}

						public static string Vote(int sid, bool positive)
						{
									int uid = new DBServices(sid).uid;
									if (positive)
									{
												string query = "update users set qualification_pos = (select qualification_pos from users where uid=" + uid + ")+1 where uid=" + uid;
												return DBCon.Exec(query);
									}
									else
									{
												string query = "update users set qualification_neg = (select qualification_neg from users where uid=" + uid + ")+1 where uid=" + uid;
												return DBCon.Exec(query);
									}
						}

						#endregion

						#region Ações de Services

						public static string CreateService(int uid, int cid, string name, string description)
						{
									DBServices service = new DBServices(uid, cid, name, description);
									return service.Create();
						}

						public static string DeleteService(int sid)
						{
									DBServices service = new DBServices(sid);
									return service.Delete();
						}

						public static string UpdateService(int sid, int cid, string name, string description)
						{
									DBServices service = new DBServices(sid, 0, cid, name, description);
									return service.Update();
						}

						/// <summary>
						/// Retorna os services com base em usuario e categoria
						/// </summary>
						/// <param name="uid"></param>
						/// <param name="cid"></param>
						/// <returns></returns>
						public static List<DBServices> GetServices(int uid, int cid)
						{
									string query = "select sid,uid,cid,name,description from services where uid=" + uid + " and cid=" + cid;
									List<DBServices> list = new List<DBServices>();
									SqlDataReader reader = new SelectQuery(query).Read();
									while (reader.Read())
									{
												int sid = reader.GetInt32(0);
												string name = reader.GetString(3);
												string description = reader.GetString(4);
												DBServices service = new DBServices(sid, uid, cid, name, description);
												list.Add(service);
									}
									reader.Close();
									return list;
						}

						public static List<DBServices> GetServices(int uid)
						{
									string query = "select sid,uid,cid,name,description from services where uid=" + uid;
									List<DBServices> list = new List<DBServices>();
									SqlDataReader reader = new SelectQuery(query).Read();
									while (reader.Read())
									{
												int sid = reader.GetInt32(0);
												string name = reader.GetString(3);
												int cid = reader.GetInt32(2);
												string description = reader.GetString(4);
												DBServices service = new DBServices(sid, uid, cid, name, description);
												list.Add(service);
									}
									reader.Close();
									return list;
						}

						public static List<DBUser> GetUsersByLocation(int distance, double latitude, double longitude)
						{
									string query = "select u.uid,u.status,u.name,u.email,u.phone,u.password from users u,locations l where u.uid = l.uid and l.location.STDistance(geography::STGeomFromText('POINT(" + latitude.ToString().Replace(',', '.') + " " + longitude.ToString().Replace(',', '.') + ")',4326))<=" + distance * 1000;
									List<DBUser> list = new List<DBUser>();
									SqlDataReader reader = new SelectQuery(query).Read();

									while (reader.Read())
									{
												int uid = reader.GetInt32(0);
												int status = reader.GetInt32(1);
												string name = reader.GetString(2);
												string phone = reader.GetString(3);
												string email = reader.GetString(4);
												string password = reader.GetString(5);
												DBUser user = new DBUser(uid, status, name, phone, email, password);
												list.Add(user);
									}
									reader.Close();
									return list;


						}

						public static string RegisterClient(int uid, int sid)
						{
									DBClients client = new DBClients(uid, sid);
									return client.Create();
						}

						#endregion

						#region Ações de Location

						public static string CreateLocation(int uid, double latitude, double longitude)
						{
									DBLocation location = new DBLocation(uid, latitude, longitude);
									return location.Create();
						}

						public static string UpdateLocation(int uid, double latitude, double logitude)
						{
									DBLocation location = new DBLocation(uid, latitude, logitude);
									return location.Update();
						}


						#endregion

						#region Ações de Clients

						public static List<DBClients> GetMyClients(int _uid)
						{
									string query = "select DISTINCT c.clid,c.uid,c.sid,c.status from clients c,users u,services s where s.uid=" + _uid + " and s.sid=c.sid";
									List<DBClients> list = new List<DBClients>();
									SqlDataReader reader = new SelectQuery(query).Read();
									while (reader.Read())
									{
												int clid = reader.GetInt32(0);
												int uid = reader.GetInt32(1);
												int sid = reader.GetInt32(2);
												int status = reader.GetInt32(3);
												DBClients clients = new DBClients(clid, uid, sid, status);
												list.Add(clients);
									}
									reader.Close();
									return list;
						}

						public static string CreateClient(int uid, int sid)
						{
									DBClients client = new DBClients(uid, sid);
									return client.Create();
						}


						public static string UpdateClient(int clid, int status)
						{
									DBClients client = new DBClients(clid);
									client.status = status;
									return client.Update();
						}

						public static string DeleteClient(int clid)
						{
									DBClients client = new DBClients(clid);
									return client.Delete();
						}

						#endregion

						#region Ações de Notifications

						public static string CreateNotification(int uid, int type, string message, [Optional] string command)
						{

									DBNotifications notification = new DBNotifications(uid, type, 0, message, command);
									return notification.Create();
						}


						public static List<DBNotifications> GetNotifications(int uid)
						{
									string query = "SELECT nid,uid,type,timestamp,status,message,command FROM notifications where uid=" + uid;
									List<DBNotifications> list = new List<DBNotifications>();
									SqlDataReader reader = new SelectQuery(query).Read();
									while (reader.Read())
									{
												int nid = reader.GetInt32(0);
												int type = reader.GetInt32(2);
												DateTime timestamp = reader.GetDateTime(3);
												int status = reader.GetInt32(4);
												string message = reader.GetString(5);
												string command = reader.GetString(6);
												DBNotifications notification = new DBNotifications(nid, uid, type, timestamp, status, message, command);
												list.Add(notification);
									}
									reader.Close();
									return list;
						}


						#endregion

			}
}