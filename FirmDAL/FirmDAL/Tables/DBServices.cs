using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace FirmDAL.Tables
{
			public class DBServices
			{
						public int sid;
						public int uid;
						public int cid;
						public string name;
						public string description;
						public string table = "services";

						public DBServices(int sid, int uid, int cid, string name, string description)
						{
									this.sid = sid;
									this.uid = uid;
									this.cid = cid;
									this.name = name;
									this.description = description;
						}

						public DBServices(int uid, int cid, string name, string description)
						{
									this.uid = uid;
									this.cid = cid;
									this.name = name;
									this.description = description;
						}

						public DBServices(int sid)
						{
									SqlDataReader reader = new SelectQuery("uid;cid;name;description", table, "sid=" + sid).Read();
									this.sid = sid;
									while (reader.Read())
									{
												this.uid = reader.GetInt32(0);
												this.cid = reader.GetInt32(1);
												this.name = reader.GetString(2);
												this.description = reader.GetString(3);
									}
									reader.Close();
						}

						public void Update()
						{
									string values = "'" + name + "';'" + description + "'";
									new UpdateQuery(table, "name;description", values, "sid=" + this.sid).Exec();
						}
						public void Delete()
						{
									new DeleteQuery(table, "sid=" + this.sid).Exec();
						}

						public void Create()
						{

									string values = uid + ";" + cid + ";'" + name + "';'" + description + "'";
									new InsertQuery(table, "uid;cid;name;description", values).Exec();
						}

			}
}
