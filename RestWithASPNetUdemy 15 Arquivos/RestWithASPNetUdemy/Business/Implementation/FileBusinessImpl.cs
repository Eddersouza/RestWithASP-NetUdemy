using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.IdentityModel.Tokens;
using RestWithASPNetUdemy.Data.VO;
using RestWithASPNetUdemy.Model;
using RestWithASPNetUdemy.Repository;
using RestWithASPNetUdemy.Security.Configuration;

namespace RestWithASPNetUdemy.Business.Implementation
{
    public class FileBusinessImpl : IFileBusiness
    {
        public byte[] getPdfFile()
        {
            string path = Directory.GetCurrentDirectory();
            var fullpath = path + "\\Other\\AngularJS na prática.pdf";
            return File.ReadAllBytes(fullpath);
        }
    }
}