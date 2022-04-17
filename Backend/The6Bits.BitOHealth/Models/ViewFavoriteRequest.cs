using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The6Bits.BitOHealth.Models
{
    public class ViewFavoriteRequest
    {
        
        public List<FavoriteDrug> data { get; set; }
        public string Error { get; set; }
        public bool isSuccess { get; set; }

        public ViewFavoriteRequest(List<FavoriteDrug> data, string error, bool isSuccess)
        {
            this.data = data;
            Error = error;
            this.isSuccess = isSuccess;
        }
        public ViewFavoriteRequest()
        {
            data = new List<FavoriteDrug>();
            Error = "";
            isSuccess = false;

        }
    }
    
    }

