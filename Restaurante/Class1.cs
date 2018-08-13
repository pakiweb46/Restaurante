using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LieferDienst
{
 
    public static class Class1
    {

        public const string connString = "server=localhost;user=root;pwd=mrklf598;database=dbbari;port=3306;";
        static int _globalValue;
        public static int GlobalValue
        {
            get
            {
                return _globalValue;
            }
            set
            {
                _globalValue = value;
            }
        } 
    }
    }

