using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace 学校管理系统1._0
{
    public class DBHelper
    {
        /*
         * 添加，删除，修改通用方法
         * 查询（很多种）
         */
        private static string Connstr = "server=(local);uid=sa;pwd=aa20000504;database=QQDB";
        /// <summary>
        /// 执行添加，删除，修改的通用方法
        /// </summary>
        /// <param name="sql">sql脚本</param>
        /// <param name="paras">参数</param>
        public static int ExecuteNonQuery(string sql,params SqlParameter[] paras)
         {
            //创建数据库连接对象\
            int result = -1;
            using (SqlConnection conn = new SqlConnection(Connstr))
            {
                //打开连接
                conn.Open();
                SqlCommand command = new SqlCommand(sql, conn);
                command.Parameters.Add(paras);//添加参数
                result = command.ExecuteNonQuery();
            }
                
            return result;
         }
        /// <summary>
        /// 执行返回第一行 第一列方法
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        public static object ExecuteScalar(string sql, params SqlParameter[] paras)
        {
            
            Object obj = null;
            using (SqlConnection conn = new SqlConnection(Connstr))
            {
                conn.Open();
                SqlCommand command = new SqlCommand(sql, conn);
                command.Parameters.AddRange(paras);//添加参数
                obj = command.ExecuteScalar();//执行脚本
            }//自动释放资源
         
            //关闭连接
            //conn.Close();
            return obj;
        }

        /// <summary>
        /// 执行返回多行多列的方法  以游标的形式
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        public static SqlDataReader ExecuteReader(string sql, params SqlParameter[] paras)
        {
            //连接式连接 和数据库保持实时连接状态
            SqlConnection conn = new SqlConnection(Connstr);
            conn.Open();
            SqlCommand command = new SqlCommand(sql, conn);
            command.Parameters.AddRange(paras);//添加参数
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            //sqlDateReader 建立在数据库连接的条件下
            return reader;
        }
        /// <summary>
        /// 执行返回一个临时表datatable 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        public static DataTable GetDaTaTable(string sql, params SqlParameter[] paras)
        {
            DataTable dt = new DataTable();
            //断开的连接，不需要显示open，也不需要Close
            using (SqlConnection conn = new SqlConnection(Connstr))
            {
                command.Parameters.AddRange(paras);//添加参数
                SqlCommand command = new SqlCommand(sql, conn);
                //创建一个数据适配器 原理：利用反射的机制返回一个结果集
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                //填充
                adapter.Fill(dt);
            }
            return dt;
        }
    }
}
