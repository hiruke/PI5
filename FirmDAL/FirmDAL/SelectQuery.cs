using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace FirmDAL
{
			public class SelectQuery
			{
						List<string> attribs = new List<string>();
						List<string> tables = new List<string>();
						List<string> conditions = new List<string>();
						string query;

						/// <summary>
						/// Toda condição é assumida como "e" (and) por padrão, para que a mesma seja assumida como um "ou" (or) o parametro deve ser iniciado com "+". Ex. '+x=3'
						/// </summary>
						/// <param name="_tables"></param>
						/// <param name="_attribs"></param>
						/// <param name="_conditions"></param>
						public SelectQuery(string _attribs, string _tables, string _conditions)
						{


									string whereParam = null;


									//Criação das listas de parâmetros
									foreach (string i in _attribs.Split(';'))
									{
												attribs.Add(i);
									}

									foreach (string i in _tables.Split(';'))
									{
												tables.Add(i);
									}

									foreach (string i in _conditions.Split(';'))
									{
												conditions.Add(i);
									}

									//Atributos 
									string attributes = null;
									for (int i = 0; i < attribs.Count; i++)
									{
												attributes += attribs[i];
												if (i + 1 < attribs.Count)
												{
															attributes += ",";
												}
									}


									if (conditions.Count > 0)
									{
												whereParam = " WHERE ";
												for (int i = 0; i < conditions.Count; i++)
												{

															if (conditions[i][0].ToString() == "+")
															{
																		if (i > 0)
																		{
																					whereParam += " or ";
																		}
																		whereParam += conditions[i].Substring(1);
															}
															else
															{
																		if (i > 0)
																		{
																					whereParam += " and ";
																		}
																		whereParam += conditions[i];

															}

												}
									}

									query = "SELECT " + attributes + " FROM " + _tables + whereParam;
						}

						public SelectQuery(string query)
						{
									this.query = query;
						}

						public SqlDataReader Read()
						{
									return DBCon.Read(query);
						}

						public SqlDataReader Read(string query)
						{
									return DBCon.Read(query);
						}

						public string getQuery()
						{
									return query;
						}
			}
}
