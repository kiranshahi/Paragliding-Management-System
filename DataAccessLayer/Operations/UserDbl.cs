﻿using System;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;

namespace DataAccessLayer.Operations
{
    public class UserDbl
    {

        public int AddUpdateUser(Users user)
        {
            using (SqlConnection con = new SqlConnection(Connection.connectionString))
            {
                SqlCommand cmd = new SqlCommand("AddUpdateUser", con);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@userID", user.Id);
                cmd.Parameters.AddWithValue("@firstName", user.FirstName);
                cmd.Parameters.AddWithValue("@lastName", user.LastName);
                cmd.Parameters.AddWithValue("@email", user.Email);
                if (user.Password == null)
                {
                    user.Password = " ";
                }
                cmd.Parameters.AddWithValue("@password", user.Password);
                cmd.Parameters.AddWithValue("@phone", user.Phone);
                cmd.Parameters.AddWithValue("@roleType", 3);
                cmd.Parameters.Add("@status", SqlDbType.Int);
                cmd.Parameters["@status"].Direction = ParameterDirection.Output;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return int.Parse(cmd.Parameters["@status"].Value.ToString());
            }
        }
        

        public int DeleteUser(int? userID)
        {
            using (SqlConnection con = new SqlConnection(Connection.connectionString))
            {

                SqlCommand cmd = new SqlCommand("DeleteUser", con);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserID", userID);
                con.Open();
                int affactedRow = cmd.ExecuteNonQuery();
                con.Close();
                return affactedRow;
            }
        }

        public IEnumerable<Users> GetAllUsers()
        {
            List<Users> lstUser = new List<Users>();
            using (SqlConnection con = new SqlConnection(Connection.connectionString))
            {
                SqlCommand cmd = new SqlCommand("GetAllUsers", con);

                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Users user = new Users();

                    user.Id = rdr["Id"].ToString();
                    user.FirstName = rdr["FirstName"].ToString();
                    user.LastName = rdr["LastName"].ToString();
                    user.Email = rdr["Email"].ToString();
                    user.Phone = rdr["PhoneNumber"].ToString();
                    lstUser.Add(user);
                }
                con.Close();
            }
            return lstUser;
        }

        public Users GetUserByID(int? id)
        {
            Users user = new Users();
            using (SqlConnection con = new SqlConnection(Connection.connectionString))
            {
                SqlCommand cmd = new SqlCommand("GetUserById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", id);


                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    user.Id = rdr["Id"].ToString();
                    user.FirstName = rdr["FirstName"].ToString();
                    user.LastName = rdr["LastName"].ToString();
                    user.Email = rdr["Email"].ToString();
                    user.Phone = rdr["Phone"].ToString();
                    user.RoleType = Convert.ToInt32(rdr["RoleType"]);
                }
                con.Close();
                return user;
            }
        }

    }
}