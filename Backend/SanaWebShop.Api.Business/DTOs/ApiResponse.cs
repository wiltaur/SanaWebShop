using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanaWebShop.Api.Business.DTOs
{
    /// <summary>
    /// Generic responses for all requests.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApiResponse<T>
    {
        public ApiResponse(T data)
        {
            Data = data;
            IsSuccess = true;
        }
        public T Data { get; set; }
        public bool IsSuccess { get; set; }
    }
}