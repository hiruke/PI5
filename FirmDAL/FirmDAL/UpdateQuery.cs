using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace FirmDAL
{
    public class UpdateQuery
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

        public UpdateQuery()
        {
            this.query = "";
        }
        public UpdateQuery(string _table, string _attribs, string _values, [Optional]string _conditions)
        {


            string setParam = null;
            string whereParam = null;


            //Criação das listas de parâmetros
            foreach (string i in _attribs.Split(';'))
            {
                attribs.Add(i);
            }
            foreach (string i in _values.Split(';'))
            {
                values.Add(i);
            }

            foreach (string i in _conditions.Split(';'))
            {
                conditions.Add(i);
            }

            //Atribuição de dados no parâmetro SET
            if (attribs.Count == values.Count && values.Count > 0)
            {
                setParam = "SET ";
                for (int i = 0; i < values.Count; i++)
                {
                    setParam += attribs[i] + "=" + values[i];
                    if (i + 1 < values.Count)
                    {
                        setParam += ",";
                    }
                }
            }
            else
            {
                query = "Quantidade diferente de parâmetros e valores";
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

            query = "UPDATE " + _table + " " + setParam + whereParam;
        }

        public string getQuery()
        {
            return query;
        }

        public string Exec()
        {
            Debug.WriteLine(DateTime.Now + "> Executando query: " + query);
            return DBCon.Exec(query);
        }
        public string Exec(string query)
        {
            Debug.WriteLine(DateTime.Now + "> Executando query: " + query);
            return DBCon.Exec(query);
        }
    }
}
