using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirmDAL.Tables;
using System.Data.SqlClient;

namespace FirmDAL
{
			public static class DBCommander
			{

						#region Ações do usuário
						public static string CreateUser(string name, string phone, string email, string password)
						{
									DBUser usuario = new DBUser(name, phone, email, password);
									usuario.Create();
									return "";
						}

						public static string UpdateUser(int uid, string name, string phone, string email, string password)
						{
									DBUser usuario = new DBUser(uid, 1, name, phone, email, password);
									usuario.Update();
									return "";
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

						#endregion

						#region Ações de Services

						public static void CreateService(int uid, int cid, string name, string description)
						{
									DBServices service = new DBServices(uid, cid, name, description);
									service.Create();
						}

						public static void DeleteService(int sid)
						{
									DBServices service = new DBServices(sid);
									service.Delete();
						}

						public static void UpdateService(int sid, int cid, string name, string description)
						{
									DBServices service = new DBServices(sid, 0, cid, name, description);
									service.Update();
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
												string password = reader.GetString(5);
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



						#endregion

						#region Ações de Location

						public static string CreateLocation(int uid, double latitude, double longitude)
						{
									DBLocation location = new DBLocation(uid, latitude, longitude);
									location.Create();
									return "";
						}

						public static string UpdateLocation(int uid, double latitude, double logitude)
						{
									DBLocation location = new DBLocation(uid, latitude, logitude);
									location.Update();
									return "";
						}


						#endregion

			}
}