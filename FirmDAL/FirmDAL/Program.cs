using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using FirmDAL.Tables;

namespace FirmDAL
{
			class Program
			{
						static void Main(string[] args)
						{
									DBCommander.CreateUser("usuario", "2222-3333", "usuario@email", "huehuebrbr");
									DBUser user = DBCommander.GetUser("usuario@email");
									DBCommander.CreateLocation(user.uid, -20.221850, -40.269261);
									DBCommander.CreateService(user.uid, 1, "svc1", "description");

									DBCommander.CreateUser("usuario2", "2222-3333", "usuario2@email", "huehuebrbr");
									DBUser user2 = DBCommander.GetUser("usuario2@email");
									DBCommander.CreateLocation(user2.uid, -20.223912, -40.280761);
									DBCommander.CreateService(user2.uid, 1, "svc2", "description");

									DBCommander.CreateUser("usuario3", "2222-3333", "usuario3@email", "huehuebrbr");
									DBUser user3 = DBCommander.GetUser("usuario3@email");
									DBCommander.CreateLocation(user3.uid, -20.277990, -40.371781);
									DBCommander.CreateService(user3.uid, 1, "svc3", "description");
									
									DBCommander.CreateUser("usuario4", "2222-3333", "usuario4@email", "huehuebrbr");
									DBUser user4 = DBCommander.GetUser("usuario4@email");
									DBCommander.CreateLocation(user4.uid, -20.250800, -40.753369);
									DBCommander.CreateService(user4.uid, 1, "svc4", "description");

									DBCommander.CreateUser("usuario5", "2222-3333", "usuario5@email", "huehuebrbr");
									DBUser user5 = DBCommander.GetUser("usuario5@email");
									DBCommander.CreateLocation(user5.uid, -20.273887, -40.400270);
									DBCommander.CreateService(user5.uid, 1, "svc5", "description");


									DBCommander.CreateUser("usuario6", "2222-3333", "usuario6@email", "huehuebrbr");
									DBUser user6 = DBCommander.GetUser("usuario6@email");
									DBCommander.CreateLocation(user6.uid, -20.278170, -40.386505);
									DBCommander.CreateService(user6.uid, 1, "svc6", "description");

									foreach (DBUser u in DBCommander.GetUsersByLocation(20, -20.222222, -40.111111))
									{
												Console.WriteLine(u.name + "-" + u.uid);
									}
									Console.ReadKey();
						}
			}
}
