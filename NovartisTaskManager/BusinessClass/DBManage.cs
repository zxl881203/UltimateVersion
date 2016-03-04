using NovartisTaskManager.BusinessClass; using NovartisTaskManager.Model; using System.Configuration; using System.Data; using System.Data.OleDb; // 数据库操作 ，User 表，Task表 namespace NovartisTaskManager {     public class DBManage      {         private OleDbConnection conn;         private string constr;         private User us;         private Task t1;         private DataTable dt1;         private Transaction trans;                   public DBManage(){             this.constr = ConfigurationManager.ConnectionStrings["dbConnection"].ToString();             conn = new OleDbConnection(constr);             }          public void Close()         {             if (conn.State == ConnectionState.Open)             {                 conn.Close();             }         }          public void getConnection()         {              if (conn.State == ConnectionState.Closed) conn.Open();          }          #region QueryUser         public User queryUser(string uid)         {             string sql = "select UID,UNAME,TYPE,TEAM FROM [USER] where [UID]='" + uid + "'";             this.getConnection();             OleDbCommand oldbCom = new OleDbCommand(sql, conn);             OleDbDataReader oldbReader = oldbCom.ExecuteReader();             if (oldbReader.Read())             {                 us = new User();                 us.ID = oldbReader.GetString(0);                 us.Name = oldbReader.GetString(1);                 us.Type = oldbReader.GetInt32(2);                 us.Team = oldbReader.GetString(3);                 oldbReader.Close();                 conn.Close();                 return us;             }             else return null;         }         #endregion          #region QueryTask         /// <summary>         /// 通过Task的Copypath信息查找相关任务信息         /// </summary>         /// <param name="copypath">下载后的文件路径</param>         /// <returns></returns>         public Task findTaskbyPath(string copypath)         {             string sql = "select * from [TASK] where copypath='" + copypath + "'";             this.getConnection();             OleDbCommand oldbCom = new OleDbCommand(sql, conn);             OleDbDataReader oldbReader = oldbCom.ExecuteReader();             if (oldbReader.Read())             {                 Model.Task ta = new Task();                 ta.tid = oldbReader.GetInt32(0).ToString();                 ta.tpath = oldbReader.GetString(2);                 this.Close();                 return ta;             }             else return null;         }         public void insertTask(string tname, string tpath)         {             //Task temp=this.queryTask(tpath);              if (findTaskbyPath(tpath) == null)             {                 string date = System.DateTime.Now.ToString("yyyy-MM-dd");                 string sql = "insert into [TASK]([TNAME],[TPATH],[DATE]) values('" + tname + "','" + tpath + "','" + date + "')";                 this.getConnection();                 OleDbCommand oldbCom = new OleDbCommand(sql, conn);                 oldbCom.ExecuteNonQuery();                 this.Close();             }          }
         /// <summary>         ///          /// </summary>         /// <param name="tname"></param>         /// <param name="tpath"></param>         /// <param name="copypath"></param>         /// <param name="status"></param>         /// <param name="date"></param>         public void insertTask(string tname, string tpath,string copypath,string status)         {             //Task temp=this.queryTask(tpath);              if (findTaskbyPath(tpath) == null)             {                 string date = System.DateTime.Now.ToString("yyyy-MM-dd");                 string sql = "insert into [TASK]([TNAME],[TPATH],[DATE],[STATUS],[COPYPATH]) values('" + tname + "','" + tpath + "','" + date + "','" + status + "','" + copypath+ "')";                 this.getConnection();                 OleDbCommand oldbCom = new OleDbCommand(sql, conn);                 oldbCom.ExecuteNonQuery();                 this.Close();             }          }                      #endregion



        #region TASK SQL

        #region find

        public DataTable findTasksbyDate(string date)
        {
            string sql = "select * from TASK where DATE='" + date + "'";
            dt1 = new DataTable();
            this.getConnection();
            OleDbCommand comm = new OleDbCommand(sql, conn);
            OleDbDataReader reader = comm.ExecuteReader();
            if (reader.Read())
            {
                OleDbDataAdapter adt = new OleDbDataAdapter(sql, conn);
                adt.Fill(dt1);
                //adt.f
                this.Close();
                return dt1;
            }
            else
            {
                dt1 = null;
                Close();
                return dt1;
            }
        }
        /// <summary>
        /// 查询status in ('','','','')
        /// </summary>
        /// <param name="status">string格式：'状态','状态','状态'</param>
        /// <returns></returns>
        public DataTable findTasksbyStatus(string status)
        {
            string sql = "select * from TASK where STATUS= in (" + status + ")"; ;
            dt1 = new DataTable();
            this.getConnection();
            OleDbCommand comm = new OleDbCommand(sql, conn);
            OleDbDataReader reader = comm.ExecuteReader();
            if (reader.Read())
            {
                OleDbDataAdapter adt = new OleDbDataAdapter(sql, conn);
                adt.Fill(dt1);
                this.Close();
                return dt1;
            }
            else
            {
                dt1 = null;
                Close();
                return dt1;
            }
        }
        public DataTable findTasksbySQL(string sql)
        {
            dt1 = new DataTable();
            this.getConnection();
            OleDbCommand comm = new OleDbCommand(sql, conn);
            OleDbDataReader reader = comm.ExecuteReader();
            if (reader.Read())
            {
                OleDbDataAdapter adt = new OleDbDataAdapter(sql, conn);
                adt.Fill(dt1);
                this.Close();
                return dt1;
            }
            else
            {
                dt1 = null;
                Close();
                return dt1;
            }
        }
        #endregion
        #region insert
        public bool insertData(string str)
        {
            OleDbCommand odbc = new OleDbCommand(str, conn);
            odbc.ExecuteNonQuery();
            conn.Close();
            return true;
        }
        #endregion
        #region update

        public void updateTask(string tpath, bool isDone)
        {
            string sql = "update";
            // insert into cxr(111, 222, 333, 444, 555) values('312312', '4332', '32132', '32132', '43242')
            this.getConnection();
            OleDbCommand oldbCom = new OleDbCommand(sql, conn);
            oldbCom.ExecuteReader();
            this.Close();
        }
        /// <summary>
        /// 更新数据库操作
        /// </summary>
        /// <param name="str"> SQL update语句</param>
        /// <returns></returns>
        public bool updateData(string str)
        {
            OleDbCommand odbc = new OleDbCommand(str, conn);
            odbc.ExecuteNonQuery();
            this.getConnection();
            conn.Close();
            return true;
        }
        #endregion
        #endregion
                   public string applyTask(string sql)         {             string path;             string sql1 = "select COPYPATH from Task where STATUS isNull order by'" ;             OleDbCommand dbcom = new OleDbCommand(sql, conn);             OleDbDataReader reader = dbcom.ExecuteReader();             if (reader.Read())             {                 path = reader.GetString(0);                 return path;             }             else             {                 return "没结果";             }                       }                        } } 