using RestWithASPNetUdemy.Data.VO;
using RestWithASPNetUdemy.Model;
using System.Collections.Generic;
using Tapioca.HATEOAS.Utils;

namespace RestWithASPNetUdemy.Business
{
    public interface IFileBusiness
    {
        byte[] getPdfFile();
    }
}