using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace FirmDAL
{
			public class DeleteQuery
			{

						List<string> attribs = new List<string>();
						List<string> values = new List<string>();
						List<string> conditions = new List<string>();
						string query;

						/// <summary>
						/// Toda condição é assumida como "e" (and) por padrão, para que a mesma seja assumida como um "ou" (or) o parametro deve ser iniciado com "+". Ex. '+x=3'
						/// </summary>
						/// <param name="_table"></param>
						/// <param name="_attribs"></param>
						/// <param name="_values"></param>
						/// <param name="_conditions"></param>
						public DeleteQuery(string _table,[Optional]string _conditions)
						{


									string whereParam = null;


														foreach (string i in _conditions.Split(';'))
									{
												conditions.Add(i);
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

									query = "DELETE " + _table + whereParam;
						}

						public string getQuery()
						{
									return query;
						}

						public string Exec()
						{
									return DBCon.Exec(query);
						}
			}
}
