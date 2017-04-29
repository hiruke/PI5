using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Device.Location;
using System.Data.SqlClient;


namespace FirmDAL.Tables
{
			public class DBLocation
			{

						public int lid;
						public int uid;
						public DateTime timestamp;
						public GeoCoordinate location;
						private string table = "locations";

						public DBLocation(int lid, int uid, DateTime timestamp, double latitude, double longitude)
						{
									this.lid = lid;
									this.uid = uid;
									this.timestamp = timestamp;
									this.location = new GeoCoordinate(latitude, longitude);
						}

						public DBLocation(int uid, double latitude, double longitude)
						{
									this.uid = uid;
									this.location = new GeoCoordinate(latitude, longitude);
						}

						public DBLocation(int uid)
						{
									SqlDataReader reader = new SelectQuery("lid;uid;timestamp;location.Lat as latitude;location.Long as longitude", "locations", "uid=" + uid).Read();
									while (reader.Read())
									{
												this.lid = reader.GetInt32(0);
												this.uid = reader.GetInt32(1);
												this.timestamp = reader.GetDateTime(2);
												location = new GeoCoordinate();
												location.Latitude = reader.GetDouble(3);
												location.Longitude = reader.GetDouble(4);

									}
									reader.Close();
						}

						public void Update()
						{
									string time = "SYSDATETIME()";
									string latitude = location.Latitude.ToString().Replace(',', '.');
									string longitude = location.Longitude.ToString().Replace(',', '.');
									string values = "'" + time + "';" + "geography::STGeomFromText('POINT(" + latitude + " " + longitude + ")',4326)";
									new UpdateQuery(table, "timestamp;location", values, "lid=" + this.lid).Exec();
						}
						public void Delete()
						{
									new DeleteQuery(table, "lid=" + this.lid).Exec();
						}

						public void Create()
						{
									string time = "SYSDATETIME()";
									string latitude = location.Latitude.ToString().Replace(',', '.');
									string longitude = location.Longitude.ToString().Replace(',', '.');
									string values = uid + ";" + time + ";" + "geography::STGeomFromText('POINT(" + latitude + " " + longitude + ")',4326)";
									new InsertQuery(table, "uid;timestamp;location", values).Exec();
						}

			}
}
