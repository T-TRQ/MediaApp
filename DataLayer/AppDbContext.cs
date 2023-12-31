﻿using Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
	public class AppDbContext : DbContext
	{
		public AppDbContext() : base("HBook")
		{
			//this.Configuration.LazyLoadingEnabled = false;
		}

		public DbSet<User> User { get; set; }
		public DbSet<Friend> Friend { get; set; }
		public DbSet<Post> Post { get; set; }
		public DbSet<FriendRequest> FriendRequest { get; set; }
		public DbSet<Comment> Comment { get; set; }


		//Place in your own instance of DbContext
		public void Reset()
		{
			using (SqlConnection conn = new SqlConnection(Database.Connection.ConnectionString))
			using (SqlCommand cmd = new SqlCommand("DECLARE @SQL VARCHAR(MAX)='' SELECT @SQL = @SQL + 'ALTER TABLE ' + QUOTENAME(FK.TABLE_SCHEMA) + '.' + QUOTENAME(FK.TABLE_NAME) + ' DROP CONSTRAINT [' + RTRIM(C.CONSTRAINT_NAME) +'];' + CHAR(13) FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS C INNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS FK ON C.CONSTRAINT_NAME = FK.CONSTRAINT_NAME INNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS PK ON C.UNIQUE_CONSTRAINT_NAME = PK.CONSTRAINT_NAME INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE CU ON C.CONSTRAINT_NAME = CU.CONSTRAINT_NAME INNER JOIN (SELECT i1.TABLE_NAME, i2.COLUMN_NAME FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS i1 INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE i2 ON i1.CONSTRAINT_NAME = i2.CONSTRAINT_NAME WHERE i1.CONSTRAINT_TYPE = 'PRIMARY KEY') PT ON PT.TABLE_NAME = PK.TABLE_NAME EXEC (@SQL);EXEC sp_msforeachtable 'ALTER TABLE ? NOCHECK CONSTRAINT all'; EXEC sp_msforeachtable 'DROP TABLE ?'", conn))
			{
				conn.Open();
				for (int i = 0; i < 5; i++)
				{
					try
					{
						cmd.ExecuteNonQuery();
					}
					catch (System.Exception)
					{
						// throw;
					}
				}
				conn.Close();
			}
			Database.Initialize(true);
		}

	}
}
