using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Net;
namespace Posts.Models
{
    public class PostModel
    {
        /// <summary>
        /// პოსტის ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// პოსტის სათაური
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "შეავსეთ პოსტის სახელი.")]
        public string Title { get; set; }
        /// <summary>
        /// პოსტის არწერა.
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "შეავსეთ პოსტის დახასიათების ველი.")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        /// <summary>
        /// პოსტის შექმნის დრო.
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "ატვირთეთ პოსტის სურათი.")]  
        public string ImageURL { get; set; }
        /// <summary>
        /// პოსტის შექმნის დრო.
        /// </summary>
        public DateTime Date { get; set; }


        public bool ValidImageURL()     // სურათის მისამართის შემოწმება.
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(ImageURL);   //request -შექმნა მოცემულ მისამართზე.

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())       //response-მიღება.
                {
                    return response.StatusCode == HttpStatusCode.OK;        //რესპონსის შემოწმება.
                }  
            }
            catch(UriFormatException ufe)   //არასწორი ლინკის შემთხვევაში.
            {
                return false;
            }
        }
    }
}
