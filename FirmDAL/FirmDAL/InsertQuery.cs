using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace FirmDAL
{
			public class InsertQuery
			{

						private List<string> attribs = new List<string>();
						private List<string> values = new List<string>();
						private string query;


						public InsertQuery()
						{
									this.query = "";
						}
						public InsertQuery(string _table, string _attribs, string _values)
						{


									string attribParam = null;


									//Criação das listas de parâmetros
									foreach (string i in _attribs.Split(';'))
									{
												attribs.Add(i);
									}
									foreach (string i in _values.Split(';'))
									{
												values.Add(i);
									}

									//Atribuição de dados no parâmetro SET
									if (attribs.Count == values.Count && values.Count > 0)
									{
												attribParam = " (";
												for (int i = 0; i < attribs.Count; i++)
												{
															attribParam += attribs[i];
															if (i + 1 < attribs.Count)
															{
																		attribParam += ",";
															}
												}

												attribParam += ") values (";

												for (int i = 0; i < values.Count; i++)
												{
															attribParam += values[i];
															if (i + 1 < values.Count)
															{
																		attribParam += ",";
															}
												}

												attribParam += ") ";
									}
									else
									{
												Console.WriteLine("Quantidade diferente de parâmetros e valores");
												query = "Quantidade diferente de parâmetros e valores";
									}


									query = "insert into " + _table + attribParam;
						}

						public string getQuery()
						{
									return query;
						}

						public string Exec()
						{
									Console.WriteLine(query);
									return DBCon.Exec(query);
						}

						public string Exec(string query)
						{
									return DBCon.Exec(query);
						}

			}
}
